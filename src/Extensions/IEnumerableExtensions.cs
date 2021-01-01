using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced
{
  static class IEnumerableExtensions
  {
    public static List<T> ToList<T>(this IEnumerable<T> enumerable)
    {
      return new List<T>(enumerable);
    }
  }
}
