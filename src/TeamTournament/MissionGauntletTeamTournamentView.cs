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
    private GauntletMovie _gauntletMovie;
    private GauntletLayer _gauntletLayer;
    private TeamTournamentVM _dataSource;

    public MissionGauntletTeamTournamentView()
    {
      this.ViewOrderPriorty = 48;
    }

    public override void OnMissionScreenInitialize()
    {
      base.OnMissionScreenInitialize();
      this._dataSource = new TeamTournamentVM(this.DisableUi, this._behavior);
      this._gauntletLayer = new GauntletLayer(this.ViewOrderPriorty, "GauntletLayer");
      this._gauntletMovie = this._gauntletLayer.LoadMovie("Tournament", this._dataSource);
      base.MissionScreen.CustomCamera = this._customCamera;
      this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
      base.MissionScreen.AddLayer(this._gauntletLayer);
    }

    public override void OnMissionScreenFinalize()
    {
      this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
      this._gauntletMovie = null;
      this._gauntletLayer = null;
      this._dataSource.OnFinalize();
      this._dataSource = null;
      base.OnMissionScreenFinalize();
    }

    public override void AfterStart()
    {
      this._behavior = base.Mission.GetMissionBehaviour<TeamTournamentBehavior>();
      var gameEntity = base.Mission.Scene.FindEntityWithTag("camera_instance");
      this._customCamera = Camera.CreateCamera();
      var vec = default(Vec3);
      gameEntity.GetCameraParamsFromCameraScript(this._customCamera, ref vec);
    }

    public override void OnMissionTick(float dt)
    {
      if (this._behavior == null)
      {
        return;
      }
      if (!this._showUi && ((this._behavior.LastMatch != null && this._behavior.CurrentMatch == null) || this._behavior.CurrentMatch.IsReady))
      {
        this._dataSource.Refresh();
        this.ShowUi();
      }
      if (!this._showUi && this._dataSource.CurrentMatch.IsValid)
      {
        var currentMatch = this._behavior.CurrentMatch;
        if (currentMatch != null && currentMatch.State == TournamentMatch.MatchState.Started)
        {
          this._dataSource.CurrentMatch.RefreshActiveMatch();
        }
      }
      if (this._dataSource.IsOver && this._showUi && !base.DebugInput.IsControlDown() && base.DebugInput.IsHotKeyPressed("ShowHighlightsSummary"))
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
      if (!this._showUi)
      {
        return;
      }
      base.MissionScreen.UpdateFreeCamera(this._customCamera.Frame);
      base.MissionScreen.CustomCamera = null;
      this._showUi = false;
      this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
    }

    private void ShowUi()
    {
      if (this._showUi)
      {
        return;
      }
      base.MissionScreen.CustomCamera = this._customCamera;
      this._showUi = true;
      this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
    }

    public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
    {
      base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, killingBlow);
      this._dataSource.OnAgentRemoved(affectedAgent);
    }

    public override void OnPhotoModeActivated()
    {
      base.OnPhotoModeActivated();
      this._gauntletLayer._gauntletUIContext.ContextAlpha = 0f;
    }

    public override void OnPhotoModeDeactivated()
    {
      base.OnPhotoModeDeactivated();
      this._gauntletLayer._gauntletUIContext.ContextAlpha = 1f;
    }
  }

  public class TeamTournamentMissionView : MissionView { }
}
