using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparer
{
  public abstract class SettlementComparerBase : IComparer<MBSettlement>
  {
    public abstract int Compare(MBSettlement x, MBSettlement y);
  }
}