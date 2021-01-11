using System.Collections.Generic;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Faction;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FactionFinder
    : FinderBase<FindFactionResult, FindFactionOptions, MBFaction, MBFactionList, MBFactionImpl>
  {
    public static FindFactionResult FindFactionThatMeetBasicHostRequirements(MBFaction faction)
    {
      var options = new FindFactionOptions()
      {
        Candidates = new MBFactionList(faction),
        Comparers = new IComparer<MBFaction>[]
        {
          BasicHostRequirementsComparer.Instance
        }
      };

      return Find(options);
    }
  }
}
