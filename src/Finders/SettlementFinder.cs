using System.Collections.Generic;
using System.Linq;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public static class SettlementFinder
  {
    public static FindHostSettlementResult FindHostSettlement(FindHostSettlementOptions options)
    {
      var sortedSettlements = SortSettlementsByComparers(options.Settlements, options.Comparers);
      return sortedSettlements.Count == 0 ? FindHostSettlementResult.Failure() : FindHostSettlementResult.Success(sortedSettlements.First());
    }

    private static MBSettlementList SortSettlementsByComparers(MBSettlementList settlements, IComparer<MBSettlement>[] comparers)
    {
      var sortedSettlements = settlements.ToList();
      sortedSettlements.Add(MBSettlement.Null);

      foreach (var comparer in comparers)
      {
        sortedSettlements.Sort(comparer);
        RemoveSettlementsRankedLowerThanNull(sortedSettlements);
      }

      sortedSettlements.Remove(MBSettlement.Null);

      return sortedSettlements;
    }

    private static void RemoveSettlementsRankedLowerThanNull(MBSettlementList settlements)
    {
      var nullIndexSearchResult = GetNullIndexOfSettlements(settlements);
      var nullIndex = nullIndexSearchResult.indexValue;

      if (!nullIndexSearchResult.wasSuccessful)
      {
        return;
      }

      var numRecordsToRemove = settlements.Count - nullIndex - 1;

      settlements.RemoveRange(nullIndex + 1, numRecordsToRemove);

    }

    private static NullIndexSearchResult GetNullIndexOfSettlements(MBSettlementList settlements)
    {
      int nullIndex = -1;
      for (int i = 0; i < settlements.Count; i++)
      {
        if (settlements[i] != MBSettlement.Null)
        {
          continue;
        }

        nullIndex = i;
        break;
      }

      return new NullIndexSearchResult() { wasSuccessful = nullIndex >= 0, indexValue = nullIndex };
    }

    private struct NullIndexSearchResult
    {
      public bool wasSuccessful;
      public int indexValue;
    }
  }
}
