using Helpers;
using SandBox.View.Menu;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TournamentsEnhanced.extensions;

namespace TournamentsEnhanced.TeamTournament.Menu
{
  internal class TeamTournamentTeamSelectionMenu
  {
    public bool GameMenuSelectRosterCondition(MenuCallbackArgs args)
    {
      bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(
          Settlement.CurrentSettlement,
          SettlementAccessModel.SettlementAction.JoinTournament,
          out bool shouldBeDisabled,
          out TextObject disabledText);

      args.optionLeaveType = GameMenuOption.LeaveType.ManageHideoutTroops;

      // if this is a team tournament, activate
      shouldBeDisabled &= TournamentKB.Current != null;
      canPlayerDo &= TournamentKB.Current != null;

      if (shouldBeDisabled || disabledText.ToString().IsStringNoneOrEmpty())
        disabledText = new TextObject($"Roster can only be selected for team tournaments.");

      return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
    }

    public void GameMenuSelectRosterConsequence(MenuCallbackArgs args)
    {
      var menuViewContext = args.MenuContext.Handler as MenuViewContext;
      if (menuViewContext != null)
        HandleRosterSelection(menuViewContext);
    }

    private void HandleRosterSelection(MenuViewContext menuViewContext)
    {
      var manageTroopsMenu = menuViewContext.AddMenuView<MenuManageTeamSelection>(new object[]
      {
          TournamentKB.Current
      });

      // override _menuManageHideoutTroops inside MenuViewContext to fake that we opened it
      // needed later for closing it
      // reset inside MenuViewContext::CloseManageHideoutTroops
      menuViewContext.SetPrivateFieldValue("_menuManageHideoutTroops", manageTroopsMenu);
    }
  }
}