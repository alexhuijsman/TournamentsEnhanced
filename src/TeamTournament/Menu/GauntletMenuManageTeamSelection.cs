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
      _dataSource = new ManageTeamSelectionVM(_tournamentKB)
      {
        IsEnabled = true
      };
      _gauntletLayer = new GauntletLayer(205, "GauntletLayer")
      {
        Name = "MenuManageHideoutTroops"
      };
      _gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
      _gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
      _movie = _gauntletLayer.LoadMovie("GameMenuManageHideoutTroops", _dataSource);
      _gauntletLayer.IsFocusLayer = true;
      ScreenManager.TrySetFocus(_gauntletLayer);
      base.MenuViewContext.AddLayer(_gauntletLayer);
      MapScreen mapScreen = ScreenManager.TopScreen as MapScreen;
      if (mapScreen != null)
        mapScreen.IsInHideoutTroopManage = true;
    }

    protected override void OnFinalize()
    {
      _gauntletLayer.IsFocusLayer = false;
      ScreenManager.TryLoseFocus(_gauntletLayer);
      _dataSource.OnFinalize();
      _dataSource = null;
      _gauntletLayer.ReleaseMovie(_movie);
      base.MenuViewContext.RemoveLayer(_gauntletLayer);
      _movie = null;
      _gauntletLayer = null;
      MapScreen mapScreen = ScreenManager.TopScreen as MapScreen;
      if (mapScreen != null)
        mapScreen.IsInHideoutTroopManage = false;

      base.OnFinalize();
    }

    protected override void OnFrameTick(float dt)
    {
      base.OnFrameTick(dt);
      if (_gauntletLayer.Input.IsHotKeyPressed("Exit"))
      {
        _dataSource.IsEnabled = false;
      }
      if (!_dataSource.IsEnabled)
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
