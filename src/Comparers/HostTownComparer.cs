using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public class HostTownComparer : IComparer<Town>
  {
    public int Compare(Town x, Town y)
    {
      var xRecord = TournamentRecords.GetForTown(x);
      var yRecord = TournamentRecords.GetForTown(y);
      var xType = xRecord.type;
      var yType = yRecord.type;

      return xRecord.
    }
  }
}