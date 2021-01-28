using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Finder.Abstract
{
  public class FindOptionsBase<W>
  where W : WrapperBase
  {
    public virtual List<W> Candidates { get; set; }
    public virtual IComparer<W>[] Comparers { get; set; }
    public virtual IComparer<W>[] FallbackComparers { get; set; }

    public virtual bool HasFallbackComparers => FallbackComparers != null && FallbackComparers.Length > 0;
  }
}