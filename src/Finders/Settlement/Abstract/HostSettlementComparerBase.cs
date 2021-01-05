using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class HostSettlementComparerBase : SettlementComparerBase
  {
    public Payor Payor { get; private set; }

    public HostSettlementComparerBase(Payor payor) => Payor = payor;

    protected bool HasExistingTournament(MBSettlement settlement) => ModState.TournamentRecords.ContainsSettlement(settlement);

    protected bool HasExistingTournament(TournamentRecord record) => record.tournamentType != TournamentType.None;

    protected bool PayorIsNotSameAs(TournamentRecord record) => record.IsHeroPayor && Payor.Hero.StringId == record.payorId;
    protected bool PayorOutranksPayorOf(TournamentRecord record) => PayorIsFactionLeader() || record.payorType == PayorType.Settlement;
    protected bool PayorIsFactionLeader() => Payor.IsHero && Payor.Hero.IsFactionLeader;
  }
}