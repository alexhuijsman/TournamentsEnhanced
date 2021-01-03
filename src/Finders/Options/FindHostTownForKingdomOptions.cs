using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced
{
  public class FindHostTownForKingdomOptions : FindHostTownOptions
  {
    public readonly MBKingdom Kingdom;

    public FindHostTownForKingdomOptions(MBKingdom kingdom, IComparer<MBTown> comparer = null) : base(kingdom.Settlements, comparer)
    {
      Kingdom = kingdom;
    }
  }
}
