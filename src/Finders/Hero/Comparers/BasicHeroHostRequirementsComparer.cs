using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class BasicHeroHostRequirementsComparer : HeroComparerBase
  {
    public static BasicHeroHostRequirementsComparer Instance { get; } = new BasicHeroHostRequirementsComparer();

    protected BasicHeroHostRequirementsComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBHero hero) =>
      hero.IsActive &&
      hero.Gold >= Settings.TournamentCost;
  }
}
