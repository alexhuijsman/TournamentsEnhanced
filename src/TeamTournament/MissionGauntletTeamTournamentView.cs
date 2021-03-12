using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;
using TournamentsEnhanced.TeamTournament.ViewModels;

namespace TournamentsEnhanced.TeamTournament
{
  [OverrideView(typeof(TeamTournamentMissionView))]
  public class MissionGauntletTeamTournamentView : MissionView
  {
    private TeamTournamentBehavior _behavior;
    private Camera _customCamera;
    private bool _showUi = true;
#pragma warning disable IDE0052 // Remove unread private members
    private IGauntletMovie _gauntletMovie;
#pragma warning restore IDE0052 // Remove unread private members
    private GauntletLayer _gauntletLayer;
    private TeamTournamentVM _dataSource;

    public MissionGauntletTeamTournamentView()
    {
      ViewOrderPriorty = 48;
    }

    public override void OnMissionScreenInitialize()
    {
      base.OnMissionScreenInitialize();
      _dataSource = new TeamTournamentVM(DisableUi, _behavior);
      _gauntletLayer = new GauntletLayer(ViewOrderPriorty, "GauntletLayer");
      _gauntletMovie = _gauntletLayer.LoadMovie("Tournament", _dataSource);
      MissionScreen.CustomCamera = _customCamera;
      _gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
      MissionScreen.AddLayer(_gauntletLayer);
    }

    public override void OnMissionScreenFinalize()
    {
      _gauntletLayer.InputRestrictions.ResetInputRestrictions();
      _gauntletMovie = null;
      _gauntletLayer = null;
      _dataSource.OnFinalize();
      _dataSource = null;
      base.OnMissionScreenFinalize();
    }

    public override void AfterStart()
    {
      _behavior = base.Mission.GetMissionBehaviour<TeamTournamentBehavior>();
      var gameEntity = base.Mission.Scene.FindEntityWithTag("camera_instance");
      _customCamera = Camera.CreateCamera();
      var vec = default(Vec3);
      gameEntity.GetCameraParamsFromCameraScript(_customCamera, ref vec);
    }

    public override void OnMissionTick(float dt)
    {
      if (_behavior == null)
      {
        return;
      }
      if (!_showUi && ((_behavior.LastMatch != null && _behavior.CurrentMatch == null) || _behavior.CurrentMatch.IsReady))
      {
        _dataSource.Refresh();
        ShowUi();
      }
      if (!_showUi && _dataSource.CurrentMatch.IsValid)
      {
        var currentMatch = _behavior.CurrentMatch;
        if (currentMatch != null && currentMatch.State == TournamentMatch.MatchState.Started)
        {
          _dataSource.CurrentMatch.RefreshActiveMatch();
        }
      }
      if (_dataSource.IsOver && _showUi && !base.DebugInput.IsControlDown() && base.DebugInput.IsHotKeyPressed("ShowHighlightsSummary"))
      {
        HighlightsController missionBehaviour = base.Mission.GetMissionBehaviour<HighlightsController>();
        if (missionBehaviour == null)
        {
          return;
        }
        missionBehaviour.ShowSummary();
      }
    }

    private void DisableUi()
    {
      if (!_showUi)
      {
        return;
      }
      MissionScreen.UpdateFreeCamera(_customCamera.Frame);
      MissionScreen.CustomCamera = null;
      _showUi = false;
      _gauntletLayer.InputRestrictions.ResetInputRestrictions();
    }

    private void ShowUi()
    {
      if (_showUi)
      {
        return;
      }
      MissionScreen.CustomCamera = _customCamera;
      _showUi = true;
      _gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
    }

    public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
    {
      base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, killingBlow);
      _dataSource.OnAgentRemoved(affectedAgent);
    }

    public override void OnPhotoModeActivated()
    {
      base.OnPhotoModeActivated();
      _gauntletLayer._gauntletUIContext.ContextAlpha = 0f;
    }

    public override void OnPhotoModeDeactivated()
    {
      base.OnPhotoModeDeactivated();
      _gauntletLayer._gauntletUIContext.ContextAlpha = 1f;
    }
  }

  public class TeamTournamentMissionView : MissionView { }
}
