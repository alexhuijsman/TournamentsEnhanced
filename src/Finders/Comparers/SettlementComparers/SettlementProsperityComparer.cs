using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced.Finder.Comparer
{
  public class SettlementProsperityComparer : SettlementComparerBase
  {
    public override int Compare(MBTown x, MBTown y)
    {
      //TODO USe multiple comparers at once such as payor and different comparers depending on tournament type
      var xRecord = ModState.TournamentRecords[x];
      var yRecord = ModState.TournamentRecords[y];
      var xProsperity = x.GetProsperityLevel();
      var yProsperity = y.GetProsperityLevel();

      var result = xProsperity.CompareTo(yProsperity);

      return xProsperity.CompareTo(yProsperity);
    }
  }
}