using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.Abstract
{
  public abstract class MBListBase<W, L> : List<W>
  where L : List<W>
  {
    public bool IsEmpty => Count == 0;
    public L ToList() => (L)new List<W>(this);
    public L Shuffle() => (L)(this as List<W>).Shuffle();
    public W First() => this[0];

    public MBListBase(IEnumerable<W> wrappers) => AddRange(wrappers);
    public MBListBase(W wrapper) => Add(wrapper);
    public MBListBase() { }
  }
}
