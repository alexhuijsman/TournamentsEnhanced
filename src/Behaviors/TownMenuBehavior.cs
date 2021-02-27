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
      MBInformationManager.ShowPrizeSelectionMenu(prizeList, OnSelectPrize, OnDeSelectPrize);
      GameMenu.SwitchToMenu("town_arena");
    }

    public override void RegisterEvents()
    {
      CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
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
