using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced
{
  public class FindHostSettlementOptions
  {
    public readonly IComparer<MBSettlement> Comparer;
    public readonly MBSettlementList Settlements;

    public FindHostSettlementOptions(MBSettlementList settlements, IComparer<MBSettlement> comparer = null)
    {
      Settlements = settlements;
      Comparer = comparer ?? new TownComparer();
    }
  }
}
