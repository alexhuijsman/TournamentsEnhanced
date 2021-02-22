using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Clan;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class ClanFinder : FinderBase<FindClanResult, FindClanOptions, MBClan, Clan>
  {
    protected BasicClanHostRequirementsComparer BasicClanHostRequirementsComparer { get; set; }
      = BasicClanHostRequirementsComparer.Instance;
    public static ClanFinder Instance { get; } = new ClanFinder();

    protected ClanFinder() { }

    public FindClanResult FindClanThatMeetsBasicHostRequirements(MBClan clan)
    {
      var candidiates = new List<MBClan>();
      candidiates.Add(clan);

      var options = new FindClanOptions()
      {
        Candidates = candidiates,
        Comparers = new IComparer<MBClan>[]
        {
          BasicClanHostRequirementsComparer
        }
      };

      return Find(options);
    }
  }
}
