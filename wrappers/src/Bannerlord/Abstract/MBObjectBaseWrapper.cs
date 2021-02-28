using TaleWorlds.ObjectSystem;

namespace TournamentsEnhanced.Wrappers.Abstract
{
  public abstract class MBObjectBaseWrapper<W, T> : MBWrapperBase<W, T>
  where W : CachedWrapperBase<W, T>, new()
  where T : MBObjectBase
  {
    public virtual string StringId => UnwrappedObject.StringId;
  }
}