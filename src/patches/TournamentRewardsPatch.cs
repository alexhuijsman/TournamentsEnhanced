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
        if (enumerator.Current.IsTierable() && enumerator.Current.Tier.Equals(Hero.MainHero.GetTournamentRewardTier()))
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
