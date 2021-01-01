using System.Collections.Generic;

using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  public class HostTownComparer : IComparer<MBTown>
  {
    public int Compare(MBTown x, MBTown y)
    {
      //TODO Compare payor and different comparers depending on tournament type
      var xRecord = ModState.TournamentRecords[x];
      var yRecord = ModState.TournamentRecords[y];
      var xType = xRecord.type;
      var yType = yRecord.type;

      return xRecord.
    }
  }
}