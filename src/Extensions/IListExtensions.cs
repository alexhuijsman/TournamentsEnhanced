using System.Collections.Generic;

using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  public static class IListExtensions
  {
    public static bool IsEmpty<T>(this IList<T> list) => list.Count == 0;
    public static T First<T>(this IList<T> list) => list[0];
  }
}
