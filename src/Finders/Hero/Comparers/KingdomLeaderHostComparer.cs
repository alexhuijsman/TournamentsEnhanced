using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class KingdomLeaderHostComparer : FactionLeaderHostComparer
  {
    public new static KingdomLeaderHostComparer Instance { get; } = new KingdomLeaderHostComparer();

    protected KingdomLeaderHostComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBHero hero) =>
      base.MeetsRequirements(hero) &&
      hero.MapFaction.IsKingdomFaction;
  }
}
