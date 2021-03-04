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
      _tournamentKB = tkb;
      _maxSelectableTroopCount = _tournamentKB.TeamSize;
      InitializeAvailableSelection();
      RefreshValues();
      OnCurrentSelectedAmountChange();
    }

    public override void RefreshValues()
    {
      base.RefreshValues();
      TitleText = new TextObject("Select Team").ToString();
      DoneText = GameTexts.FindText("str_done").ToString();
      CancelText = GameTexts.FindText("str_cancel").ToString();
      CurrentSelectedAmountTitle = new TextObject("Selected Tournament Team").ToString();
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
      Troops = new MBBindingList<ManageHideoutTroopItemVM>();
      _currentTotalSelectedTroopCount = 0;
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

      foreach (var member in availableForSelection.GetTroopRoster())
      {
        var troopItemVM = new ManageHideoutTroopItemVM(member,
          new Action<ManageHideoutTroopItemVM>(OnAddCount),
          new Action<ManageHideoutTroopItemVM>(OnRemoveCount));

        troopItemVM.IsLocked = member.Character.IsPlayerCharacter || (member.Number - member.WoundedNumber <= 0);
        Troops.Add(troopItemVM);

        if (member.Character.IsPlayerCharacter)
        {
          troopItemVM.CurrentAmount = 1;
          _currentTotalSelectedTroopCount += 1;
        }
      }
    }

    private void OnRemoveCount(ManageHideoutTroopItemVM troopItem)
    {
      if (troopItem.CurrentAmount > 0)
      {
        troopItem.CurrentAmount--;
        _currentTotalSelectedTroopCount--;
      }
      OnCurrentSelectedAmountChange();
    }

    private void OnAddCount(ManageHideoutTroopItemVM troopItem)
    {
      if (troopItem.CurrentAmount < troopItem.MaxAmount && _currentTotalSelectedTroopCount < _maxSelectableTroopCount)
      {
        troopItem.CurrentAmount++;
        _currentTotalSelectedTroopCount++;
      }
      OnCurrentSelectedAmountChange();
    }

    private void OnCurrentSelectedAmountChange()
    {
      foreach (ManageHideoutTroopItemVM manageHideoutTroopItemVM in Troops)
      {
        manageHideoutTroopItemVM.IsRosterFull = _currentTotalSelectedTroopCount >= _maxSelectableTroopCount;
      }
      GameTexts.SetVariable("LEFT", _currentTotalSelectedTroopCount);
      GameTexts.SetVariable("RIGHT", _maxSelectableTroopCount);
      CurrentSelectedAmountText = GameTexts.FindText("str_LEFT_over_RIGHT_in_paranthesis", null).ToString();
    }

    #region DYN CALLED METHODS
#pragma warning disable IDE0051 // Remove unused private members
    /// <summary>
    /// DO NOT REMOVE THIS IS CALLED DYNAMICALLY
    /// </summary>
    private void ExecuteDone()
    {
      if (_currentTotalSelectedTroopCount != _tournamentKB.TeamSize)
      {
        InformationManager.DisplayMessage(new InformationMessage($"Selected team members count must be {_tournamentKB.TeamSize}."));
        return;
      }
      var troopRoster = new List<CharacterObject>();
      foreach (ManageHideoutTroopItemVM manageHideoutTroopItemVM in Troops)
      {
        if (manageHideoutTroopItemVM.CurrentAmount > 0)
        {
          for (var i = 0; i < manageHideoutTroopItemVM.CurrentAmount; i++)
            troopRoster.Add(manageHideoutTroopItemVM.Troop.Character);
        }
      }
      _tournamentKB.SelectedRoster = troopRoster;
      IsEnabled = false;
      GameMenu.SwitchToMenu("town");
      _tournamentKB.TournamentGame.PrepareForTournamentGame(true);
      Campaign.Current.TournamentManager.OnPlayerJoinTournament(_tournamentKB.TournamentGame.GetType(), Settlement.CurrentSettlement);
    }

    /// <summary>
    /// DO NOT REMOVE THIS IS CALLED DYNAMICALLY
    /// </summary>

    private void ExecuteCancel()

    {
      IsEnabled = false;
    }

    /// <summary>
    /// DO NOT REMOVE THIS IS CALLED DYNAMICALLY
    /// </summary>
    private void ExecuteReset()
    {
      InitializeAvailableSelection();
      _maxSelectableTroopCount = _tournamentKB.TeamSize;
      OnCurrentSelectedAmountChange();
    }
#pragma warning restore IDE0051 // Remove unused private members
    #endregion

    #region view properties

    [DataSourceProperty]
    public bool IsEnabled
    {
      get => _isEnabled;

      set
      {
        if (value != _isEnabled)
        {
          _isEnabled = value;
          OnPropertyChangedWithValue(value, "IsEnabled");
        }
      }
    }

    [DataSourceProperty]
    public MBBindingList<ManageHideoutTroopItemVM> Troops
    {
      get => _troops;

      set
      {
        if (value != _troops)
        {
          _troops = value;
          OnPropertyChangedWithValue(value, "Troops");
        }
      }
    }

    [DataSourceProperty]
    public string DoneText
    {
      get => _doneText;

      set
      {
        if (value != _doneText)
        {
          _doneText = value;
          OnPropertyChangedWithValue(value, "DoneText");
        }
      }
    }

    [DataSourceProperty]
    public string CancelText
    {
      get => _cancelText;

      set
      {
        if (value != _cancelText)
        {
          _cancelText = value;
          OnPropertyChangedWithValue(value, "CancelText");
        }
      }
    }

    [DataSourceProperty]
    public string TitleText
    {
      get => _titleText;

      set
      {
        if (value != _titleText)
        {
          _titleText = value;
          OnPropertyChangedWithValue(value, "TitleText");
        }
      }
    }

    [DataSourceProperty]
    public string CurrentSelectedAmountText
    {
      get => _currentSelectedAmountText;

      set
      {
        if (value != _currentSelectedAmountText)
        {
          _currentSelectedAmountText = value;
          OnPropertyChangedWithValue(value, "CurrentSelectedAmountText");
        }
      }
    }

    [DataSourceProperty]
    public string CurrentSelectedAmountTitle
    {
      get => _currentSelectedAmountTitle;

      set
      {
        if (value != _currentSelectedAmountTitle)
        {
          _currentSelectedAmountTitle = value;
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