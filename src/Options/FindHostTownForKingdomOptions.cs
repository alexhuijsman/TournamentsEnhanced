using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  public class FindHostTownForKingdomOptions : FindHostTownOptions
  {
    public readonly MBKingdom Kingdom;

    public FindHostTownForKingdomOptions(MBKingdom kingdom, IComparer<Town> comparer = null) : base(kingdom.Settlements.ToList(), comparer)
    {
      Kingdom = kingdom;
    }
  }
}
