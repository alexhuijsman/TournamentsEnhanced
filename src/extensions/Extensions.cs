using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TournamentsEnhanced
{
  static class Extensions
  {

    public static bool IsLedBy(this Settlement settlement, TextObject leaderName)
    {
      return settlement.OwnerClan.Leader.Name.Equals(leaderName);
    }

    public static List<T> Shuffle<T>(this List<T> list)
    {
      int n = list.Count;
      int k;
      while (n > 1)
      {
        n--;
        k = MBRandom.DeterministicRandomInt(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }

      return list;
    }

  }
}
