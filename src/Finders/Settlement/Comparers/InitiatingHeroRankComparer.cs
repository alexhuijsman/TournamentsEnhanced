using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class InitiatingHeroRankComparer : ExistingTournamentComparer
  {
    public new static InitiatingHeroRankComparer Instance { get; } = null;
    public new static InitiatingHeroRankComparer InstanceIncludingExisting { get; } = null;

    public InitiatingHeroRankComparer(MBHero initiatingHero) : base(true, initiatingHero) { }

    protected override bool MeetsRequirements(MBSettlement settlement)
    {
      if (!base.MeetsRequirements(settlement))
      {
        return false;
      }

      if (!ModState.TournamentRecords.ContainsSettlement(settlement))
      {
        return true;
      }

      var record = ModState.TournamentRecords[settlement];

      return InitiatingHero.IsFactionLeader &&
             (!record.HasInitiatingHero || InitiatingHero == record.FindInitiatingHero().MapFaction.Leader);
    }
  }
}
