using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class HeroFinder : FinderBase<FindHeroResult, FindHeroOptions, MBHero, Hero>
  {
    public static HeroFinder Instance { get; } = new HeroFinder();

    protected HeroFinder() { }
    public virtual FindHeroResult FindKingdomLeaders(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates.ToList(),
        Comparers = new IComparer<MBHero>[] { KingdomLeaderHostComparer.Instance }
      };

      return Find(options);
    }

    public virtual FindHeroResult FindHostsThatMeetBasicRequirements(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates.ToList(),
        Comparers = new IComparer<MBHero>[] { BasicHeroHostRequirementsComparer.Instance }
      };

      return Find(options);
    }

    public virtual FindHeroResult FindClanLeaders(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates.ToList(),
        Comparers = new IComparer<MBHero>[] { ClanLeaderHostComparer.Instance }
      };

      return Find(options);
    }

    public virtual FindHeroResult FindHostsFromWeddedHeroes(MBHero firstWeddedHero, MBHero secondWeddedHero)
    {
      var kingdomLeaderResult = FindKingdomLeaders(firstWeddedHero, secondWeddedHero);
      var clanLeaderResult = FindClanLeaders(firstWeddedHero, secondWeddedHero);

      var primaryHostHero =
          kingdomLeaderResult.Succeeded ?
            kingdomLeaderResult.Nominee :
            clanLeaderResult.Succeeded ?
              clanLeaderResult.Nominee :
              null;

      var firstIsKingdomLeader = kingdomLeaderResult.AllQualifiedCandidates.Contains(firstWeddedHero);
      var secondIsKingdomLeader = kingdomLeaderResult.AllQualifiedCandidates.Contains(secondWeddedHero);

      var firstIsClanLeader = clanLeaderResult.AllQualifiedCandidates.Contains(firstWeddedHero);
      var secondIsClanLeader = clanLeaderResult.AllQualifiedCandidates.Contains(secondWeddedHero);

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

      var didFindHost = kingdomLeaderResult.Succeeded || clanLeaderResult.Succeeded;

      return didFindHost ? FindHeroResult.Success(primaryHostHero, secondaryHostHero) : FindHeroResult.Failure;
    }

    public virtual FindHeroResult FindFactionLeaders(params MBHero[] candidates)
    {
      var options = new FindHeroOptions()
      {
        Candidates = candidates.ToList(),
        Comparers = new IComparer<MBHero>[] { FactionLeaderHostComparer.Instance }
      };

      return Find(options);
    }
  }
}
