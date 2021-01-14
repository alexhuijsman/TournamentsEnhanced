using System;
using System.Runtime.CompilerServices;

namespace TournamentsEnhanced.Wrappers.Abstract
{
  public abstract class CachedWrapperBase<W, T> : WrapperBase<W, WeakReference<T>>
  where W : CachedWrapperBase<W, T>, new()
  where T : class
  {
    private static readonly ConditionalWeakTable<T, W> CachedWrappers = new ConditionalWeakTable<T, W>();

    public new T UnwrappedObject
    {
      get
      {
        T unwrappedObject = null;

        base.UnwrappedObject.TryGetTarget(out unwrappedObject);

        return unwrappedObject;
      }

      set
      {
        base.UnwrappedObject = new WeakReference<T>(value);
      }
    }


    protected CachedWrapperBase() : base() { }
    protected CachedWrapperBase(T obj) : base(new WeakReference<T>(obj)) { }

    public static W GetWrapper(T obj)
    {
      W wrapper;
      if (!CachedWrappers.TryGetValue(obj, out wrapper))
      {
        wrapper = InstantiateWrapperForObject(obj);
        CachedWrappers.Add(obj, wrapper);
      }

      return wrapper;
    }

    private static W InstantiateWrapperForObject(T obj)
    {
      return new W() { UnwrappedObject = obj };
    }
  }
}