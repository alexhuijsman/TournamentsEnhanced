using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class BasicSettlementHostRequirementsComparer : HostSettlementComparerBase
  {
    public static BasicSettlementHostRequirementsComparer Instance { get; } = new BasicSettlementHostRequirementsComparer();
    protected BasicSettlementHostRequirementsComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBSettlement settlement) =>
      settlement.IsTown &&
      settlement.Town.FoodStocks >= Settings.FoodStocksDecrease;
  }
}
