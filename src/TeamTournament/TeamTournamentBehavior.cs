using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.TournamentMissions.Missions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace TournamentsEnhanced.TeamTournament
{
  public class TeamTournamentBehavior : MissionLogic, ICameraModeLogic
  {
    public TournamentGame TournamentGame { get; private set; }
    public int PlayerTeamLostAtRound { get => _playerLostAtRound; }
    public bool IsPlayerEliminated { get; private set; }
    public int CurrentRoundIndex { get; private set; }
    public TeamTournamentMatch LastMatch { get; private set; }
    public TeamTournamentRound[] Rounds { get; private set; }
    public SpectatorCameraTypes GetMissionCameraLockMode(bool lockedToMainPlayer)
      => !this.IsPlayerParticipating ? SpectatorCameraTypes.LockToAnyAgent : SpectatorCameraTypes.Invalid;
    public TeamTournamentRound CurrentRound => this.Rounds[this.CurrentRoundIndex];
    public TeamTournamentRound NextRound => this.CurrentRoundIndex < this.Rounds.Length - 1 ? this.Rounds[this.CurrentRoundIndex + 1] : null;
    public TeamTournamentMatch CurrentMatch => this.CurrentRound != null ? this.CurrentRound.CurrentMatch : null;
    public TeamTournamentMember Winner { get; private set; }
    public bool IsPlayerParticipating { get; private set; }
    public Settlement Settlement { get; private set; }
    public float BetOdd { get; private set; }
    public int MaximumBetInstance => Math.Min(150, this.PlayerDenars);
    public int BettedDenars { get; private set; }
    public int OverallExpectedDenars { get; private set; }
    public int PlayerDenars => Hero.MainHero.Gold;
    private TournamentKB CurrentTKB { get; set; }
    private TeamTournamentMissionController MissionBehavior { get; set; }

    public TeamTournamentBehavior(TournamentGame tournamentGame, Settlement settlement, ITournamentGameBehavior gameBehavior, bool isPlayerParticipating)
    {
      this.Settlement = settlement;
      this.TournamentGame = tournamentGame;
      this.MissionBehavior = gameBehavior as TeamTournamentMissionController;
      this.CurrentTKB = TournamentKB.Current;
      this.Rounds = new TeamTournamentRound[CurrentTKB.Rounds];
      this.CreateTeams();
      this.CurrentRoundIndex = -1;
      this.LastMatch = null;
      this.Winner = null;
      this.IsPlayerParticipating = isPlayerParticipating;
    }

    private void CreateTeams()
    {
      // first we take in our selected team
      var mainParticipants = new List<TeamTournamentMember>(CurrentTKB.SelectedRoster.Select(x => new TeamTournamentMember(x)));
      _teams = new List<TeamTournamentTeam>() {
          new TeamTournamentTeam(mainParticipants, Hero.MainHero.ClanBanner, Hero.MainHero.ClanBanner.GetPrimaryColor())
      };

      // now create rest
      var createdTeams = CreateTournamentTeams();

      // if we managed to get all, merge with our team and set
      _teams.AddRange(createdTeams);
    }

    private List<TeamTournamentTeam> CreateTournamentTeams()
    {
      var teamComposition = new List<TournamentParticipant>();

      // now check out if we can form teams locally from other heroes
      var heroesInSettlement = this.Settlement
        .GetCombatantHeroesInSettlement()
        .Where(x => !CurrentTKB.SelectedRoster.Contains(x));

      var createdTeams = new List<TeamTournamentTeam>();
      var addedParticipants = new List<CharacterObject>();

      // TODO: appy logic from TournamentParticipantPatch

      // first try to get teams of every local party, "they arrived just for this event"
      foreach (var partyLeader in heroesInSettlement.Where(x => x.HeroObject.IsPartyLeader))
      {
        var strongestPartyTeam = MobilePartyHelper
          .GetStrongestAndPriorTroops(partyLeader.HeroObject.PartyBelongedTo, CurrentTKB.TeamSize, false)
          .Select(x => new TeamTournamentMember(x.Troop));

        // if this party can't at least have a team full team, drop them
        if (strongestPartyTeam.Count() == CurrentTKB.TeamSize)
        {
          createdTeams.Add(new TeamTournamentTeam(strongestPartyTeam.ToList()));
          addedParticipants.AddRange(strongestPartyTeam.Select(x => x.Character));
        }

        if (createdTeams.Count >= CurrentTKB.TeamsCount - 1) // remember we already have one team
          return createdTeams;
      }

      var currentTeam = new List<TeamTournamentMember>();

      // if we are still not done, create teams with local heroes
      var possibleHeroes = heroesInSettlement.Where(x => !x.HeroObject.IsPartyLeader && !addedParticipants.Contains(x)).ToList();
      foreach (var localHero in possibleHeroes)
      {
        currentTeam.Add(new TeamTournamentMember(localHero));

        if (currentTeam.Count() >= CurrentTKB.TeamSize)
        {
          var team1 = new TeamTournamentTeam(currentTeam);
          createdTeams.Add(team1);
          currentTeam = new List<TeamTournamentMember>();
        }

        if (possibleHeroes.Count() - currentTeam.Count() <= 0 && currentTeam.Count() > 0) // fill up hero team if no more heroes left 
        {
          var simpletonList = GetSimpletons(Settlement.Culture).Shuffle().Take(CurrentTKB.TeamSize - currentTeam.Count()).Select(x => new TeamTournamentMember(x)).ToList();
          currentTeam.AddRange(simpletonList);
          createdTeams.Add(new TeamTournamentTeam(currentTeam));
          currentTeam = new List<TeamTournamentMember>();
        }

        if (createdTeams.Count >= CurrentTKB.TeamsCount - 1) // remember we already have one team
          return createdTeams;
      }

      // well still not done, just add troops to fill it
      var possibleSimpletons = GetSimpletons(Settlement.Culture).ToList();
      if (possibleSimpletons.Count > 0)
      {
        do
        {
          var teamToAdd = new List<CharacterObject>();
          for (var i = 0; i < CurrentTKB.TeamSize; i++)
          {
            teamToAdd.Add(possibleSimpletons.GetRandomElement());
          }
          createdTeams.Add(new TeamTournamentTeam(teamToAdd.Select(x => new TeamTournamentMember(x)).ToList()));
        }
        while (createdTeams.Count < CurrentTKB.TeamsCount - 1);

        if (createdTeams.Count >= CurrentTKB.TeamsCount - 1) // remember we already have one team
          return createdTeams;
      }

      // if not done here, something was really bad fill up with ALL
      possibleSimpletons = GetSimpletons().ToList();
      if (possibleSimpletons.Count > 0)
      {
        do
        {
          var teamToAdd = new List<CharacterObject>();
          for (var i = 0; i < CurrentTKB.TeamSize; i++)
          {
            teamToAdd.Add(possibleSimpletons.GetRandomElement());
          }
          createdTeams.Add(new TeamTournamentTeam(teamToAdd.Select(x => new TeamTournamentMember(x)).ToList()));
        }
        while (createdTeams.Count < CurrentTKB.TeamsCount - 1);
        return createdTeams;
      }

      return null;
    }

    private List<CharacterObject> GetSimpletons(CultureObject culture = null, CharacterObject baseChar = null)
    {
      if (baseChar == null)
        baseChar = CharacterObject.FindFirst(x => x.IsBasicTroop && x.UpgradeTargets != null && (culture != null ? x.Culture == culture : true));

      var simpletons = new List<CharacterObject>() { baseChar };

      if (baseChar.UpgradeTargets != null && baseChar.UpgradeTargets.Length > 0)
      {
        for (var i = 0; i < baseChar.UpgradeTargets.Length; i++)
          simpletons.AddRange(GetSimpletons(culture, baseChar.UpgradeTargets[i]));
      }

      return simpletons;
    }

    public override void AfterStart()
    {
      this.CurrentRoundIndex = 0;
      this.CreateTorunamentTree();
      this.CalculateBet();
      Utilities.SetDifficulty();
    }

    public override void OnMissionTick(float dt)
    {
      if (this.CurrentMatch != null && this.CurrentMatch.State == TournamentMatch.MatchState.Started && this.MissionBehavior.IsMatchEnded())
        this.EndCurrentMatch();
    }

    public void StartMatch()
    {
      if (this.CurrentMatch.IsPlayerParticipating)
      {
        Campaign.Current.TournamentManager.OnPlayerJoinMatch(this.TournamentGame.GetType());
      }
      this.CurrentMatch.Start();
      base.Mission.SetMissionMode(MissionMode.Tournament, true);
      this.MissionBehavior.StartMatch(this.CurrentMatch, this.NextRound == null);
      CampaignEventDispatcher.Instance.OnPlayerStartedTournamentMatch(this.Settlement.Town);
    }

    public void SkipMatch()
    {
      if (this.CurrentMatch.IsReady)
        this.CurrentMatch.Start();

      this.MissionBehavior.SkipMatch(this.CurrentMatch);
      this.EndCurrentMatch();
    }

    private void EndCurrentMatch()
    {
      this.LastMatch = this.CurrentMatch;
      this.CurrentRound.EndMatch();
      this.MissionBehavior.OnMatchEnded();

      // add winners to next round
      if (this.NextRound != null)
      {
        // fill in round
        this.LastMatch.Winners.ToList().ForEach(x => NextRound.AddTeam(x));
        this.MatchEnd?.Invoke(this.LastMatch);
      }

      // fire off events if player was disqualified 
      if (this.LastMatch.IsPlayerParticipating)
      {
        if (!this.LastMatch.IsPlayerTeamWinner)
          this.OnPlayerTeamEliminated();
        else
          this.OnPlayerTeamWinMatch();
      }

      if (this.CurrentRound.CurrentMatch == null) // done with this round
      {
        // check if done with Tournament or not
        if (this.CurrentRoundIndex < this.Rounds.Length - 1)
        {
          // not done yet, go to next round
          this.CurrentRoundIndex++;
          this.CalculateBet();
        }
        else
        {
          // done with Tournament
          this.CalculateBet();
          InformationManager.AddQuickInformation(new TextObject("{=tWzLqegB}Tournament is over.", null), 0, null, "");
          this.Winner = this.LastMatch.Winners.First().GetTeamLeader();
          if (this.Winner.Character.IsHero)
          {
            if (this.Winner.Character == CharacterObject.PlayerCharacter)
              this.OnPlayerWinTournament();

            Campaign.Current.TournamentManager.AddLeaderboardEntry(this.Winner.Character.HeroObject);
          }
          Utilities.UnsetDifficulty();
          CampaignEventDispatcher.Instance.OnTournamentWon(this.Winner.Character, this.Settlement.Town);
          this.TournamentEnd?.Invoke();
        }
      }
    }

    private void OnPlayerTeamEliminated()
    {
      this._playerLostAtRound = this.CurrentRoundIndex + 1;
      this.IsPlayerEliminated = true;
      this.BetOdd = 0f;
      if (this.BettedDenars > 0)
      {
        GiveGoldAction.ApplyForCharacterToSettlement(null, Settlement.CurrentSettlement, this.BettedDenars, false);
      }
      this.OverallExpectedDenars = 0;
      CampaignEventDispatcher.Instance.OnPlayerEliminatedFromTournament(this.CurrentRoundIndex, this.Settlement.Town);
    }

    private void OnPlayerTeamWinMatch() => Campaign.Current.TournamentManager.OnPlayerWinMatch(this.TournamentGame.GetType());

    private void OnPlayerWinTournament()
    {
      if (Campaign.Current.GameMode != CampaignGameMode.Campaign)
        return;

      GainRenownAction.Apply(Hero.MainHero, this.TournamentGame.TournamentWinRenown, false);

      if (Hero.MainHero.MapFaction.IsKingdomFaction && Hero.MainHero.MapFaction.Leader != Hero.MainHero)
        GainKingdomInfluenceAction.ApplyForDefault(Hero.MainHero, 1f);

      Hero.MainHero.PartyBelongedTo.ItemRoster.AddToCounts(this.TournamentGame.Prize, 1);

      if (this.OverallExpectedDenars > 0)
        GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.OverallExpectedDenars, false);

      Campaign.Current.TournamentManager.OnPlayerWinTournament(this.TournamentGame.GetType());
    }

    private void CreateTorunamentTree()
    {
      var T = CurrentTKB.TeamsCount;          // Teams count
      var R = CurrentTKB.Rounds;              // Rounds count 
      var M = CurrentTKB.FirstRoundMatches;   // Matches count
      var TPM = T / M;                        // Teams per match
      var W = TPM / 2;                        // Winners per match

      if (Math.Log(T, 2) > 4) // current interface allows 4 rounds max
        W = 1;

      for (int r = 0; r < R; r++)
      {
        Rounds[r] = new TeamTournamentRound(T, M, W);

        if (r < R)
        {
          T = W * M;
          TPM = Math.Max(Math.Min(MBRandom.Random.Next(0, 2) * 2 + 2, T / 2), 2);
          M = T / TPM;
          W = TPM / 2;
        }
      }

      // fill in first round
      this._teams.ForEach(x => Rounds[0].AddTeam(x));
    }

    public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
    {
      canPlayerLeave = false;
      return null;
    }

    public void PlaceABet(int bet)
    {
      this.BettedDenars += bet;
      this.OverallExpectedDenars += this.GetExpectedDenarsForBet(bet);
      GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, bet, true);
    }

    public int GetExpectedDenarsForBet(int bet) => (int)(this.BetOdd * bet);

    public int GetMaximumBet()
    {
      if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
        return 150 * (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
      return 150;
    }

    // TODO: this needs "rework"
    private void CalculateBet()
    {
      if (this.IsPlayerParticipating)
      {
        if (this.CurrentRound.CurrentMatch == null)
        {
          this.BetOdd = 0f;
          return;
        }

        if (this.IsPlayerEliminated || !this.IsPlayerParticipating)
        {
          this.OverallExpectedDenars = 0;
          this.BetOdd = 0f;
          return;
        }
        // TODO: make a better bet odd calculation
        this.BetOdd = MBRandom.Random.Next(3, 5);
      }
    }

    public event Action TournamentEnd;
    public event Action<TeamTournamentMatch> MatchEnd;
    public const float EndMatchTimerDuration = 6f;
    private List<TeamTournamentTeam> _teams;
    private int _playerLostAtRound;
    public const float MaximumOdd = 4f;
  }
}
