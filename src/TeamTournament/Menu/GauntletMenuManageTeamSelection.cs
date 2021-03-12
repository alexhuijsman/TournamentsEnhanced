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
      _tournamentKB = tkb;
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();

      _dataSource = new ManageTeamSelectionVM(_tournamentKB) { IsEnabled = true };

      Layer = new GauntletLayer(206) { Name = "MenuManageHideoutTroops" };
      Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
      Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));

      _gauntletLayer = (Layer as GauntletLayer);
      _movie = _gauntletLayer.LoadMovie("GameMenuManageHideoutTroops", _dataSource);

      Layer.IsFocusLayer = true;

      ScreenManager.TrySetFocus(Layer);
      MenuViewContext.AddLayer(Layer);

      if (ScreenManager.TopScreen is MapScreen mapScreen)
        mapScreen.IsInHideoutTroopManage = true;
    }

    protected override void OnFinalize()
    {
      _gauntletLayer.IsFocusLayer = false;
      ScreenManager.TryLoseFocus(_gauntletLayer);
      _dataSource.OnFinalize();
      _dataSource = null;
      _gauntletLayer.ReleaseMovie(_movie);
      MenuViewContext.RemoveLayer(_gauntletLayer);
      _movie = null;
      _gauntletLayer = null;

      if (ScreenManager.TopScreen is MapScreen mapScreen)
        mapScreen.IsInHideoutTroopManage = false;

      base.OnFinalize();
    }

    protected override void OnFrameTick(float dt)
    {
      base.OnFrameTick(dt);

      if (_gauntletLayer.Input.IsHotKeyPressed("Exit"))
        _dataSource.IsEnabled = false;

      if (!_dataSource.IsEnabled)
        MenuViewContext.CloseManageHideoutTroops();
    }

    private readonly TournamentKB _tournamentKB;
    private GauntletLayer _gauntletLayer;
    private ManageTeamSelectionVM _dataSource;
    private IGauntletMovie _movie;
  }

  // Gauntlet Dummy
  public class MenuManageTeamSelection : MenuView
  {
  }
}
