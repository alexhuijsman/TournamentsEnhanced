using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Patches
{
  [HarmonyPatch(typeof(TournamentMatch), "End")]
  class TournamentEndMatchPatch
  {
    protected static Settings Settings { get; set; } = Settings.Instance;
    protected static ModState ModState { get; set; } = ModState.Instance;
    protected static CampaignModel CampaignModel { get; set; } = CampaignModel.Instance;

    static void Postfix(TournamentMatch __instance)
    {
      var tournamentMatch = (MBTournamentMatch)__instance;

      if (Settings.VeryHardTournaments && tournamentMatch.IsPlayerParticipating())
      {
        CampaignOptions.CombatAIDifficulty = CampaignModel.NonTournamentDifficulty;
      }

      ModState.TournamentRecords.Remove(tournamentMatch.HostSettlement);
    }
  }
}
