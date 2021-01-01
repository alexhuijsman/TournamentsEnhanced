using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.Abstract
{

  public interface ICachedWrapperBase : IWrapperBase
  {

  }

  public abstract class CachedWrapperBase : WrapperBase, ICachedWrapperBase
  {
    public static W Wrap<T, W>(T obj)
      where W : CachedWrapperBase<W, T>, new()
    {
      return CachedWrapperBase<W, T>.GetWrapperFor(obj);
    }
    public static T Unwrap<W, T>(W wrapper)
      where W : CachedWrapperBase<W, T>, new()
    {
      return wrapper.UnwrappedObject;
    }
  }

  public abstract class CachedWrapperBase<W, T> : WrapperBase<T>
  where W : WrapperBase<T>, new()
  {
    private static readonly IDictionary<T, W> Cache = new Dictionary<T, W>();

    public CachedWrapperBase() : base() { }
    public CachedWrapperBase(T obj) : base(obj) { }

    public static W GetWrapperFor(T obj)
    {
      if (!Cache.ContainsKey(obj))
      {
        var wrapper = InstantiateWrapperForObject(obj);

        Cache.Add(obj, new W());
      }

      return Cache[obj];
    }

    private static W InstantiateWrapperForObject(T obj)
    {
      var wrapper = new W()
      {
        UnwrappedObject = obj
      };

      return wrapper;
    }
  }
}