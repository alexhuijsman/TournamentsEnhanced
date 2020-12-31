using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public class FindHostTownOptions
  {
    public readonly IComparer<Town> Comparer;
    public readonly IList<Settlement> Settlements;

    public FindHostTownOptions(IList<Settlement> settlements, IComparer<Town> comparer = null)
    {
      Settlements = settlements;
      Comparer = comparer ?? new HostTownComparer();
    }
  }
}
