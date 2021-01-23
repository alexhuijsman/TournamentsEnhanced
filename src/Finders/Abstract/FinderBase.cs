using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder.Abstract
{
  public class FinderBase<R, O, W, T>
    where R : FindResultBase<R, W, T>, new()
    where O : FindOptionsBase<W>
    where W : MBWrapperBase<W, T>, new()
    where T : class
  {
    public R Find(O options)
    {
      List<W> remainingCandidates;

      remainingCandidates = SortAndFilterByComparers(options.Candidates.ToList(), options.Comparers);

      if (remainingCandidates.IsEmpty() && options.HasFallbackComparers)
      {
        remainingCandidates = SortAndFilterByComparers(options.Candidates.ToList(), options.FallbackComparers);
      }

      return remainingCandidates.IsEmpty() ?
        FindResultBase<R, W, T>.Failure :
        FindResultBase<R, W, T>.Success(remainingCandidates);
    }

    private List<W> SortAndFilterByComparers(List<W> candidates, IComparer<W>[] comparers)
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

    private void RemoveCandidatesRankedLowerThanNull(List<W> candidates)
    {
      var nullIndexSearchResult = GetNullIndex(candidates);
      var nullIndex = nullIndexSearchResult.indexValue;

      if (!nullIndexSearchResult.wasSuccessful)
      {
        return;
      }

      candidates.RemoveRange(0, nullIndex + 1);

    }

    private NullIndexSearchResult GetNullIndex(List<W> candidates)
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
