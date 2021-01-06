using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class BasicHostRequirementsHeroComparer : HeroComparerBase
  {
    internal override bool MeetsRequirements(MBHero hero) =>
      hero.IsActive &&
      !hero.IsPrisoner &&
      hero.Gold >= Settings.Instance.TournamentCost;
  }
}
