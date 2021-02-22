using HarmonyLib;
using SandBox;
using SandBox.Source.Missions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace TournamentsEnhanced.TeamTournament.Patches
{
  [HarmonyPatch(typeof(MissionStarter), "OpenTournamentFightMission")]
  class MissionStarterOpenTournamentFightMissionPatch
  {
    static bool Prefix(ref Mission __result, string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
    {
      if (TournamentKB.Current != null && TournamentKB.Current.IsTeamTournament)
      {
        __result = MissionState.OpenNew("TournamentFight", SandBoxMissions.CreateSandBoxMissionInitializerRecord(scene, "", false), delegate (Mission missionController)
        {
          var tournamentMissionController = new TeamTournamentMissionController(culture);
          return new MissionBehaviour[]
          {
          new CampaignMissionComponent(),
          tournamentMissionController,
          new TeamTournamentBehavior(tournamentGame, settlement, tournamentMissionController, isPlayerParticipating), // this is patched!
          new AgentVictoryLogic(),
          new MissionAgentPanicHandler(),
          new AgentBattleAILogic(),
          new ArenaAgentStateDeciderLogic(),
          new MissionHardBorderPlacer(),
          new MissionBoundaryPlacer(),
          new MissionOptionsComponent(),
          new HighlightsController(),
          new SandboxHighlightsController()
          };
        }, true, true);
        return false;
      }
      return true;
    }
  }
}
