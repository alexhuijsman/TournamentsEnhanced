using System.Collections.Generic;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  public class FindHostTownOptions
  {
    public readonly IComparer<MBTown> Comparer;
    public readonly MBSettlementList Settlements;

    public FindHostTownOptions(MBSettlementList settlements, IComparer<MBTown> comparer = null)
    {
      Settlements = settlements;
      Comparer = comparer ?? new HostTownComparer();
    }
  }
}
