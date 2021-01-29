using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Models;

namespace TournamentsEnhanced.Patches
{
  [HarmonyPatch(typeof(TournamentMatch), "Start")]
  class TournamentStartMatchPatch
  {
    protected static Settings Settings { get; set; } = Settings.Instance;
    protected static CampaignModel CampaignModel { get; set; } = CampaignModel.Instance;

    static void Postfix()
    {
      if (Settings.VeryHardTournaments)
      {
        CampaignModel.NonTournamentDifficulty = CampaignOptions.CombatAIDifficulty;
        CampaignOptions.CombatAIDifficulty = CampaignOptions.Difficulty.Realistic;
      }
    }
  }
}
