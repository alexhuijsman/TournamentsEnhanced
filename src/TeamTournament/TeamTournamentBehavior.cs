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
      => !IsPlayerParticipating ? SpectatorCameraTypes.LockToAnyAgent : SpectatorCameraTypes.Invalid;
    public TeamTournamentRound CurrentRound => Rounds[CurrentRoundIndex];
    public TeamTournamentRound NextRound => CurrentRoundIndex < Rounds.Length - 1 ? Rounds[CurrentRoundIndex + 1] : null;
    public TeamTournamentMatch CurrentMatch => CurrentRound != null ? CurrentRound.CurrentMatch : null;
    public TeamTournamentMember Winner { get; private set; }
    public bool IsPlayerParticipating { get; private set; }
    public Settlement Settlement { get; private set; }
    public float BetOdd { get; private set; }
    public int MaximumBetInstance => Math.Min(150, PlayerDenars);
    public int BettedDenars { get; private set; }
    public int OverallExpectedDenars { get; private set; }
    public int PlayerDenars => Hero.MainHero.Gold;
    private TournamentKB CurrentTKB { get; set; }
    private TeamTournamentMissionController MissionBehavior { get; set; }

    public TeamTournamentBehavior(TournamentGame tournamentGame, Settlement settlement, ITournamentGameBehavior gameBehavior, bool isPlayerParticipating)
    {
      Settlement = settlement;
      TournamentGame = tournamentGame;
      MissionBehavior = gameBehavior as TeamTournamentMissionController;
      CurrentTKB = TournamentKB.Current;
      Rounds = new TeamTournamentRound[CurrentTKB.Rounds];
      CreateTeams();
      CurrentRoundIndex = -1;
      LastMatch = null;
      Winner = null;
      IsPlayerParticipating = isPlayerParticipating;
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
      var heroesInSettlement = Settlement
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
      CurrentRoundIndex = 0;
      CreateTorunamentTree();
      CalculateBet();
      Utilities.SetDifficulty();
    }

    public override void OnMissionTick(float dt)
    {
      if (CurrentMatch != null && CurrentMatch.State == TournamentMatch.MatchState.Started && MissionBehavior.IsMatchEnded())
        EndCurrentMatch();
    }

    public void StartMatch()
    {
      if (CurrentMatch.IsPlayerParticipating)
      {
        Campaign.Current.TournamentManager.OnPlayerJoinMatch(TournamentGame.GetType());
      }
      CurrentMatch.Start();
      base.Mission.SetMissionMode(MissionMode.Tournament, true);
      MissionBehavior.StartMatch(CurrentMatch, NextRound == null);
      CampaignEventDispatcher.Instance.OnPlayerStartedTournamentMatch(Settlement.Town);
    }

    public void SkipMatch()
    {
      if (CurrentMatch.IsReady)
        CurrentMatch.Start();

      MissionBehavior.SkipMatch(CurrentMatch);
      EndCurrentMatch();
    }

    private void EndCurrentMatch()
    {
      LastMatch = CurrentMatch;
      CurrentRound.EndMatch();
      MissionBehavior.OnMatchEnded();

      // add winners to next round
      if (NextRound != null)
      {
        // fill in round
        LastMatch.Winners.ToList().ForEach(x => NextRound.AddTeam(x));
        MatchEnd?.Invoke(LastMatch);
      }

      // fire off events if player was disqualified 
      if (LastMatch.IsPlayerParticipating)
      {
        if (!LastMatch.IsPlayerTeamWinner)
          OnPlayerTeamEliminated();
        else
          OnPlayerTeamWinMatch();
      }

      if (CurrentRound.CurrentMatch == null) // done with this round
      {
        // check if done with Tournament or not
        if (CurrentRoundIndex < Rounds.Length - 1)
        {
          // not done yet, go to next round
          CurrentRoundIndex++;
          CalculateBet();
        }
        else
        {
          // done with Tournament
          CalculateBet();
          InformationManager.AddQuickInformation(new TextObject("{=tWzLqegB}Tournament is over.", null), 0, null, "");
          Winner = LastMatch.Winners?.FirstOrDefault()?.GetTeamLeader();
          if (Winner?.Character.IsHero ?? false)
          {
            if (Winner.Character == CharacterObject.PlayerCharacter)
              OnPlayerWinTournament();

            Campaign.Current.TournamentManager.AddLeaderboardEntry(Winner.Character.HeroObject);
          }
          Utilities.UnsetDifficulty();
          CampaignEventDispatcher.Instance.OnTournamentFinished(Winner?.Character, Settlement.Town);
          TournamentEnd?.Invoke();
        }
      }
    }

    private void OnPlayerTeamEliminated()
    {
      _playerLostAtRound = CurrentRoundIndex + 1;
      IsPlayerEliminated = true;
      BetOdd = 0f;
      if (BettedDenars > 0)
      {
        GiveGoldAction.ApplyForCharacterToSettlement(null, Settlement.CurrentSettlement, BettedDenars, false);
      }
      OverallExpectedDenars = 0;
      CampaignEventDispatcher.Instance.OnPlayerEliminatedFromTournament(CurrentRoundIndex, Settlement.Town);
    }

    private void OnPlayerTeamWinMatch() => Campaign.Current.TournamentManager.OnPlayerWinMatch(TournamentGame.GetType());

    private void OnPlayerWinTournament()
    {
      if (Campaign.Current.GameMode != CampaignGameMode.Campaign)
        return;

      GainRenownAction.Apply(Hero.MainHero, TournamentGame.TournamentWinRenown, false);

      if (Hero.MainHero.MapFaction.IsKingdomFaction && Hero.MainHero.MapFaction.Leader != Hero.MainHero)
        GainKingdomInfluenceAction.ApplyForDefault(Hero.MainHero, 1f);

      Hero.MainHero.PartyBelongedTo.ItemRoster.AddToCounts(TournamentGame.Prize, 1);

      if (OverallExpectedDenars > 0)
        GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, OverallExpectedDenars, false);

      Campaign.Current.TournamentManager.OnPlayerWinTournament(TournamentGame.GetType());
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
      _teams.ForEach(x => Rounds[0].AddTeam(x));
    }

    public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
    {
      canPlayerLeave = false;
      return null;
    }

    public void PlaceABet(int bet)
    {
      BettedDenars += bet;
      OverallExpectedDenars += GetExpectedDenarsForBet(bet);
      GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, bet, true);
    }

    public int GetExpectedDenarsForBet(int bet) => (int)(BetOdd * bet);

    public int GetMaximumBet()
    {
      if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
        return 150 * (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
      return 150;
    }

    // TODO: this needs "rework"
    private void CalculateBet()
    {
      if (IsPlayerParticipating)
      {
        if (CurrentRound.CurrentMatch == null)
        {
          BetOdd = 0f;
          return;
        }

        if (IsPlayerEliminated || !IsPlayerParticipating)
        {
          OverallExpectedDenars = 0;
          BetOdd = 0f;
          return;
        }
        // TODO: make a better bet odd calculation
        BetOdd = MBRandom.Random.Next(3, 5);
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
