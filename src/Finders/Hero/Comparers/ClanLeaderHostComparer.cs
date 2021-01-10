using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class ClanLeaderHostComparer : FactionLeaderHostComparer
  {
    public new static ClanLeaderHostComparer Instance { get; } = new ClanLeaderHostComparer();

    protected ClanLeaderHostComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBHero hero) =>
      base.MeetsRequirements(hero) &&
      hero.MapFaction.IsClan;
  }
}
