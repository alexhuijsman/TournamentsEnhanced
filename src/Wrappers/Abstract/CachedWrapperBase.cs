using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.Abstract
{
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
        UnwrapedObject = obj
      };

      return wrapper;
    }

    public static WW Wrap<WW, TT>(TT obj)
      where WW : WrapperBase<TT>, new()
    {
      return CachedWrapperBase<WW, TT>.GetWrapperFor(obj);
    }
    public static TT Unwrap<WW, TT>(WW wrapper)
      where WW : WrapperBase<TT>, new()
    {
      return wrapper.UnwrapedObject;
    }
  }
}