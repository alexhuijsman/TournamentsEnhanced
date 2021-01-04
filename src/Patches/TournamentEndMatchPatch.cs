using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Models;
using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(TournamentMatch), "End")]
  class TournamentEndMatchPatch
  {
    static void Postfix(TournamentMatch __instance)
    {
      var tournamentMatch = (MBTournamentMatch)__instance;

      if (Settings.Instance.VeryHardTournaments && tournamentMatch.IsPlayerParticipating())
      {
        CampaignOptions.CombatAIDifficulty = CampaignModel.NonTournamentDifficulty;
      }

      ModState.TournamentRecords.Remove(tournamentMatch.HostSettlement);
    }
  }
}
