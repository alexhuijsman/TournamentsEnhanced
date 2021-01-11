using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.Abstract
{
  public abstract class CachedWrapperBase<W, T> : WrapperBase<W, T>
  where W : WrapperBase<W, T>, new()
  {
    private static readonly IDictionary<T, W> Cache = new Dictionary<T, W>();

    public CachedWrapperBase() : base() { }
    public CachedWrapperBase(T obj) : base(obj) { }

    public static W GetWrapper(T obj)
    {
      if (!Cache.ContainsKey(obj))
      {
        var wrapper = InstantiateWrapperForObject(obj);

        Cache.Add(obj, wrapper);
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

    public static WW Wrap<WW, TT>(TT obj)
      where WW : WrapperBase<WW, TT>, new()
    {
      return CachedWrapperBase<WW, TT>.GetWrapper(obj);
    }

    public static TT Unwrap<WW, TT>(WW wrapper)
      where WW : WrapperBase<WW, TT>, new()
    {
      return wrapper.UnwrappedObject;
    }
  }
}