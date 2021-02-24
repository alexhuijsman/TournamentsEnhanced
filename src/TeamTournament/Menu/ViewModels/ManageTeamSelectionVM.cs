using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.ManageHideoutTroops;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TournamentsEnhanced.TeamTournament.Menu.ViewModels
{
  internal class ManageTeamSelectionVM : ViewModel
  {
    public ManageTeamSelectionVM(TournamentKB tkb)
    {
      this._tournamentKB = tkb;
      this._maxSelectableTroopCount = _tournamentKB.TeamSize;
      this.InitializeAvailableSelection();
      this.RefreshValues();
      this.OnCurrentSelectedAmountChange();
    }

    public override void RefreshValues()
    {
      base.RefreshValues();
      this.TitleText = new TextObject("Select Team").ToString();
      this.DoneText = GameTexts.FindText("str_done").ToString();
      this.CancelText = GameTexts.FindText("str_cancel").ToString();
      this.CurrentSelectedAmountTitle = new TextObject("Selected Tournament Team").ToString();
    }

    private bool CanBeSelectedByHero(CharacterObject character)
    {
      var isSameSettlement = character.HeroObject.CurrentSettlement == Hero.MainHero.CurrentSettlement;
      if (!isSameSettlement) return false;

      var isSameClan = Hero.MainHero.Clan == character.HeroObject.Clan;
      if (isSameClan) return true;

      var isSameAlliance = Hero.MainHero.MapFaction == character.HeroObject.MapFaction;
      if (isSameAlliance) return true;

      return Hero.MainHero.AllLivingRelatedHeroes().Contains(character.HeroObject);
    }

    private void InitializeAvailableSelection()
    {
      this.Troops = new MBBindingList<ManageHideoutTroopItemVM>();
      this._currentTotalSelectedTroopCount = 0;
      var availableForSelection = TroopRoster.CreateDummyTroopRoster();

      // we add everyone is settlement that is relevant to the selection list
      var selectableChars = Settlement.CurrentSettlement
        .GetCombatantHeroesInSettlement()
        .Where(x => CanBeSelectedByHero(x));

      var flattenTroopRoster = new FlattenedTroopRoster(0);

      // add the main hero at the top
      flattenTroopRoster.Add(Hero.MainHero.CharacterObject, 1, 0);

      foreach (var character in selectableChars)
      {
        flattenTroopRoster.Add(character, 1, 0);
      }
      availableForSelection.Add(flattenTroopRoster); // add every other hero afterwards

      // now also add own trops in party roster
      var partyTroops = MobileParty.MainParty.MemberRoster.ToFlattenedRoster().Where(x => !x.Troop.IsHero);
      availableForSelection.Add(partyTroops);

      var availableForSelectionEnumerator = availableForSelection.GetEnumerator();
      TroopRosterElement member;
      while (availableForSelectionEnumerator.MoveNext())
      {
        member = availableForSelectionEnumerator.Current;
        var troopItemVM = new ManageHideoutTroopItemVM(member,
          new Action<ManageHideoutTroopItemVM>(this.OnAddCount),
          new Action<ManageHideoutTroopItemVM>(this.OnRemoveCount));

        troopItemVM.IsLocked = member.Character.IsPlayerCharacter || (member.Number - member.WoundedNumber <= 0);
        this.Troops.Add(troopItemVM);

        if (member.Character.IsPlayerCharacter)
        {
          troopItemVM.CurrentAmount = 1;
          this._currentTotalSelectedTroopCount += 1;
        }
      }
    }

    private void OnRemoveCount(ManageHideoutTroopItemVM troopItem)
    {
      if (troopItem.CurrentAmount > 0)
      {
        troopItem.CurrentAmount--;
        this._currentTotalSelectedTroopCount--;
      }
      this.OnCurrentSelectedAmountChange();
    }

    private void OnAddCount(ManageHideoutTroopItemVM troopItem)
    {
      if (troopItem.CurrentAmount < troopItem.MaxAmount && this._currentTotalSelectedTroopCount < this._maxSelectableTroopCount)
      {
        troopItem.CurrentAmount++;
        this._currentTotalSelectedTroopCount++;
      }
      this.OnCurrentSelectedAmountChange();
    }

    private void OnCurrentSelectedAmountChange()
    {
      foreach (ManageHideoutTroopItemVM manageHideoutTroopItemVM in this.Troops)
      {
        manageHideoutTroopItemVM.IsRosterFull = this._currentTotalSelectedTroopCount >= this._maxSelectableTroopCount;
      }
      GameTexts.SetVariable("LEFT", this._currentTotalSelectedTroopCount);
      GameTexts.SetVariable("RIGHT", this._maxSelectableTroopCount);
      this.CurrentSelectedAmountText = GameTexts.FindText("str_LEFT_over_RIGHT_in_paranthesis", null).ToString();
    }

    #region DYN CALLED METHODS
#pragma warning disable IDE0051 // Remove unused private members
    /// <summary>
    /// DO NOT REMOVE THIS IS CALLED DYNAMICALLY
    /// </summary>
    private void ExecuteDone()
    {
      if (this._currentTotalSelectedTroopCount != this._tournamentKB.TeamSize)
      {
        InformationManager.DisplayMessage(new InformationMessage($"Selected team members count must be {this._tournamentKB.TeamSize}."));
        return;
      }
      var troopRoster = new List<CharacterObject>();
      foreach (ManageHideoutTroopItemVM manageHideoutTroopItemVM in this.Troops)
      {
        if (manageHideoutTroopItemVM.CurrentAmount > 0)
        {
          for (var i = 0; i < manageHideoutTroopItemVM.CurrentAmount; i++)
            troopRoster.Add(manageHideoutTroopItemVM.Troop.Character);
        }
      }
      this._tournamentKB.SelectedRoster = troopRoster;
      this.IsEnabled = false;
      GameMenu.SwitchToMenu("town");
      this._tournamentKB.TournamentGame.PrepareForTournamentGame(true);
      Campaign.Current.TournamentManager.OnPlayerJoinTournament(this._tournamentKB.TournamentGame.GetType(), Settlement.CurrentSettlement);
    }

    /// <summary>
    /// DO NOT REMOVE THIS IS CALLED DYNAMICALLY
    /// </summary>

    private void ExecuteCancel()

    {
      this.IsEnabled = false;
    }

    /// <summary>
    /// DO NOT REMOVE THIS IS CALLED DYNAMICALLY
    /// </summary>
    private void ExecuteReset()
    {
      this.InitializeAvailableSelection();
      this._maxSelectableTroopCount = _tournamentKB.TeamSize;
      this.OnCurrentSelectedAmountChange();
    }
#pragma warning restore IDE0051 // Remove unused private members
    #endregion

    #region view properties

    [DataSourceProperty]
    public bool IsEnabled
    {
      get => this._isEnabled;

      set
      {
        if (value != this._isEnabled)
        {
          this._isEnabled = value;
          OnPropertyChangedWithValue(value, "IsEnabled");
        }
      }
    }

    [DataSourceProperty]
    public MBBindingList<ManageHideoutTroopItemVM> Troops
    {
      get => this._troops;

      set
      {
        if (value != this._troops)
        {
          this._troops = value;
          OnPropertyChangedWithValue(value, "Troops");
        }
      }
    }

    [DataSourceProperty]
    public string DoneText
    {
      get => this._doneText;

      set
      {
        if (value != this._doneText)
        {
          this._doneText = value;
          OnPropertyChangedWithValue(value, "DoneText");
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
    public string TitleText
    {
      get => this._titleText;

      set
      {
        if (value != this._titleText)
        {
          this._titleText = value;
          OnPropertyChangedWithValue(value, "TitleText");
        }
      }
    }

    [DataSourceProperty]
    public string CurrentSelectedAmountText
    {
      get => this._currentSelectedAmountText;

      set
      {
        if (value != this._currentSelectedAmountText)
        {
          this._currentSelectedAmountText = value;
          OnPropertyChangedWithValue(value, "CurrentSelectedAmountText");
        }
      }
    }

    [DataSourceProperty]
    public string CurrentSelectedAmountTitle
    {
      get => this._currentSelectedAmountTitle;

      set
      {
        if (value != this._currentSelectedAmountTitle)
        {
          this._currentSelectedAmountTitle = value;
          OnPropertyChangedWithValue(value, "CurrentSelectedAmountTitle");
        }
      }
    }
    #endregion

    private TournamentKB _tournamentKB;
    private int _maxSelectableTroopCount;
    private int _currentTotalSelectedTroopCount;
    private bool _isEnabled;
    private string _doneText;
    private string _cancelText;
    private string _titleText;
    private string _currentSelectedAmountText;
    private string _currentSelectedAmountTitle;
    private MBBindingList<ManageHideoutTroopItemVM> _troops;
  }

}