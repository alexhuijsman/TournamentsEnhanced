using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  public class FindHostTownOptions
  {
    public readonly IComparer<MBTown> Comparer;
    public readonly IEnumerable<MBSettlement> Settlements;

    public FindHostTownOptions(IEnumerable<MBSettlement> settlements, IComparer<MBTown> comparer = null)
    {
      Settlements = settlements;
      Comparer = comparer ?? new HostTownComparer();
    }
  }
}
