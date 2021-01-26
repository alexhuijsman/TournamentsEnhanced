using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced
{
  public static class ListExtensions
  {
    public static MBMBRandom MBMBRandom { private get; set; } = MBMBRandom.Instance;

    public static List<T> Shuffle<T>(this List<T> list)
    {
      int n = list.Count;
      int k;
      while (n > 1)
      {
        n--;
        k = MBMBRandom.DeterministicRandomInt(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }

      return list;
    }
  }
}
