using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class HeroFinder : FinderBase<FindHeroResult, FindHeroOptions, MBHero, MBHeroList, Hero>
  {
    public static FindHeroResult FindMaleKingdomLeaderHosts(MBHero initiatingHero, params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates,
        Comparers = new IComparer<MBHero>[] { new MaleKingdomLeaderHostComparer(initiatingHero) }
      };

      return HeroFinder.Find(options);
    }

    public static FindHeroResult FindHostsThatMeetBasicRequirements(MBHero initiatingHero, params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates,
        Comparers = new IComparer<MBHero>[] { new BasicHostRequirementsHeroComparer(initiatingHero) }
      };

      return Find(options);
    }

    public static FindHeroResult FindMaleClanLeaderHosts(MBHero initiatingHero, params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates,
        Comparers = new IComparer<MBHero>[] { new MaleClanLeaderHostComparer(initiatingHero) }
      };

      return HeroFinder.Find(options);
    }

    public static FindHeroResult FindHostsFromWeddedHeroes(MBHero firstWeddedHero, MBHero secondWeddedHero)
    {

      //TODO clan leader from current town should host tournament, if not, then do below
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
