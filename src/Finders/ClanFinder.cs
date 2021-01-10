using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Clan;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class ClanFinder : FinderBase<FindClanResult, FindClanOptions, MBClan, MBClanList, Clan>
  {
    public static FindClanResult FindClanThatMeetsBasicHostRequirements(MBClan faction)
    {
      var options = new FindClanOptions()
      {
        Candidates = new MBClanList(faction),
        Comparers = new IComparer<MBClan>[]
        {
          BasicHostRequirementsComparer.Instance
        }
      };

      return Find(options);
    }
  }
}
