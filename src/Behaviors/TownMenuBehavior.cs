using System;
using System.Collections.Generic;

using Helpers;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

using TournamentsEnhanced.Behaviors.Abstract;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Localization;
using TournamentsEnhanced.Wrappers.ObjectSystem;
using static TaleWorlds.CampaignSystem.GameMenus.GameMenuOption;

namespace TournamentsEnhanced.Behaviors
{
  class TownMenuBehavior : MBEncounterGameMenuBehavior
  {
    protected ModState ModState { get; set; } = ModState.Instance;
    protected TournamentBuilder TournamentBuilder { get; set; } = TournamentBuilder.Instance;
    protected Settings Settings { get; set; } = Settings.Instance;
    protected MBSettlement MBSettlement { get; set; } = MBSettlement.Instance;
    protected MBCampaign MBCampaign { get; set; } = MBCampaign.Instance;
    protected MBInformationManagerFacade MBInformationManagerFacade { get; set; } = MBInformationManagerFacade.Instance;


    public bool OnPrizeSelectMenuCondition(MenuCallbackArgs args)
    {
      bool shouldBeDisabled;
      MBTextObject disabledText;
      bool canPlayerDo = MBCampaign.CanMainHeroJoinTournamentAtCurrentSettlement(out shouldBeDisabled, out disabledText);
      args.optionLeaveType = LeaveType.Manage;
      return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
    }

    public void OnPrizeSelectMenuConsequence(MenuCallbackArgs args)
    {
      var prizeList = MBItemObject.GetAvailableTournamentPrizes();
      ShowPrizeSelectionMenu(prizeList, OnSelectPrize, OnDeSelectPrize);
      GameMenu.SwitchToMenu("town_arena");
    }

    public override void RegisterEvents()
    {
      CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
    }


    public void ShowPrizeSelectionMenu(List<MBItemObject> items,
                                                   Action<List<MBInquiryElement>> affirmativeAction,
                                                   Action<List<MBInquiryElement>> negativeAction)
    {
      var inquiryElements = CreateElementListFromItems(items);
      List<MBInquiryElement> selectedElements;
      bool isAffirmative;

      if (inquiryElements.Count > 0)
      {
        var inquiryData =
          new MBMultiSelectionInquiryData(
            "Prize Selection",
            "Pick a prize from the list below",
            inquiryElements,
            true,
            1,
            "OK",
            "Cancel",
            (List<InquiryElement> list) => OnAffirmativeAction(
              list,
              out isAffirmative,
              out selectedElements),
            (List<InquiryElement> list) => OnNegativeAction(
              list,
              out isAffirmative,
              out selectedElements));

        MBInformationManager.ShowMultiSelectionInquiry(inquiryData, true);
      }
      else
      {
        MBInformationManagerFacade.DisplayAsLogEntry("Error creating prize list");
      }
    }

    private static void OnAffirmativeAction(List<InquiryElement> list, out bool affirmativeResult, out List<MBInquiryElement> selectedInquiryElements)
    {
      affirmativeResult = true;
      selectedInquiryElements = list.CastList<MBInquiryElement>();
    }

    private static void OnNegativeAction(List<InquiryElement> list, out bool affirmativeResult, out List<MBInquiryElement> selectedInquiryElements)
    {
      affirmativeResult = false;
      selectedInquiryElements = list.CastList<MBInquiryElement>();
    }


    private static List<MBInquiryElement> CreateElementListFromItems(List<MBItemObject> itemList)
    {
      var inquiryElements = new List<MBInquiryElement>();

      foreach (var item in itemList)
      {
        inquiryElements.Add(CreateInquiryElementFromItem(item));
      }

      return inquiryElements;
    }

    private static InquiryElement CreateInquiryElementFromItem(MBItemObject item)
    {
      var itemModifier =
          string.IsNullOrWhiteSpace(item.StringId) ? null : MBMBObjectManager.GetObjectById<ItemModifier>(item.StringId);
      var equipmentElement = new EquipmentElement(item, itemModifier);
      var imageIdentifier = new ImageIdentifier(
          equipmentElement.Item.StringId,
          ImageIdentifierType.Item,
          equipmentElement.GetModifiedItemName().ToString());

      return new InquiryElement(
          equipmentElement.Item.StringId,
          equipmentElement.GetModifiedItemName().ToString(),
          imageIdentifier,
          true,
          equipmentElement.ToString()
      );
    }

    private void OnSelectPrize(List<MBInquiryElement> prizes)
    {
      if (prizes.Count > 0)
      {
        TournamentGame tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(MBSettlement.CurrentSettlement.Town);
        ItemObject prize = MBObjectManager.Instance.GetObject<ItemObject>(prizes.First().Identifier.ToString());
        typeof(TournamentGame).GetProperty("Prize").SetValue(tournamentGame, prize);
        GameMenu.SwitchToMenu("town_arena");
      }

    }

    private void OnDeSelectPrize(List<MBInquiryElement> prizeSelections)
    {

    }



    private bool OnHostTournamentMenuCondition(MenuCallbackArgs args)
    {
      args.optionLeaveType = GameMenuOption.LeaveType.Continue;
      return !Settlement.CurrentSettlement.Town.HasTournament &&
              Settlement.CurrentSettlement.IsTown &&
              Settlement.CurrentSettlement.OwnerClan.Leader.IsHumanPlayerCharacter &&
              ModState.DaysSince[TournamentType.PlayerInitiated] >= Settings.MinDaysBetweenHostedTournaments;
    }

    private void OnHostTournamentMenuConsequence(MenuCallbackArgs args)
    {
      TournamentBuilder.TryCreatePlayerInitiatedTournament();
      ModState.DaysSince[TournamentType.PlayerInitiated] = 0;
      GameMenu.ActivateGameMenu("town_arena");
    }

    private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
    {
      campaignGameStarter.AddGameMenuOption("town_arena", "host_tournament", "Host a tournament in your honor (" +
        Settings.TournamentCost.ToString() + "{GOLD_ICON})",
        new GameMenuOption.OnConditionDelegate(OnHostTournamentMenuCondition),
        new GameMenuOption.OnConsequenceDelegate(OnHostTournamentMenuConsequence), false, 1, false);

      campaignGameStarter.AddGameMenuOption("town_arena", "select_prize", "Select your prize",
        new GameMenuOption.OnConditionDelegate(OnPrizeSelectMenuCondition),
        new GameMenuOption.OnConsequenceDelegate(OnPrizeSelectMenuConsequence), false, 1, true);
    }
  }
}
