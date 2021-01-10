using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class BasicHostRequirementsComparer : HostSettlementComparerBase
  {
    public static BasicHostRequirementsComparer Instance { get; } = new BasicHostRequirementsComparer();
    protected BasicHostRequirementsComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBSettlement settlement) =>
      settlement.IsTown &&
      settlement.Town.FoodStocks >= Settings.Instance.FoodStocksDecrease;
  }
}
