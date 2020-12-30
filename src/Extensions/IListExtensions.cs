using System.Collections.Generic;

using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  static class IListExtensions
  {
    public static List<T> Shuffle<T>(this List<T> list)
    {
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = MBRandom.DeterministicRandomInt(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }

      return list;
    }
  }
}
