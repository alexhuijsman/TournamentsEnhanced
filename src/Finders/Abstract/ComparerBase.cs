using System.Collections.Generic;

namespace TournamentsEnhanced.Finder.Comparers.Abstract
{
  public abstract class ComparerBase<T> : IComparer<T>
  {
    public static readonly int XIsGreaterThanY = 1;
    public static readonly int XIsEqualToY = 0;
    public static readonly int XIsLessThanY = -1;

    public abstract int Compare(T x, T y);
  }
}