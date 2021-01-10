using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class BasicHostRequirementsComparer : HeroComparerBase
  {
    public static BasicHostRequirementsComparer Instance { get; } = new BasicHostRequirementsComparer();

    protected BasicHostRequirementsComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBHero hero) =>
      hero.IsActive &&
      !hero.IsPrisoner &&
      hero.Gold >= Settings.Instance.TournamentCost;
  }
}
