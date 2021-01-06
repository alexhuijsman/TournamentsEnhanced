using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ExistingTournamentPayorComparer : ExistingTournamentComparer
  {
    public ExistingTournamentPayorComparer(Payor payor) : base(payor, true) { }

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
