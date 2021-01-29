using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class HostSettlementComparerBase : SettlementComparerBase
  {
    protected ModState ModState { get; set; } = ModState.Instance;

    protected HostSettlementComparerBase(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected bool HasExistingTournament(MBSettlement settlement) =>
      ModState.TournamentRecords.ContainsSettlement(settlement);
  }
}
