using System;
using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public static class SettlementFinder
  {
    public static FindHostTownResult FindHostSettlement(FindHostSettlementOptions options)
    {
      var results = SortSettlementsByComparers(options.Settlements.ToList(), options.Comparers);
      return hostTown == null ? FindHostTownResult.Failure() : FindHostTownResult.Success(hostTown);
    }

    private static MBSettlementList SortSettlementsByComparers(MBSettlementList settlements, IComparer<MBSettlement>[] comparers)
    {
      foreach (var comparer in comparers)
      {
        SortSettlementsByComparers(comparer);
      }
    }

    private static void SortSettlementsByComparers(IComparer<MBSettlement> comparer)
    {
      throw new NotImplementedException();
    }
  }
}
