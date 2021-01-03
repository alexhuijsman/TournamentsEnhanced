using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparer
{
  public abstract class TownComparerBase : IComparer<MBTown>
  {
    public abstract int Compare(MBTown x, MBTown y);
  }
}