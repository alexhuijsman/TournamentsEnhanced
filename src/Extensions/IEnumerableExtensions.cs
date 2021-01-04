using System.Collections.Generic;

namespace TournamentsEnhanced
{
  public static class IEnumerableExtensions
  {
    public static List<T> ToList<T>(this IEnumerable<T> enumerable)
    {
      return new List<T>(enumerable);
    }
  }
}
