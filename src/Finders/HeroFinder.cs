using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class HeroFinder : FinderBase<FindHeroResult, FindHeroOptions, MBHero, Hero>
  {
    public static FindHeroResult FindKingdomLeaders(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates.ToList(),
        Comparers = new IComparer<MBHero>[] { KingdomLeaderHostComparer.Instance }
      };

      return HeroFinder.Find(options);
    }

    public static FindHeroResult FindHostsThatMeetBasicRequirements(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates.ToList(),
        Comparers = new IComparer<MBHero>[] { BasicHostRequirementsComparer.Instance }
      };

      return Find(options);
    }

    public static FindHeroResult FindClanLeaders(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates.ToList(),
        Comparers = new IComparer<MBHero>[] { ClanLeaderHostComparer.Instance }
      };

      return HeroFinder.Find(options);
    }

    public static FindHeroResult FindHostsFromWeddedHeroes(MBHero firstWeddedHero, MBHero secondWeddedHero)
    {
      var maleKingdomLeaderResult = HeroFinder.FindKingdomLeaders(firstWeddedHero, secondWeddedHero);
      var maleClanLeaderResult = HeroFinder.FindClanLeaders(firstWeddedHero, secondWeddedHero);

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

    public static FindHeroResult FindFactionLeaders(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates.ToList(),
        Comparers = new IComparer<MBHero>[] { FactionLeaderHostComparer.Instance }
      };

      return HeroFinder.Find(options);
    }
  }
}
