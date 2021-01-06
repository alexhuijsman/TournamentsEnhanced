using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ProsperityComparer : HostSettlementComparerBase
  {
    public float MinProsperity { get; private set; }

    public ProsperityComparer(Payor payor, float minProsperity) : base(payor)
    {
      MinProsperity = minProsperity;
    }

    public override int Compare(MBSettlement x, MBSettlement y)
    {
      var result = 0;

      var wasResultAssigned =
        TryComparePreconditions(x, y, ref result) ? true :
        CompareProsperity(x, y, out result);

      return result;
    }

    internal bool CompareProsperity(MBSettlement x, MBSettlement y, out int result)
    {
      var xIsMoreProsperous = x.Prosperity > y.Prosperity;
      var xIsLessProsperous = x.Prosperity < y.Prosperity;

      result = xIsMoreProsperous ? XIsGreaterThanY : xIsLessProsperous ? XIsLessThanY : XIsEqualToY;

      return true;
    }

    internal override bool MeetsRequirements(MBSettlement wrapper) =>
      wrapper.Prosperity >= MinProsperity;
  }
}