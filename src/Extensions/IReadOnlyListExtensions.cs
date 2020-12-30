using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class IReadOnlyListExtensions
  {
    public static List<Town> ToList(this IReadOnlyList<Town> readOnlyList)
    {
      return new List<Town>(readOnlyList);
    }
  }
}