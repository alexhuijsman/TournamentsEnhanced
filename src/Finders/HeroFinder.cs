using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class HeroFinder : FinderBase<FindHeroResult, FindHeroOptions, MBHero, MBHeroList, Hero>
  {
    public static FindHeroResult FindMaleKingdomLeaderHosts(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates,
        Comparers = new IComparer<MBHero>[] { new MaleKingdomLeaderHostComparer() }
      };

      return HeroFinder.Find(options);
    }

    public static FindHeroResult FindHostsThatMeetBasicRequirements(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates,
        Comparers = new IComparer<MBHero>[] { new BasicHostRequirementsHeroComparer() }
      };

      return Find(options);
    }

    public static FindHeroResult FindMaleClanLeaderHosts(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates,
        Comparers = new IComparer<MBHero>[] { new MaleClanLeaderHostComparer() }
      };

      return HeroFinder.Find(options);
    }
  }
}
