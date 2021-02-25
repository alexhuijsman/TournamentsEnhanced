using System;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class InitiatingHeroOwnershipComparer : ExistingTournamentComparer
  {
    public new static InitiatingHeroOwnershipComparer Instance { get => throw new InvalidOperationException(); }
    public new static InitiatingHeroOwnershipComparer InstanceIncludingExisting { get => throw new InvalidOperationException(); }

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

      return settlement.OwnerClan.Leader == InitiatingHero;
    }
  }
}
