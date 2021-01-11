using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced
{
  static class IListExtensions
  {
    public static IList<T> DeterministicShuffle<T>(this IList<T> list)
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

    public static bool IsEmpty<T>(this IList<T> list) => list.Count == 0;
    public static T First<T>(this IList<T> list) => list[0];
  }
}
