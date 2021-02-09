using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class InitiatingHeroOwnershipComparer : ExistingTournamentComparer
  {
    private new static InitiatingHeroOwnershipComparer Instance { get; } = null;
    private new static InitiatingHeroOwnershipComparer InstanceIncludingExisting { get; } = null;

    public InitiatingHeroOwnershipComparer(MBHero initiatingHero) : base(true, initiatingHero) { }

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

      return settlement.OwnerClan.Leader == InitiatingHero;
    }
  }
}
