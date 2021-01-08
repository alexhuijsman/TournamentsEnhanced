using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class PayorRelationComparer : ExistingTournamentComparer
  {
    public float MaxRelation { get; private set; }

    public PayorRelationComparer(MBHero initiatingHero, float maxRelation) : base(initiatingHero, true)
    {
      MaxRelation = maxRelation;
    }

    public override int Compare(MBSettlement x, MBSettlement y)
    {
      var result = 0;
      //TODO adapt and use this style in all comparers
      if (!TryComparePreconditions(x, y, ref result))
      {
        ComparePayorRelation(x, y, out result);
      }

      return result;
    }

    //TODO move to Hero comparers
    internal bool ComparePayorRelation(MBSettlement x, MBSettlement y, out int result)
    {
      var xHasRecord = ModState.TournamentRecords.ContainsSettlement(x);
      var yHasRecord = ModState.TournamentRecords.ContainsSettlement(y);

      var xRecord = xHasRecord ? ModState.TournamentRecords[x] : default(TournamentRecord);
      var yRecord = yHasRecord ? ModState.TournamentRecords[y] : default(TournamentRecord);

      var xPayorHero = xRecord.FindPayorHero();
      var yPayorHero = yRecord.FindPayorHero();

      var xRelation = xPayorHero.GetRelation(InitiatingHero);
      var yRelation = yPayorHero.GetRelation(InitiatingHero);

      var xHasWorseRelation = xRelation < yRelation;
      var xHasBetterRelation = xRelation > yRelation;

      result = xHasWorseRelation ? XIsGreaterThanY : xHasBetterRelation ? XIsLessThanY : XIsEqualToY;

      return true;
    }
  }
}