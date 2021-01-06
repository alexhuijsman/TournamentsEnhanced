using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ExistingTournamentPayorComparer : ExistingTournamentComparer
  {
    public ExistingTournamentPayorComparer(Payor payor) : base(payor, true) { }

    public override int Compare(MBSettlement x, MBSettlement y)
    {
      var result = 0;

      var wasResultAssigned =
        TryComparePreconditions(x, y, ref result);

      return result;
    }

    internal override bool MeetsRequirements(MBSettlement settlement)
    {
      var hasRecord = ModState.TournamentRecords.ContainsSettlement(settlement);
      var record = hasRecord ? ModState.TournamentRecords[settlement] : default(TournamentRecord);

      return HasExistingTournament(settlement) &&
             PayorIsSameAs(record) &&
             PayorOutranksPayorOf(record);
    }
  }
}
