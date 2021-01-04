using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder
{
  public interface IFindOptionsBase<W, L>
  where W : IWrapperBase
  where L : MBListBase<W, L>
  {
    L Candidates { get; }
    IComparer<W>[] Comparers { get; }
  }
}