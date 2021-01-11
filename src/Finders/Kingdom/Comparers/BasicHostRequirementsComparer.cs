using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Kingdom
{
  public class BasicHostRequirementsComparer : KingdomComparerBase
  {
    public static BasicHostRequirementsComparer Instance { get; } = new BasicHostRequirementsComparer();

    protected BasicHostRequirementsComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBKingdom kingdom) =>
      !kingdom.Settlements.IsEmpty() &&
      kingdom.Settlements.FindIndex(
        (settlement) => settlement.IsTown &&
        PayorMeetsRequirements(settlement.OwnerClan.Leader)
      ) != -1;

    private bool PayorMeetsRequirements(MBHero payor)
    {
      var result = HeroFinder.FindHostsThatMeetBasicRequirements(payor);

      return result.Succeeded;
    }
  }
}
