using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class MaleKingdomLeaderHostComparer : BasicHostRequirementsHeroComparer
  {
    public MaleKingdomLeaderHostComparer(MBHero initiatingHero) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBHero hero) =>
      base.MeetsRequirements(hero) &&
      !hero.IsFemale &&
      hero.IsFactionLeader &&
      hero.MapFaction.IsKingdomFaction;
  }
}
