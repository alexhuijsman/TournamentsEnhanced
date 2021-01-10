using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class HostSettlementComparerBase : SettlementComparerBase
  {
    protected HostSettlementComparerBase(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected bool HasExistingTournament(MBSettlement settlement) =>
      ModState.TournamentRecords.ContainsSettlement(settlement);

    //TODO move to Hero Comparers
    protected bool InitiatingHeroIsSameAs(TournamentRecord record) => record.initiatingHeroStringId == InitiatingHero.StringId;
    protected bool HeroIsKingdomLeader(MBHero hero) => hero.IsFactionLeader && hero.MapFaction.IsKingdomFaction;
  }
}
