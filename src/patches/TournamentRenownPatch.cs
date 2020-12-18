using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
  class TournamentRenownPatch
  {
    static void Postfix(ref int __result)
    {
      __result = TournamentsEnhancedSettings.Instance.RenownReward;
    }
  }
}
