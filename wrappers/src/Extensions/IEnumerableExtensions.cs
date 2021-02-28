using System.Collections.Generic;
using System.Linq;

namespace TournamentsEnhanced.Wrappers
{
  public static class IEnumerableExtensions
  {
    public static List<T> ToList<T>(this IEnumerable<T> enumerable)
    {
      return new List<T>(enumerable);
    }

    public static List<T> CastList<T>(this IEnumerable<object> enumerable)
    {
      return enumerable.Cast<T>().ToList();
    }
  }
}
