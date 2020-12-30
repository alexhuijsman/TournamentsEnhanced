using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(TournamentMatch), "End")]
  class TournamentEndMatchPatch
  {
    static void Postfix(TournamentMatch __instance)
    {
      if (Settings.Instance.VeryHardTournaments && __instance.IsPlayerParticipating())
      {
        CampaignOptions.CombatAIDifficulty = CampaignModel.difficultyBeforeTournament;
      }

      TournamentRecords.RemoveByTown(__instance.GetTown());
    }
  }
}
