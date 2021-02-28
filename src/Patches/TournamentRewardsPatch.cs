using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Patches
{
  [HarmonyPatch(typeof(TournamentGame), "GetTournamentPrize")]
  class TournamentRewardsPatch
  {
    static void Postfix(TournamentGame __instance, ref ItemObject __result)
    {
      {
        __result = MBItemObject.Instance.GetAvailableTournamentPrizes().GetRandomElement();
        return;
      }

    }
  }
}
