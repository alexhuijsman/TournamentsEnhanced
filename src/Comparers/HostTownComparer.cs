using System.Collections.Generic;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  public class HostTownComparer : IComparer<MBTown>
  {
    public int Compare(MBTown x, MBTown y)
    {
      var xRecord = TournamentRecords.GetForTown(x);
      var yRecord = TournamentRecords.GetForTown(y);
      var xType = xRecord.type;
      var yType = yRecord.type;

      return xRecord.
    }
  }
}