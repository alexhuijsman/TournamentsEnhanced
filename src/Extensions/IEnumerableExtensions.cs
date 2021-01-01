using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced
{
  static class IEnumerableExtensions
  {
    public static List<T> ToList<T>(this IEnumerable<T> enumerable)
    {
      return new List<T>(enumerable);
    }

    public static List<T> Unwrap<W, T>(this IEnumerable<W> wrappedObjects)
        where W : CachedWrapperBase<W, T>, new()
    {
      return wrappedObjects.ToList().Unwrap<W, T>();
    }

    public static List<W> Wrap<W, T>(this IEnumerable<T> objects)
    where W : CachedWrapperBase<W, T>, new()
    {
      return objects.ToList().Wrap<W, T>();
    }

    private static W WrapConverter<T, W>(this T obj) where W : CachedWrapperBase<W, T>, new() => CachedWrapperBase<W, T>.GetWrapperFor(obj);
    private static T UnwrapConverter<W, T>(this W wrapper) where W : CachedWrapperBase<W, T>, new() => wrapper.UnwrappedObject;
  }
}
