using System;
using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.Abstract
{
  public abstract class CachedWrapperBase<W, T> : WrapperBase<W, T>
  where W : WrapperBase<W, T>, new()
  {
    private static readonly IDictionary<T, WeakReference<W>> WeakReferences = new Dictionary<T, WeakReference<W>>();

    public CachedWrapperBase() : base() { }
    public CachedWrapperBase(T obj) : base(obj) { }

    public static W GetWrapper(T obj)
    {
      W wrapper;

      if (!WeakReferences.ContainsKey(obj) || !WeakReferences[obj].TryGetTarget(out wrapper))
      {
        wrapper = InstantiateWrapperForObject(obj);
        WeakReferences.Add(obj, new WeakReference<W>(wrapper, false));
      }

      return wrapper;
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