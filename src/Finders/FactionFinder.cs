using System.Collections.Generic;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Faction;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FactionFinder
    : FinderBase<FindFactionResult, FindFactionOptions, MBFaction, MBFactionImpl>
  {
    public static FindFactionResult FindFactionThatMeetBasicHostRequirements(MBFaction faction)
    {
      var candidiates = new List<MBFaction>();
      candidiates.Add(faction);

      var options = new FindFactionOptions()
      {
        Candidates = candidiates,
        Comparers = new IComparer<MBFaction>[]
        {
          BasicHostRequirementsComparer.Instance
        }
      };

      return Find(options);
    }
  }
}
