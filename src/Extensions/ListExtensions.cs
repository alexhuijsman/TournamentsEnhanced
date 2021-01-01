using System;
using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

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

    public static List<T> Unwrap<W, T>(this List<W> wrappedObjects)
    where W : CachedWrapperBase<W, T>, new()
    {
      var converter = new Converter<W, T>(CachedWrapperBase<W, T>.Unwrap<W, T>);

      return wrappedObjects.ConvertAll<T>(converter);
    }

    public static List<W> Wrap<W, T>(this List<T> objects)
    where W : CachedWrapperBase<W, T>, new()
    {
      var converter = new Converter<T, W>(CachedWrapperBase<W, T>.Wrap<W, T>);

      return objects.ConvertAll<W>(converter);
    }
  }
}
