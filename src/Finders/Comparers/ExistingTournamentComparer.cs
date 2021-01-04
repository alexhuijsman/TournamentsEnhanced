using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public class ExistingTournamentComparer : HostSettlementComparerBase
  {
    public float MaxRelation { get; private set; }

    public ExistingTournamentComparer(Payor payor, float maxRelation) : base(payor)
    {
      MaxRelation = maxRelation;
    }

    public override int Compare(MBSettlement x, MBSettlement y)
    {
      var xHasRecord = ModState.TournamentRecords.ContainsSettlement(x);
      var yHasRecord = ModState.TournamentRecords.ContainsSettlement(y);

      var xRecord = xHasRecord ? ModState.TournamentRecords[x] : default(TournamentRecord);
      var yRecord = yHasRecord ? ModState.TournamentRecords[y] : default(TournamentRecord);

      var xHasPayorHero = xHasRecord && xRecord.IsHeroPayor;
      var yHasPayorHero = yHasRecord && yRecord.IsHeroPayor;

      var xPayorHero = xHasPayorHero ? xRecord.PayorHero : null;
      var yPayorHero = yHasPayorHero ? yRecord.PayorHero : null;

      var xRelation = xHasPayorHero ? xPayorHero.GetRelation(Payor.Hero) : 0;
      var yRelation = yHasPayorHero ? yPayorHero.GetRelation(Payor.Hero) : 0;

      var xMeetsRequirements = !x.IsNull &&
                                x.Town.HasTournament &&
                                xRecord.IsHeroPayor &&
                                xPayorHero != Payor.Hero &&
                                xRelation < MaxRelation;

      var yMeetsRequirements = !y.IsNull &&
                                y.Town.HasTournament &&
                                yRecord.IsHeroPayor &&
                                yPayorHero != Payor.Hero &&
                                yRelation < MaxRelation;

      if (!xMeetsRequirements)
      {
        return yMeetsRequirements ? XIsLessThanY : XIsEqualToY;
      }

      if (!yMeetsRequirements)
      {
        return XIsGreaterThanY;
      }

      var xHasWorseRelation = xRelation < yRelation;
      var xHasBetterRelation = xRelation > yRelation;

      return xHasWorseRelation ? XIsGreaterThanY : xHasBetterRelation ? XIsLessThanY : XIsEqualToY;
    }

    private object GetPayorHero(TournamentRecord xRecord)
    {
      throw new System.NotImplementedException();
    }
  }
}