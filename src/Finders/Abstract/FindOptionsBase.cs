using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder
{
  public class FindOptionsBase<W, L>
  where W : IWrapperBase
  where L : MBListBase<W, L>
  {
    public L Candidates { get; set; }
    public IComparer<W>[] Comparers { get; set; }
  }
}