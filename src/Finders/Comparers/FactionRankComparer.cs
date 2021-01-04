using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public class FactionRankComparer : HostSettlementComparerBase
  {
    public FactionRankComparer(Payor payor) : base(payor) { }

    public override int Compare(MBSettlement x, MBSettlement y)
    {
      throw new System.NotImplementedException();
    }
  }
}