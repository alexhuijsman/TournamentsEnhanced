using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public static class SettlementFinder
  {
    public static FindHostSettlementResult FindHostSettlement(FindHostSettlementOptions options)
    {
      var validHostSettlements = options.Settlements.ToList();
      SortAndFilterByComparers(validHostSettlements, options.Comparers);

      return validHostSettlements.IsEmpty ?
        FindHostSettlementResult.Failure() :
        FindHostSettlementResult.Success(validHostSettlements.First());
    }

    private static MBSettlementList SortAndFilterByComparers(MBSettlementList settlements, IComparer<MBSettlement>[] comparers)
    {
      settlements.Add(MBSettlement.Null);

      foreach (var comparer in comparers)
      {
        settlements.Sort(comparer);
        RemoveSettlementsRankedLowerThanNull(settlements);
      }

      settlements.Remove(MBSettlement.Null);

      return settlements;
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
