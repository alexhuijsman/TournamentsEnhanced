using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(TournamentMatch), "Start")]
  class TournamentStartMatchPatch
  {

    static void Postfix()
    {
      if (TournamentsEnhancedSettings.Instance.VeryHardTournaments)
      {
        Utilities.difficulty = CampaignOptions.CombatAIDifficulty;
        CampaignOptions.CombatAIDifficulty = CampaignOptions.Difficulty.Realistic;
      }
    }
  }
}
