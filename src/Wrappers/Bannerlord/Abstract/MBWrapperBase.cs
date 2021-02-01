namespace TournamentsEnhanced.Wrappers.Abstract
{
  public abstract class MBWrapperBase<W, T> : CachedWrapperBase<W, T>
  where W : CachedWrapperBase<W, T>, new()
  where T : class
  {
    public MBWrapperBase() { }
    public MBWrapperBase(T obj) : base(obj) { }
  }
}