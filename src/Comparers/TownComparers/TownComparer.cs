using System.Collections.Generic;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced.Comparer
{
  public abstract class TownComparer : IComparer<MBTown>
  {
    public abstract int Compare(MBTown x, MBTown y);
  }
}