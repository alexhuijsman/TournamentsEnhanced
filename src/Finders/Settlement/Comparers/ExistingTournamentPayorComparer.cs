using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ExistingTournamentPayorComparer : HostSettlementComparerBase
  {
    public ExistingTournamentPayorComparer(Payor payor) : base(payor) { }

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

      var payorIsKingdomLeader = Payor.Hero.IsFactionLeader;

      // if payor is kingdom leader and x/y payor is not kingdom leader
      // if payor is not kingdom leader but x/y payor is settlement
      // 

      var xOwner = x.ClanLeader;
      var yOwner = y.ClanLeader;

      return xMeetsRequirements ? yMeetsRequirements ? XIsEqualToY : XIsGreaterThanY : XIsLessThanY;
    }

    private bool MeetsAllRequirements(TournamentRecord record) => HasExistingTournament(record) &&
                                                                  PayorIsNotSameAs(record) &&
                                                                  PayorOutranksPayorOf(record);
  }
}
