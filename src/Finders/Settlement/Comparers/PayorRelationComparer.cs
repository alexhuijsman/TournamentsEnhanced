using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class PayorRelationComparer : ExistingTournamentComparer
  {
    public float MaxRelation { get; private set; }

    public PayorRelationComparer(Payor payor, float maxRelation) : base(payor, true)
    {
      MaxRelation = maxRelation;
    }

    public override int Compare(MBSettlement x, MBSettlement y)
    {
      var result = 0;

      var wasResultAssigned =
        TryComparePreconditions(x, y, ref result) ? true :
        ComparePayorRelation(x, y, out result);

      return result;
    }

    internal bool ComparePayorRelation(MBSettlement x, MBSettlement y, out int result)
    {
      var xHasRecord = ModState.TournamentRecords.ContainsSettlement(x);
      var yHasRecord = ModState.TournamentRecords.ContainsSettlement(y);

      var xRecord = xHasRecord ? ModState.TournamentRecords[x] : default(TournamentRecord);
      var yRecord = yHasRecord ? ModState.TournamentRecords[y] : default(TournamentRecord);

      var xHasPayorHero = xRecord.IsHeroPayor;
      var yHasPayorHero = yRecord.IsHeroPayor;

      var xPayorHero = xHasPayorHero ? xRecord.FindPayorHero() : null;
      var yPayorHero = yHasPayorHero ? yRecord.FindPayorHero() : null;

      var xRelation = xHasPayorHero ? xPayorHero.GetRelation(Payor.Hero) : 0;
      var yRelation = yHasPayorHero ? yPayorHero.GetRelation(Payor.Hero) : 0;

      var xHasWorseRelation = xRelation < yRelation;
      var xHasBetterRelation = xRelation > yRelation;

      result = xHasWorseRelation ? XIsGreaterThanY : xHasBetterRelation ? XIsLessThanY : XIsEqualToY;

      return true;
    }
  }
}