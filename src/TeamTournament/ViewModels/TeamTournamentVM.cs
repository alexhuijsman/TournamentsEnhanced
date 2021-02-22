using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.ViewModelCollection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace TournamentsEnhanced.TeamTournament.ViewModels
{
  public class TeamTournamentVM : ViewModel
  {
    public Action DisableUI { get; }
    public TeamTournamentBehavior Tournament { get; }

    public TeamTournamentVM(Action disableUI, TeamTournamentBehavior tournamentBehavior)
    {
      this.DisableUI = disableUI;
      this.CurrentMatch = new TeamTournamentMatchVM();

      this.Round1 = new TeamTournamentRoundVM();
      this.Round2 = new TeamTournamentRoundVM();
      this.Round3 = new TeamTournamentRoundVM();
      this.Round4 = new TeamTournamentRoundVM();

      this._rounds = new List<TeamTournamentRoundVM>
      {
        this.Round1,
        this.Round2,
        this.Round3,
        this.Round4
      };

      this._tournamentWinner = new TeamTournamentMemberVM();
      this.Tournament = tournamentBehavior;
      this.WinnerIntro = GameTexts.FindText("str_tournament_winner_intro", null).ToString();
      this.BattleRewards = new MBBindingList<TournamentRewardVM>();

      for (int i = 0; i < this.Tournament.Rounds.Length; i++)
        this._rounds[i].Initialize(this.Tournament.Rounds[i], GameTexts.FindText("str_tournament_round", i.ToString()));


      this.Refresh();

      this.Tournament.TournamentEnd += this.OnTournamentEnd;
      this.Tournament.MatchEnd += this.OnMatchEnd;

      this.PrizeVisual = (this.HasPrizeItem ? new ImageIdentifierVM(this.Tournament.TournamentGame.Prize) : new ImageIdentifierVM(ImageIdentifierType.Null));
      this.RefreshValues();
    }

    private void OnMatchEnd(TeamTournamentMatch match)
    {
      if (ActiveRoundIndex < this._rounds.Count + 1 && this.Tournament.NextRound != null)
      {
        this._rounds[ActiveRoundIndex + 1].Initialize(this.Tournament.NextRound);
        this.RefreshValues();
      }
    }

    public override void RefreshValues()
    {
      base.RefreshValues();

      this.LeaveText = GameTexts.FindText("str_tournament_leave", null).ToString();
      this.SkipRoundText = GameTexts.FindText("str_tournament_skip_round", null).ToString();
      this.WatchRoundText = GameTexts.FindText("str_tournament_watch_round", null).ToString();
      this.JoinTournamentText = GameTexts.FindText("str_tournament_join_tournament", null).ToString();
      this.BetText = GameTexts.FindText("str_bet", null).ToString();
      this.AcceptText = GameTexts.FindText("str_accept", null).ToString();
      this.CancelText = GameTexts.FindText("str_cancel", null).ToString();
      this.TournamentWinnerTitle = GameTexts.FindText("str_tournament_winner_title", null).ToString();
      this.BetTitleText = GameTexts.FindText("str_wager", null).ToString();
      GameTexts.SetVariable("MAX_AMOUNT", this.Tournament.GetMaximumBet());
      GameTexts.SetVariable("GOLD_ICON", "{=!}<img src=\"Icons\\Coin@2x\">");
      this.BetDescriptionText = GameTexts.FindText("str_tournament_bet_description", null).ToString();
      this.TournamentPrizeText = GameTexts.FindText("str_tournament_prize", null).ToString();
      this.PrizeItemName = this.Tournament.TournamentGame.Prize.Name.ToString();
      MBTextManager.SetTextVariable("SETTLEMENT_NAME", this.Tournament.Settlement.Name, false);
      this.TournamentTitle = GameTexts.FindText("str_tournament", null).ToString();
      this.CurrentWagerText = GameTexts.FindText("str_tournament_current_wager", null).ToString();

      if (this._round1 != null)
        this._round1.RefreshValues();
      if (this._round2 != null)
        this._round2.RefreshValues();
      if (_round3 != null)
        _round3.RefreshValues();
      if (this._round4 != null)
        this._round4.RefreshValues();
      if (this._currentMatch != null)
        this._currentMatch.RefreshValues();
      if (this._tournamentWinner != null)
        this._tournamentWinner.RefreshValues();
    }

    private void RefreshBetProperties()
    {
      TextObject textObject = new TextObject("{=L9GnQvsq}Stake: {BETTED_DENARS}", null);
      textObject.SetTextVariable("BETTED_DENARS", this.Tournament.BettedDenars);
      this.BettedDenarsText = textObject.ToString();
      TextObject textObject2 = new TextObject("{=xzzSaN4b}Expected: {OVERALL_EXPECTED_DENARS}", null);
      textObject2.SetTextVariable("OVERALL_EXPECTED_DENARS", this.Tournament.OverallExpectedDenars);
      this.OverallExpectedDenarsText = textObject2.ToString();
      TextObject textObject3 = new TextObject("{=yF5fpwNE}Total: {TOTAL}", null);
      textObject3.SetTextVariable("TOTAL", this.Tournament.PlayerDenars);
      this.TotalDenarsText = textObject3.ToString();
      OnPropertyChanged("IsBetButtonEnabled");
      this.MaximumBetValue = Math.Min(this.Tournament.GetMaximumBet() - this._thisRoundBettedAmount, Hero.MainHero.Gold);
      GameTexts.SetVariable("NORMALIZED_EXPECTED_GOLD", (int)(this.Tournament.BetOdd * 100f));
      GameTexts.SetVariable("GOLD_ICON", "{=!}<img src=\"Icons\\Coin@2x\">");
      this.BetOddsText = GameTexts.FindText("str_tournament_bet_odd", null).ToString();
    }

    private void OnNewRoundStarted(int prevRoundIndex, int currentRoundIndex)
    {
      this._isPlayerParticipating = this.Tournament.IsPlayerParticipating;
      this._thisRoundBettedAmount = 0;
    }

    public void Refresh()
    {
      this.IsCurrentMatchActive = false;
      CurrentMatch = _rounds[Tournament.CurrentRoundIndex].MatchVMs.FirstOrDefault(m => m.IsValid && m.Match == Tournament.CurrentMatch);
      this.ActiveRoundIndex = this.Tournament.CurrentRoundIndex;
      this.CanPlayerJoin = this.PlayerCanJoinMatch();
      OnPropertyChanged("IsTournamentIncomplete");
      OnPropertyChanged("InitializationOver");
      OnPropertyChanged("IsBetButtonEnabled");
      this.HasPrizeItem = (this.Tournament.TournamentGame.Prize != null && !this.IsOver);
    }

    /// <summary>
    /// TODO: make some better team winning interface
    ///       for now vanilla view with team-leader as winner
    /// </summary>
    private void OnTournamentEnd()
    {
      var winnerTeams = this.Tournament.LastMatch.Teams.OrderByDescending(x => x.Score).ToList();
      var firstTeamLeader = new TeamTournamentMemberVM(winnerTeams.ElementAt(0).GetTeamLeader());
      var secondTeamLeader = new TeamTournamentMemberVM(winnerTeams.ElementAt(1).GetTeamLeader());
      this.TournamentWinner = firstTeamLeader;

      if (this.TournamentWinner.IsMainHero)
      {
        GameTexts.SetVariable("TOURNAMENT_FINAL_OPPONENT", $"{(firstTeamLeader == this.TournamentWinner ? secondTeamLeader : firstTeamLeader).Name}'s Team");
        this.WinnerIntro = GameTexts.FindText("str_tournament_result_won", null).ToString();

        if (this.Tournament.TournamentGame.TournamentWinRenown > 0f)
        {
          GameTexts.SetVariable("RENOWN", this.Tournament.TournamentGame.TournamentWinRenown.ToString("F1"));
          this.BattleRewards.Add(new TournamentRewardVM(GameTexts.FindText("str_tournament_renown", null).ToString()));
        }

        if (this.Tournament.TournamentGame.Prize != null)
        {
          GameTexts.SetVariable("REWARD", this.Tournament.TournamentGame.Prize.Name.ToString());
          this.BattleRewards.Add(new TournamentRewardVM(GameTexts.FindText("str_tournament_reward", null).ToString(), new ImageIdentifierVM(this.Tournament.TournamentGame.Prize)));
        }

        if (this.Tournament.OverallExpectedDenars > 0)
        {
          GameTexts.SetVariable("BET", this.Tournament.OverallExpectedDenars.ToString());
          this.BattleRewards.Add(new TournamentRewardVM(GameTexts.FindText("str_tournament_bet", null).ToString()));
        }
      }
      else if (firstTeamLeader.IsMainHero || secondTeamLeader.IsMainHero)
      {
        GameTexts.SetVariable("TOURNAMENT_FINAL_OPPONENT", $"{(firstTeamLeader == this.TournamentWinner ? firstTeamLeader : secondTeamLeader).Name}'s Team");
        this.WinnerIntro = GameTexts.FindText("str_tournament_result_eliminated_at_final", null).ToString();
      }
      else
      {
        GameTexts.SetVariable("TOURNAMENT_FINAL_PARTICIPANT_A", $"{(firstTeamLeader == this.TournamentWinner ? firstTeamLeader : secondTeamLeader).Name}'s Team");
        GameTexts.SetVariable("TOURNAMENT_FINAL_PARTICIPANT_B", $"{(firstTeamLeader == this.TournamentWinner ? secondTeamLeader : firstTeamLeader).Name}'s Team");

        if (this._isPlayerParticipating)
        {
          GameTexts.SetVariable("TOURNAMENT_ELIMINATED_ROUND", this.Tournament.PlayerTeamLostAtRound + 1);
          this.WinnerIntro = GameTexts.FindText("str_tournament_result_eliminated", null).ToString();
        }
        else
          this.WinnerIntro = GameTexts.FindText("str_tournament_result_spectator", null).ToString();
      }
      this.IsOver = true;
    }

    private bool PlayerCanJoinMatch()
    {
      if (this.IsTournamentIncomplete)
        return this.Tournament.CurrentMatch.IsPlayerParticipating;

      return false;
    }

    public void OnAgentRemoved(Agent agent)
    {
      if (this.IsCurrentMatchActive && agent.IsHuman)
      {
        var member = GetMemberForSeed(agent.Origin.UniqueSeed);
        if (member != null)
          member.IsDead = true;
      }
    }

    private TeamTournamentMemberVM GetMemberForSeed(int seed)
    {
      return this.CurrentMatch.GetMatchMemberVMs().FirstOrDefault(x => x.Member != null && x.Member.Descriptor.CompareTo(seed) == 0);
    }

    #region view commands
#pragma warning disable IDE0051 // Remove unused private members
    /// <summary>
    /// DO NOT REMOVE
    /// </summary>
    private void ExecuteShowPrizeItemTooltip()

    {
      if (this.HasPrizeItem)
      {
        InformationManager.AddTooltipInformation(typeof(ItemObject), new object[]
        {
          new EquipmentElement(this.Tournament.TournamentGame.Prize, null)
        });
      }
    }

    /// <summary>
    /// DO NOT REMOVE
    /// </summary>
    private void ExecuteHidePrizeItemTooltip()
    {
      InformationManager.HideInformations();
    }

    /// <summary>
    /// DO NOT REMOVE
    /// </summary>
    private void ExecuteBet()
    {
      this._thisRoundBettedAmount += this.WageredDenars;
      this.Tournament.PlaceABet(this.WageredDenars);
      this.RefreshBetProperties();
    }

    /// <summary>
    /// DO NOT REMOVE
    /// </summary>
    private void ExecuteJoinTournament()
    {
      if (this.PlayerCanJoinMatch())
      {
        this.Tournament.StartMatch();
        this.IsCurrentMatchActive = true;
        this.CurrentMatch.Refresh(true);
        this.CurrentMatch.State = 3;
        this.DisableUI();
        this.IsCurrentMatchActive = true;
      }
    }

    /// <summary>
    /// DO NOT REMOVE
    /// </summary>
    private void ExecuteSkipRound()
    {
      if (this.IsTournamentIncomplete)
      {
        this.Tournament.SkipMatch();
      }
      this.Refresh();
    }

    /// <summary>
    /// DO NOT REMOVE
    /// </summary>
    private void ExecuteWatchRound()
    {
      if (!this.PlayerCanJoinMatch())
      {
        this.Tournament.StartMatch();
        this.IsCurrentMatchActive = true;
        this.CurrentMatch.Refresh(true);
        this.CurrentMatch.State = 3;
        this.DisableUI();
        this.IsCurrentMatchActive = true;
      }
    }

    /// <summary>
    /// DO NOT REMOVE
    /// </summary>
    private void ExecuteLeave()
    {
      if (this.CurrentMatch != null)
      {
        InformationManager.ShowInquiry(new InquiryData(
          GameTexts.FindText("str_forfeit", null).ToString(),
          new TextObject("Are you sure?").ToString(),
          true,
          true,
          GameTexts.FindText("str_yes", null).ToString(),
          GameTexts.FindText("str_no", null).ToString(),
          this.ExitFinishTournament,
          null),
        true);
      }
      Mission.Current.EndMission();
    }

    private void ExitFinishTournament()
    {
      Campaign.Current.TournamentManager.ResolveTournament(this.Tournament.TournamentGame, Settlement.CurrentSettlement.Town);
      Mission.Current.EndMission();
    }

#pragma warning restore IDE0051 // Remove unused private members
    #endregion

    #region view properties
    [DataSourceProperty]
    public string TournamentWinnerTitle
    {
      get => this._tournamentWinnerTitle;
      set
      {
        if (value != this._tournamentWinnerTitle)
        {
          this._tournamentWinnerTitle = value;
          OnPropertyChangedWithValue(value, "TournamentWinnerTitle");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMemberVM TournamentWinner
    {
      get => this._tournamentWinner;
      set
      {
        if (value != this._tournamentWinner)
        {
          this._tournamentWinner = value;
          OnPropertyChangedWithValue(value, "TournamentWinner");
        }
      }
    }

    [DataSourceProperty]
    public int MaximumBetValue
    {
      get => this._maximumBetValue;
      set
      {
        if (value != this._maximumBetValue)
        {
          this._maximumBetValue = value;
          OnPropertyChangedWithValue(value, "MaximumBetValue");
          this._wageredDenars = -1;
          this.WageredDenars = 0;
        }
      }
    }

    [DataSourceProperty]
    public bool IsBetButtonEnabled => this.PlayerCanJoinMatch() && this.Tournament.GetMaximumBet() > this._thisRoundBettedAmount && Hero.MainHero.Gold > 0;

    [DataSourceProperty]
    public string BetText
    {
      get => this._betText;
      set
      {
        if (value != this._betText)
        {
          this._betText = value;
          OnPropertyChangedWithValue(value, "BetText");
        }
      }
    }

    [DataSourceProperty]
    public string BetTitleText
    {
      get => this._betTitleText;
      set
      {
        if (value != this._betTitleText)
        {
          this._betTitleText = value;
          OnPropertyChangedWithValue(value, "BetTitleText");
        }
      }
    }

    [DataSourceProperty]
    public string CurrentWagerText
    {
      get => this._currentWagerText;
      set
      {
        if (value != this._currentWagerText)
        {
          this._currentWagerText = value;
          OnPropertyChangedWithValue(value, "CurrentWagerText");
        }
      }
    }

    [DataSourceProperty]
    public string BetDescriptionText
    {
      get => this._betDescriptionText;
      set
      {
        if (value != this._betDescriptionText)
        {
          this._betDescriptionText = value;
          OnPropertyChangedWithValue(value, "BetDescriptionText");
        }
      }
    }

    [DataSourceProperty]
    public ImageIdentifierVM PrizeVisual
    {
      get => this._prizeVisual;
      set
      {
        if (value != this._prizeVisual)
        {
          this._prizeVisual = value;
          OnPropertyChangedWithValue(value, "PrizeVisual");
        }
      }
    }

    [DataSourceProperty]
    public string PrizeItemName
    {
      get => this._prizeItemName;
      set
      {
        if (value != this._prizeItemName)
        {
          this._prizeItemName = value;
          OnPropertyChangedWithValue(value, "PrizeItemName");
        }
      }
    }

    [DataSourceProperty]
    public string TournamentPrizeText
    {
      get => this._tournamentPrizeText;
      set
      {
        if (value != this._tournamentPrizeText)
        {
          this._tournamentPrizeText = value;
          OnPropertyChangedWithValue(value, "TournamentPrizeText");
        }
      }
    }

    [DataSourceProperty]
    public int WageredDenars
    {
      get => this._wageredDenars;
      set
      {
        if (value != this._wageredDenars)
        {
          this._wageredDenars = value;
          OnPropertyChangedWithValue(value, "WageredDenars");
          this.ExpectedBetDenars = ((this._wageredDenars == 0) ? 0 : this.Tournament.GetExpectedDenarsForBet(this._wageredDenars));
        }
      }
    }

    [DataSourceProperty]
    public int ExpectedBetDenars
    {
      get => this._expectedBetDenars;
      set
      {
        if (value != this._expectedBetDenars)
        {
          this._expectedBetDenars = value;
          OnPropertyChangedWithValue(value, "ExpectedBetDenars");
        }
      }
    }

    [DataSourceProperty]
    public string BetOddsText
    {
      get => this._betOddsText;
      set
      {
        if (value != this._betOddsText)
        {
          this._betOddsText = value;
          OnPropertyChangedWithValue(value, "BetOddsText");
        }
      }
    }

    [DataSourceProperty]
    public string BettedDenarsText
    {
      get => this._bettedDenarsText;
      set
      {
        if (value != this._bettedDenarsText)
        {
          this._bettedDenarsText = value;
          OnPropertyChangedWithValue(value, "BettedDenarsText");
        }
      }
    }

    [DataSourceProperty]
    public string OverallExpectedDenarsText
    {
      get => this._overallExpectedDenarsText;
      set
      {
        if (value != this._overallExpectedDenarsText)
        {
          this._overallExpectedDenarsText = value;
          OnPropertyChangedWithValue(value, "OverallExpectedDenarsText");
        }
      }
    }

    [DataSourceProperty]
    public string CurrentExpectedDenarsText
    {
      get => this._currentExpectedDenarsText;
      set
      {
        if (value != this._currentExpectedDenarsText)
        {
          this._currentExpectedDenarsText = value;
          OnPropertyChangedWithValue(value, "CurrentExpectedDenarsText");
        }
      }
    }

    [DataSourceProperty]
    public string TotalDenarsText
    {
      get => this._totalDenarsText;
      set
      {
        if (value != this._totalDenarsText)
        {
          this._totalDenarsText = value;
          OnPropertyChangedWithValue(value, "TotalDenarsText");
        }
      }
    }

    [DataSourceProperty]
    public string AcceptText
    {
      get => this._acceptText;
      set
      {
        if (value != this._acceptText)
        {
          this._acceptText = value;
          OnPropertyChangedWithValue(value, "AcceptText");
        }
      }
    }

    [DataSourceProperty]
    public string CancelText
    {
      get => this._cancelText;
      set
      {
        if (value != this._cancelText)
        {
          this._cancelText = value;
          OnPropertyChangedWithValue(value, "CancelText");
        }
      }
    }

    [DataSourceProperty]
    public bool IsCurrentMatchActive
    {
      get => this._isCurrentMatchActive;
      set
      {
        this._isCurrentMatchActive = value;
        OnPropertyChangedWithValue(value, "IsCurrentMatchActive");
      }
    }

    [DataSourceProperty]
    public TeamTournamentMatchVM CurrentMatch
    {
      get => this._currentMatch;
      set
      {
        if (value != this._currentMatch)
        {
          if (this._currentMatch != null && this._currentMatch.IsValid)
          {
            this._currentMatch.State = 2;
            this._currentMatch.Refresh(false);

            int index = this._rounds.FindIndex(r => r.MatchVMs.Any(m => m.Match == this.Tournament.LastMatch));

            if (index < this.Tournament.Rounds.Length - 1)
              this._rounds[index + 1].Initialize();
          }

          this._currentMatch = value;
          OnPropertyChangedWithValue(value, "CurrentMatch");

          if (this._currentMatch != null)
            this._currentMatch.State = 1;
        }
      }
    }

    [DataSourceProperty]
    public bool IsTournamentIncomplete => this.Tournament == null || this.Tournament.CurrentMatch != null;

    [DataSourceProperty]
    public int ActiveRoundIndex
    {
      get => this._activeRoundIndex;
      set
      {
        if (value != this._activeRoundIndex)
        {
          this.OnNewRoundStarted(this._activeRoundIndex, value);
          this._activeRoundIndex = value;
          OnPropertyChangedWithValue(value, "ActiveRoundIndex");
          this.RefreshBetProperties();
        }
      }
    }

    [DataSourceProperty]
    public bool CanPlayerJoin
    {
      get => this._canPlayerJoin;
      set
      {
        if (value != this._canPlayerJoin)
        {
          this._canPlayerJoin = value;
          OnPropertyChangedWithValue(value, "CanPlayerJoin");
        }
      }
    }

    [DataSourceProperty]
    public bool HasPrizeItem
    {
      get => this._hasPrizeItem;
      set
      {
        if (value != this._hasPrizeItem)
        {
          this._hasPrizeItem = value;
          OnPropertyChangedWithValue(value, "HasPrizeItem");
        }
      }
    }

    [DataSourceProperty]
    public string JoinTournamentText
    {
      get => this._joinTournamentText;
      set
      {
        if (value != this._joinTournamentText)
        {
          this._joinTournamentText = value;
          OnPropertyChangedWithValue(value, "JoinTournamentText");
        }
      }
    }

    [DataSourceProperty]
    public string SkipRoundText
    {
      get => this._skipRoundText;
      set
      {
        if (value != this._skipRoundText)
        {
          this._skipRoundText = value;
          OnPropertyChangedWithValue(value, "SkipRoundText");
        }
      }
    }

    [DataSourceProperty]
    public string WatchRoundText
    {
      get => this._watchRoundText;
      set
      {
        if (value != this._watchRoundText)
        {
          this._watchRoundText = value;
          OnPropertyChangedWithValue(value, "WatchRoundText");
        }
      }
    }

    [DataSourceProperty]
    public string LeaveText
    {
      get => this._leaveText;
      set
      {
        if (value != this._leaveText)
        {
          this._leaveText = value;
          OnPropertyChangedWithValue(value, "LeaveText");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentRoundVM Round1
    {
      get => this._round1;
      set
      {
        if (value != this._round1)
        {
          this._round1 = value;
          OnPropertyChangedWithValue(value, "Round1");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentRoundVM Round2
    {
      get => this._round2;
      set
      {
        if (value != this._round2)
        {
          this._round2 = value;
          OnPropertyChangedWithValue(value, "Round2");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentRoundVM Round3
    {
      get => this._round3;
      set
      {
        if (value != this._round3)
        {
          this._round3 = value;
          OnPropertyChangedWithValue(value, "Round3");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentRoundVM Round4
    {
      get
      {
        return this._round4;
      }
      set
      {
        if (value != this._round4)
        {
          this._round4 = value;
          OnPropertyChangedWithValue(value, "Round4");
        }
      }
    }

    [DataSourceProperty]
    public bool InitializationOver
    {
      get
      {
        return true;
      }
    }

    [DataSourceProperty]
    public string TournamentTitle
    {
      get => this._tournamentTitle;
      set
      {
        if (value != this._tournamentTitle)
        {
          this._tournamentTitle = value;
          OnPropertyChangedWithValue(value, "TournamentTitle");
        }
      }
    }

    [DataSourceProperty]
    public bool IsOver
    {
      get => this._isOver;
      set
      {
        if (this._isOver != value)
        {
          this._isOver = value;
          OnPropertyChangedWithValue(value, "IsOver");
        }
      }
    }

    [DataSourceProperty]
    public string WinnerIntro
    {
      get => this._winnerIntro;
      set
      {
        if (value != this._winnerIntro)
        {
          this._winnerIntro = value;
          OnPropertyChangedWithValue(value, "WinnerIntro");
        }
      }
    }

    [DataSourceProperty]
    public MBBindingList<TournamentRewardVM> BattleRewards
    {
      get => this._battleRewards;
      set
      {
        if (value != this._battleRewards)
        {
          this._battleRewards = value;
          OnPropertyChangedWithValue(value, "BattleRewards");
        }
      }
    }
    #endregion view properties

    private readonly List<TeamTournamentRoundVM> _rounds;
    private int _thisRoundBettedAmount;
    private bool _isPlayerParticipating;
    private TeamTournamentRoundVM _round1;
    private TeamTournamentRoundVM _round2;
    private TeamTournamentRoundVM _round3;
    private TeamTournamentRoundVM _round4;
    private int _activeRoundIndex = -1;
    private string _joinTournamentText;
    private string _skipRoundText;
    private string _watchRoundText;
    private string _leaveText;
    private bool _canPlayerJoin;
    private TeamTournamentMatchVM _currentMatch;
    private bool _isCurrentMatchActive;
    private string _betTitleText;
    private string _betDescriptionText;
    private string _betOddsText;
    private string _bettedDenarsText;
    private string _overallExpectedDenarsText;
    private string _currentExpectedDenarsText;
    private string _totalDenarsText;
    private string _acceptText;
    private string _cancelText;
    private string _prizeItemName;
    private string _tournamentPrizeText;
    private string _currentWagerText;
    private int _wageredDenars = -1;
    private int _expectedBetDenars = -1;
    private string _betText;
    private int _maximumBetValue;
    private string _tournamentWinnerTitle;
    private TeamTournamentMemberVM _tournamentWinner;
    private string _tournamentTitle;
    private bool _isOver;
    private bool _hasPrizeItem;
    private string _winnerIntro;
    private ImageIdentifierVM _prizeVisual;
    private MBBindingList<TournamentRewardVM> _battleRewards;
  }
}
