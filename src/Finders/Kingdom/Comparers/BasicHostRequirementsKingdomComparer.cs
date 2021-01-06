using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class BasicHostRequirementsKingdomComparer : KingdomComparerBase
  {
    internal override bool MeetsRequirements(MBKingdom kingdom) =>
      !kingdom.Settlements.IsEmpty &&
      kingdom.Settlements.FindIndex((settlement) => settlement.IsTown) != -1;
  }
}
