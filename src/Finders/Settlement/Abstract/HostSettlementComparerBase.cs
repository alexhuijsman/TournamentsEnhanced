using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class HostSettlementComparerBase : SettlementComparerBase
  {
    public Payor Payor { get; private set; }

    public HostSettlementComparerBase(Payor payor) => Payor = payor;

    protected bool HasExistingTournament(MBSettlement settlement) =>
      ModState.TournamentRecords.ContainsSettlement(settlement);

    protected bool PayorIsSameAs(TournamentRecord record) =>
      record.IsHeroPayor &&
      record.FindPayorHero() == Payor.Hero;

    protected bool PayorOutranksPayorOf(TournamentRecord record) =>
      (PayorIsFactionLeader() && PayorFactionIsKingdom()) ||
      record.payorType == PayorType.Settlement;

    protected bool PayorIsFactionLeader() =>
      Payor.IsHero &&
      Payor.Hero.IsFactionLeader;

    protected bool PayorFactionIsKingdom() =>
      Payor.Hero.MapFaction.IsKingdomFaction;
  }
}