using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Kingdom;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class KingdomFinder
    : FinderBase<FindKingdomResult, FindKingdomOptions, MBKingdom, MBKingdomList, Kingdom>
  {
    public static FindKingdomResult FindKingdomThatMeetBasicHostRequirements(MBKingdom faction)
    {
      var options = new FindKingdomOptions()
      {
        Candidates = new MBKingdomList(faction),
        Comparers = new IComparer<MBKingdom>[]
        {
          BasicHostRequirementsComparer.Instance
        }
      };

      return Find(options);
    }
  }
}
