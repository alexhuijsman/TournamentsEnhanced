using SandBox.View.Map;
using SandBox.View.Menu;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Missions;
using TournamentsEnhanced.TeamTournament.Menu.ViewModels;

namespace TournamentsEnhanced.TeamTournament.Menu
{
  [OverrideView(typeof(MenuManageTeamSelection))]
  public class GauntletMenuManageTeamSelection : MenuView
  {
    public GauntletMenuManageTeamSelection(TournamentKB tkb)
    {
      this._tournamentKB = tkb;
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this._dataSource = new ManageTeamSelectionVM(this._tournamentKB)
      {
        IsEnabled = true
      };
      this._gauntletLayer = new GauntletLayer(205, "GauntletLayer")
      {
        Name = "MenuManageHideoutTroops"
      };
      this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
      this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
      this._movie = this._gauntletLayer.LoadMovie("GameMenuManageHideoutTroops", this._dataSource);
      this._gauntletLayer.IsFocusLayer = true;
      ScreenManager.TrySetFocus(this._gauntletLayer);
      base.MenuViewContext.AddLayer(this._gauntletLayer);
      MapScreen mapScreen = ScreenManager.TopScreen as MapScreen;
      if (mapScreen != null)
        mapScreen.IsInHideoutTroopManage = true;
    }

    protected override void OnFinalize()
    {
      this._gauntletLayer.IsFocusLayer = false;
      ScreenManager.TryLoseFocus(this._gauntletLayer);
      this._dataSource.OnFinalize();
      this._dataSource = null;
      this._gauntletLayer.ReleaseMovie(this._movie);
      base.MenuViewContext.RemoveLayer(this._gauntletLayer);
      this._movie = null;
      this._gauntletLayer = null;
      MapScreen mapScreen = ScreenManager.TopScreen as MapScreen;
      if (mapScreen != null)
        mapScreen.IsInHideoutTroopManage = false;

      base.OnFinalize();
    }

    protected override void OnFrameTick(float dt)
    {
      base.OnFrameTick(dt);
      if (this._gauntletLayer.Input.IsHotKeyPressed("Exit"))
      {
        this._dataSource.IsEnabled = false;
      }
      if (!this._dataSource.IsEnabled)
      {
        base.MenuViewContext.CloseManageHideoutTroops();
      }
    }

    private readonly TournamentKB _tournamentKB;
    private GauntletLayer _gauntletLayer;
    private ManageTeamSelectionVM _dataSource;
    private GauntletMovie _movie;
  }

  // Gauntlet Dummy
  public class MenuManageTeamSelection : MenuView
  {
  }
}
