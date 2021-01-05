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
      var xMeetsRequirements = !x.IsNull && x.Prosperity >= MinProsperity;
      var yMeetsRequirements = !y.IsNull && y.Prosperity >= MinProsperity;

      if (!xMeetsRequirements)
      {
        return yMeetsRequirements ? XIsLessThanY : XIsEqualToY;
      }

      if (!yMeetsRequirements)
      {
        return XIsGreaterThanY;
      }

      var xIsMoreProsperous = x.Prosperity > y.Prosperity;
      var xIsLessProsperous = x.Prosperity < y.Prosperity;

      return xIsMoreProsperous ? XIsGreaterThanY : xIsLessProsperous ? XIsLessThanY : XIsEqualToY;
    }
  }
}