using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Faction
{
  public class BasicFactionHostRequirementsComparer : FactionComparerBase
  {
    public static BasicFactionHostRequirementsComparer Instance { get; } = new BasicFactionHostRequirementsComparer();
    protected HeroFinder HeroFinder { get; set; } = HeroFinder.Instance;

    protected BasicFactionHostRequirementsComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBFaction faction) =>
      !faction.Settlements.IsEmpty() &&
      faction.Settlements.FindIndex(
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
