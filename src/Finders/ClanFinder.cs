using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Clan;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class ClanFinder : FinderBase<FindClanResult, FindClanOptions, MBClan, Clan>
  {
    public static ClanFinder Instance { get; } = new ClanFinder();

    private ClanFinder() { }
    public FindClanResult FindClanThatMeetsBasicHostRequirements(MBClan clan)
    {
      var candidiates = new List<MBClan>();
      candidiates.Add(clan);

      var options = new FindClanOptions()
      {
        Candidates = candidiates,
        Comparers = new IComparer<MBClan>[]
        {
          BasicClanHostRequirementsComparer.Instance
        }
      };

      return Find(options);
    }
  }
}
