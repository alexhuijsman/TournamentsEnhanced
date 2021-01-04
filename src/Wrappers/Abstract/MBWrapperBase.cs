using TaleWorlds.ObjectSystem;

namespace TournamentsEnhanced.Wrappers.Abstract
{
  public abstract class MBWrapperBase<W, T> : CachedWrapperBase<W, T>
  where W : WrapperBase<T>, new()
  { }
}