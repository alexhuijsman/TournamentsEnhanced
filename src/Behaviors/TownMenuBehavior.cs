using System;

using Helpers;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

using TournamentsEnhanced.Behaviors.Abstract;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Localization;

using static TaleWorlds.CampaignSystem.GameMenus.GameMenuOption;

namespace TournamentsEnhanced.Behaviors
{
  class TownMenuBehavior : MBEncounterGameMenuBehavior
  {
    public static bool PrizeSelectCondition(MenuCallbackArgs args)
    {
      bool shouldBeDisabled;
      MBTextObject disabledText;
      bool canPlayerDo = MBCampaign.CanMainHeroJoinTournamentAtCurrentSettlement(out shouldBeDisabled, out disabledText);
      args.optionLeaveType = LeaveType.Manage;
      return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
    }

    public static void PrizeSelectConsequence(MenuCallbackArgs args)
    {
      var prizeList = MBItemObject.GetAvailableTournamentPrizes();
      MBInformationManager.ShowPrizeSelectionMenu(prizeList, OnSelectPrize, OnDeSelectPrize);
      GameMenu.SwitchToMenu("town_arena");
    }

    public override void RegisterEvents()
    {
      CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
    }

    private static void OnSelectPrize(MBInquiryElementList prizes)
    {
      if (prizes.Count > 0)
      {
        TournamentGame tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(MBSettlement.CurrentTown);
        ItemObject prize = MBObjectManager.Instance.GetObject<ItemObject>(prizes.First().Identifier.ToString());
        typeof(TournamentGame).GetProperty("Prize").SetValue(tournamentGame, prize);
        GameMenu.SwitchToMenu("town_arena");
      }

    }

    private static void OnDeSelectPrize(MBInquiryElementList prizeSelections)
    {

    }



    private static bool game_menu_town_arena_host_tournament_condition(MenuCallbackArgs args)
    {
      args.optionLeaveType = GameMenuOption.LeaveType.Continue;
      return !Settlement.CurrentSettlement.Town.HasTournament &&
              Settlement.CurrentSettlement.IsTown &&
              Settlement.CurrentSettlement.OwnerClan.Leader.IsHumanPlayerCharacter &&
              ModState.WeeksSinceHostedTournament >= 1;
    }

    private static void game_menu_town_arena_host_tournament_consequence(MenuCallbackArgs args)
    {
      TournamentBuilder.TryCreatePeaceTournamentForFaction();
      ModState.WeeksSinceHostedTournament = 0;
      GameMenu.ActivateGameMenu("town_arena");
    }

    private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
    {
      campaignGameStarter.AddGameMenuOption("town_arena", "host_tournament", "Host a tournament in your honor (" +
        Settings.Instance.TournamentCost.ToString() + "{GOLD_ICON})",
        new GameMenuOption.OnConditionDelegate(game_menu_town_arena_host_tournament_condition),
        new GameMenuOption.OnConsequenceDelegate(game_menu_town_arena_host_tournament_consequence), false, 1, false);

      campaignGameStarter.AddGameMenuOption("town_arena", "select_prize", "Select your prize",
        new GameMenuOption.OnConditionDelegate(TownMenuBehavior.PrizeSelectCondition),
        new GameMenuOption.OnConsequenceDelegate(TownMenuBehavior.PrizeSelectConsequence), false, 1, true);
    }
  }
}
