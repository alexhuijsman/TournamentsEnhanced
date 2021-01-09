using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder.Abstract
{
  public class FinderBase<R, O, W, L, T>
    where R : FindResultBase<R, W, L, T>, new()
    where O : FindOptionsBase<W, L>
    where W : MBWrapperBase<W, T>, new()
    where L : MBListBase<W, L>
  {
    public static R Find(O options)
    {
      L remainingCandidates;

      remainingCandidates = SortAndFilterByComparers(options.Candidates.ToList(), options.Comparers);

      if (remainingCandidates.IsEmpty && options.HasFallbackComparers)
      {
        remainingCandidates = SortAndFilterByComparers(options.Candidates.ToList(), options.FallbackComparers);
      }

      return remainingCandidates.IsEmpty ?
        FindResultBase<R, W, L, T>.Failure :
        FindResultBase<R, W, L, T>.Success(remainingCandidates);
    }

    protected static L SortAndFilterByComparers(L candidates, IComparer<W>[] comparers)
    {
      candidates.Add(MBWrapperBase<W, T>.Null);

      foreach (var comparer in comparers)
      {
        candidates.Sort(comparer);
        RemoveCandidatesRankedLowerThanNull(candidates);
      }

      candidates.Remove(MBWrapperBase<W, T>.Null);

      return candidates;
    }

    private static void RemoveCandidatesRankedLowerThanNull(L candidates)
    {
      var nullIndexSearchResult = GetNullIndex(candidates);
      var nullIndex = nullIndexSearchResult.indexValue;

      if (!nullIndexSearchResult.wasSuccessful)
      {
        return;
      }

      var numRecordsToRemove = candidates.Count - nullIndex - 1;

      candidates.RemoveRange(nullIndex + 1, numRecordsToRemove);

    }

    private static NullIndexSearchResult GetNullIndex(L candidates)
    {
      int nullIndex = -1;
      for (int i = 0; i < candidates.Count; i++)
      {
        if (candidates[i] != MBWrapperBase<W, T>.Null)
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
