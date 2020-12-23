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
      if (Settings.Instance.VeryHardTournaments)
      {
        CampaignModel.difficultyBeforeTournament = CampaignOptions.CombatAIDifficulty;
        CampaignOptions.CombatAIDifficulty = CampaignOptions.Difficulty.Realistic;
      }
    }
  }
}
