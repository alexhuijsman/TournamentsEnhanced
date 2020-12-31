using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers
{
  public class CachedWrapper<W, T> : Wrapper<T>
  where W : Wrapper<T>, new()
  {
    private static readonly IDictionary<T, W> Cache = new Dictionary<T, W>();

    public CachedWrapper() : base() { }
    public CachedWrapper(T obj) : base(obj) { }

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
      var wrapper = new W();
      wrapper.Wrap(obj);

      return wrapper;
    }
  }
}