using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

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
