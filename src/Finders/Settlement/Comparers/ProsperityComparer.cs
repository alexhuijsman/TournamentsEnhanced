using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ProsperityComparer : HostSettlementComparerBase
  {
    public float MinProsperity { get; private set; }

    public ProsperityComparer(MBHero payor, float minProsperity = 0) : base(payor)
    {
      MinProsperity = minProsperity;
    }

    public override int Compare(MBSettlement x, MBSettlement y)
    {
      var result = 0;

      if (!TryComparePreconditions(x, y, ref result))
      {
        CompareProsperity(x, y, out result);
      }

      return result;
    }

    internal bool CompareProsperity(MBSettlement x, MBSettlement y, out int result)
    {
      var xIsMoreProsperous = x.Prosperity > y.Prosperity;
      var xIsLessProsperous = x.Prosperity < y.Prosperity;

      result = xIsMoreProsperous ? XIsGreaterThanY : xIsLessProsperous ? XIsLessThanY : XIsEqualToY;

      return true;
    }

    protected override bool MeetsRequirements(MBSettlement wrapper) =>
      wrapper.Prosperity >= MinProsperity;
  }
}