using System.Collections.Generic;
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
      List<ItemObject>.Enumerator enumerator = ItemObject.All.GetEnumerator();
      List<ItemObject> tier = new List<ItemObject>();

      while (enumerator.MoveNext())
      {
        if (Utilities.IsTierable(enumerator.Current) && enumerator.Current.Tier.Equals((ItemObject.ItemTiers)Utilities.RewardTier()))
        {
          tier.Add(enumerator.Current);
        }
      }
      if (!tier.IsEmpty())
      {
        __result = tier.GetRandomElement();
        return;
      }

    }
  }
}
