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

    public static FindHeroResult FindHostsFromWeddedHeroes(MBHero firstWeddedHero, MBHero secondWeddedHero)
    {
      var maleKingdomLeaderResult = HeroFinder.FindMaleKingdomLeaderHosts(firstWeddedHero, secondWeddedHero);
      var maleClanLeaderResult = HeroFinder.FindMaleClanLeaderHosts(firstWeddedHero, secondWeddedHero);

      var primaryHostHero =
          maleKingdomLeaderResult.Succeeded ?
            maleKingdomLeaderResult.Nominee :
            maleClanLeaderResult.Succeeded ?
              maleClanLeaderResult.Nominee :
              null;

      var firstIsKingdomLeader = maleKingdomLeaderResult.AllQualifiedCandidates.Contains(firstWeddedHero);
      var secondIsKingdomLeader = maleKingdomLeaderResult.AllQualifiedCandidates.Contains(secondWeddedHero);

      var firstIsClanLeader = maleClanLeaderResult.AllQualifiedCandidates.Contains(firstWeddedHero);
      var secondIsClanLeader = maleClanLeaderResult.AllQualifiedCandidates.Contains(secondWeddedHero);

      var firstIsPrimaryHost = primaryHostHero == firstWeddedHero;
      var secondIsPrimaryHost = primaryHostHero == secondWeddedHero;

      var secondaryHostHero =
        firstIsPrimaryHost ?
          (secondIsKingdomLeader || secondIsClanLeader) ?
            secondWeddedHero :
            null :
          (firstIsKingdomLeader || firstIsClanLeader) ?
            firstWeddedHero :
            null;

      var didFindHost = maleKingdomLeaderResult.Succeeded || maleClanLeaderResult.Succeeded;

      return didFindHost ? FindHeroResult.Success(primaryHostHero, secondaryHostHero) : FindHeroResult.Failure;
    }
  }
}
