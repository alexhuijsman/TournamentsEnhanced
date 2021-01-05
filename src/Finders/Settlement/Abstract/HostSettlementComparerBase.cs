using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class HostSettlementComparerBase : SettlementComparerBase
  {

    public Payor Payor { get; private set; }
    public bool HasExistingTournament(MBSettlement settlement) => ModState.TournamentRecords.ContainsSettlement(settlement);
    public HostSettlementComparerBase(Payor payor) => Payor = payor;
  }
}