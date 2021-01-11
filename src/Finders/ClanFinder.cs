using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Clan;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class ClanFinder : FinderBase<FindClanResult, FindClanOptions, MBClan, Clan>
  {
    public static FindClanResult FindClanThatMeetsBasicHostRequirements(MBClan clan)
    {
      var candidiates = new List<MBClan>();
      candidiates.Add(clan);

      var options = new FindClanOptions()
      {
        Candidates = candidiates,
        Comparers = new IComparer<MBClan>[]
        {
          BasicHostRequirementsComparer.Instance
        }
      };

      return Find(options);
    }
  }
}
