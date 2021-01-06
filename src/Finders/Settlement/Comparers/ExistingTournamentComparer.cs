using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ExistingTournamentComparer : HostSettlementComparerBase
  {
    public bool CanOverrideExisting { get; private set; }

    public ExistingTournamentComparer(Payor payor, bool canOverrideExisting) : base(payor)
    {
      CanOverrideExisting = canOverrideExisting;
    }

    internal override bool MeetsRequirements(MBSettlement settlement)
    {
      return CanOverrideExisting ||
             !HasExistingTournament(settlement);
    }
  }
}
