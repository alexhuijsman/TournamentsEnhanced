using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(TournamentGame), "GetTournamentPrize")]
  class TournamentRewardsPatch
  {
    static void Postfix(TournamentGame __instance, ref ItemObject __result)
    {
      {
        __result = MBItemObject.GetAvailableTournamentPrizes().GetRandomElement();
        return;
      }

    }
  }
}
