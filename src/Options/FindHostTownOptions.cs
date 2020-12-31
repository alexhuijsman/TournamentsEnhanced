using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public struct FindHostTownOptions
  {
    public readonly IComparer<Town> TownComparer;

    public FindHostTownOptions(IComparer<Town> townComparer = null)
    {
      TownComparer = townComparer;
    }
  }
}
