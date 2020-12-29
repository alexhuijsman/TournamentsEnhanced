using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(TournamentMatch), "End")]
  class TournamentEndMatchPatch
  {
    static void Postfix()
    {
      if (Settings.Instance.VeryHardTournaments)
      {
        CampaignOptions.CombatAIDifficulty = CampaignModel.difficultyBeforeTournament;
      }

      TournamentRecords.DeleteForCurrentTown();
    }
  }
}
