using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace TournamentsEnhanced
{
  public static class IReadOnlyListExtensions
  {
    public static List<Town> ToList(this IReadOnlyList<Town> readOnlyList)
    {
      return new List<Town>(readOnlyList);
    }

    public static List<MBSettlement> ConvertToMBSettlementList(this MBReadOnlyList<Settlement> sourceList)
    {
      var convertedList = new List<MBSettlement>(sourceList.Count);

      foreach (var settlement in sourceList)
      {
        convertedList.Add(settlement);
      }

      return convertedList;
    }
  }
}