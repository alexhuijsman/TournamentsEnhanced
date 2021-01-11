using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder
{
  public class FindOptionsBase<W>
  where W : IWrapperBase
  {
    public List<W> Candidates { get; set; }
    public IComparer<W>[] Comparers { get; set; }
    public IComparer<W>[] FallbackComparers { get; set; }

    public bool HasFallbackComparers => FallbackComparers != null;
  }
}