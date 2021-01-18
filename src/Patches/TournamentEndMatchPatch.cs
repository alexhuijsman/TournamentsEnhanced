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
    public static ModState ModState { protected get; set; } = ModState.Instance;
    public static CampaignModel CampaignModel { protected get; set; } = CampaignModel.Instance;

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
