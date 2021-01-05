using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ExistingTournamentRelationComparer : ExistingTournamentComparer
  {
    public float MaxRelation { get; private set; }

    public ExistingTournamentRelationComparer(Payor payor, float maxRelation) : base(payor, true)
    {
      MaxRelation = maxRelation;
    }

    public override int Compare(MBSettlement x, MBSettlement y)
    {
      var xHasRecord = ModState.TournamentRecords.ContainsSettlement(x);
      var yHasRecord = ModState.TournamentRecords.ContainsSettlement(y);

      var xRecord = xHasRecord ? ModState.TournamentRecords[x] : default(TournamentRecord);
      var yRecord = yHasRecord ? ModState.TournamentRecords[y] : default(TournamentRecord);

      var xMeetsRequirements = !x.IsNull && MeetsAllRequirements(xRecord);

      var yMeetsRequirements = !y.IsNull && MeetsAllRequirements(yRecord);

      if (!xMeetsRequirements)
      {
        return yMeetsRequirements ? XIsLessThanY : XIsEqualToY;
      }

      if (!yMeetsRequirements)
      {
        return XIsGreaterThanY;
      }

      var xHasPayorHero = xRecord.IsHeroPayor;
      var yHasPayorHero = yRecord.IsHeroPayor;

      var xPayorHero = xHasPayorHero ? xRecord.FindPayorHero() : null;
      var yPayorHero = yHasPayorHero ? yRecord.FindPayorHero() : null;

      var xRelation = xHasPayorHero ? xPayorHero.GetRelation(Payor.Hero) : 0;
      var yRelation = yHasPayorHero ? yPayorHero.GetRelation(Payor.Hero) : 0;

      var xHasWorseRelation = xRelation < yRelation;
      var xHasBetterRelation = xRelation > yRelation;

      return xHasWorseRelation ? XIsGreaterThanY : xHasBetterRelation ? XIsLessThanY : XIsEqualToY;
    }

    private bool MeetsAllRequirements(TournamentRecord record) =>
      !HasExistingTournament(record) ||
      (CanOverrideExisting && MeetsRelationRequirements(record));

    private bool MeetsRelationRequirements(TournamentRecord record) =>
      record.IsSettlementPayor ||
      (record.IsHeroPayor && record.FindPayorHero().GetRelation(Payor.Hero) <= MaxRelation);
  }
}