using System;

using TournamentsEnhanced.Wrappers;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced
{
  public static class Extensions
  {
    public static W Wrap<W, T>(this T obj)
      where W : CachedWrapperBase<W, T>, new()
    {
      return CachedWrapperBase<W, T>.GetWrapperFor(obj);
    }

    public static Type GetWrapperType<T>(this T obj)
    {
      return WrapperLookup.GetWrapperTypeFor(obj);
    }
  }
}