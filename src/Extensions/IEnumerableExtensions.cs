using System.Collections.Generic;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  static class IEnumerableExtensions
  {
    public static List<T> ToList<T>(this IEnumerable<T> enumerable)
    {
      return new List<T>(enumerable);
    }

    public static List<T> UnwrapAll<W, T>(this IEnumerable<W> wrappedObjects)
        where W : CachedWrapperBase<W, T>, new()
    {
      return wrappedObjects.ToList().UnwrapAll<W, T>();
    }

    public static List<W> WrapAll<W, T>(this IEnumerable<T> objects)
    where W : CachedWrapperBase<W, T>, new()
    {
      return objects.ToList().WrapAll<T, W>();
    }

    private static W WrapConverter<T, W>(this T obj) where W : CachedWrapperBase<W, T>, new() => CachedWrapperBase<W, T>.GetWrapperFor(obj);
    private static T UnwrapConverter<W, T>(this W wrapper) where W : CachedWrapperBase<W, T>, new() => wrapper.Unwrap();
  }
}
