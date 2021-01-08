using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Clan;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class ClanFinder : FinderBase<FindClanResult, FindClanOptions, MBClan, MBClanList, Clan>
  {
    public static FindClanResult FindClanThatMeetsBasicHostRequirements(MBClan faction, MBHero initiatingHero)
    {
      var options = new FindClanOptions()
      {
        Candidates = new MBClanList(faction),
        Comparers = new IComparer<MBClan>[]
        {
          new BasicHostRequirementsClanComparer(initiatingHero)
        }
      };

      return Find(options);
    }
  }
}
