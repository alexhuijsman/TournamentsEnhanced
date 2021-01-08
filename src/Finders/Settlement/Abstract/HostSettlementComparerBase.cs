using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class HostSettlementComparerBase : SettlementComparerBase
  {
    public HostSettlementComparerBase(MBHero initiatingHero) : base(initiatingHero) { }

    protected bool HasExistingTournament(MBSettlement settlement) =>
      ModState.TournamentRecords.ContainsSettlement(settlement);

    //TODO move to Hero Comparers
    protected bool InitiatingHeroIsSameAs(TournamentRecord record) => record.initiatingHeroStringId == InitiatingHero.StringId;
    protected bool HeroIsKingdomLeader(MBHero hero) => hero.IsFactionLeader && hero.MapFaction.IsKingdomFaction;
  }
}
