using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public static class HeroFinder
  {
    public static FindHeroResult FindHero(FindHeroOptions options)
    {
      var validHeroes = options.Candidates.ToList();
      SortAndFilterByComparers(validHeroes, options.Comparers);

      return validHeroes.IsEmpty ?
        FindHeroResult.Failure() :
        FindHeroResult.Success(validHeroes.First());
    }

    private static MBHeroList SortAndFilterByComparers(MBHeroList heroes, IComparer<MBHero>[] comparers)
    {
      heroes.Add(MBHero.Null);

      foreach (var comparer in comparers)
      {
        heroes.Sort(comparer);
        RemoveCandidatesRankedLowerThanNull(heroes);
      }

      heroes.Remove(MBHero.Null);

      return heroes;
    }

    private static void RemoveCandidatesRankedLowerThanNull(MBHeroList heroes)
    {
      var nullIndexSearchResult = GetNullIndexOfCandidates(heroes);
      var nullIndex = nullIndexSearchResult.indexValue;

      if (!nullIndexSearchResult.wasSuccessful)
      {
        return;
      }

      var numRecordsToRemove = heroes.Count - nullIndex - 1;

      heroes.RemoveRange(nullIndex + 1, numRecordsToRemove);

    }

    private static NullIndexSearchResult GetNullIndexOfCandidates(MBHeroList heroes)
    {
      int nullIndex = -1;
      for (int i = 0; i < heroes.Count; i++)
      {
        if (heroes[i] != MBHero.Null)
        {
          continue;
        }

        nullIndex = i;
        break;
      }

      return new NullIndexSearchResult() { wasSuccessful = nullIndex >= 0, indexValue = nullIndex };
    }

    private struct NullIndexSearchResult
    {
      public bool wasSuccessful;
      public int indexValue;
    }
  }
}
