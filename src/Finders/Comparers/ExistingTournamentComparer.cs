using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public class ExistingTournamentComparer : HostSettlementComparerBase
  {
    public bool CanOverrideExisting { get; private set; }

    public ExistingTournamentComparer(Payor payor, bool canOverrideExisting) : base(payor)
    {
      CanOverrideExisting = canOverrideExisting;
    }

    public override int Compare(MBSettlement x, MBSettlement y)
    {
      var xHasRecord = ModState.TournamentRecords.ContainsSettlement(x);
      var yHasRecord = ModState.TournamentRecords.ContainsSettlement(y);

      var xRecord = xHasRecord ? ModState.TournamentRecords[x] : default(TournamentRecord);
      var yRecord = yHasRecord ? ModState.TournamentRecords[y] : default(TournamentRecord);

      var xMeetsRequirements = !x.IsNull && MeetsRequirements(xRecord);

      var yMeetsRequirements = !y.IsNull && MeetsRequirements(yRecord);

      return xMeetsRequirements ? yMeetsRequirements ? XIsEqualToY : XIsGreaterThanY : XIsLessThanY;
    }

    private bool MeetsRequirements(TournamentRecord record) =>
      CanOverrideExisting || record.tournamentType == TournamentType.None;
  }
}
