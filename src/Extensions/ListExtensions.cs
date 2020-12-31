using System;
using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  static class ListExtensions
  {
    public static List<T> Shuffle<T>(this List<T> list)
    {
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = MBRandom.DeterministicRandomInt(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }

      return list;
    }

    public static List<T> UnwrapAll<W, T>(this List<W> wrappedObjects)
    where W : CachedWrapper<W, T>, new()
    {
      var converter = new Converter<W, T>(UnwrapConverter<W, T>);
      return wrappedObjects.ConvertAll<T>(converter);
    }

    public static List<W> WrapAll<T, W>(this List<T> objects)
    where W : CachedWrapper<W, T>, new()
    {
      var converter = new Converter<T, W>(WrapConverter<T, W>);
      return objects.ConvertAll<W>(converter);
    }

    private static W WrapConverter<T, W>(this T obj) where W : CachedWrapper<W, T>, new() => CachedWrapper<W, T>.GetWrapperFor(obj);
    private static T UnwrapConverter<W, T>(this W wrapper) where W : CachedWrapper<W, T>, new() => wrapper.Unwrap();
  }
}
