using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class FactionLeaderHostComparer : BasicHeroHostRequirementsComparer
  {
    public new static FactionLeaderHostComparer Instance { get; } = new FactionLeaderHostComparer();

    protected FactionLeaderHostComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBHero hero) =>
      base.MeetsRequirements(hero) &&
      hero.IsFactionLeader;
  }
}
