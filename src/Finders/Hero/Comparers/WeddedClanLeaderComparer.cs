using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class WeddedClanLeaderComparer : BasicHostRequirementsHeroComparer
  {
    internal override bool MeetsRequirements(MBHero hero) =>
      base.MeetsRequirements(hero) &&
      !hero.IsFemale &&
      hero.IsFactionLeader &&
      hero.MapFaction.IsClan;
  }
}
