using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Kingdom
{
  public class BasicKingdomHostRequirementsComparer : KingdomComparerBase
  {
    public HeroFinder HeroFinder { protected get; set; } = HeroFinder.Instance;
    public static BasicKingdomHostRequirementsComparer Instance { get; } = new BasicKingdomHostRequirementsComparer();

    protected BasicKingdomHostRequirementsComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

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
