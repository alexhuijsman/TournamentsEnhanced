using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.Abstract
{
  public abstract class MBListBase<W, L> : List<W>
  where L : List<W>
  {
    public L ToList() => (L)new List<W>(this);
  }
}
