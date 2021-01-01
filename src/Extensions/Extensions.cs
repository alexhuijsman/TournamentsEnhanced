using System;

using TournamentsEnhanced.Collections;
using TournamentsEnhanced.Utils;

namespace TournamentsEnhanced
{
  public static class Extensions
  {
    public static W Wrap<W, T>(this T obj)
    {
      var wrapperType = obj.GetWrapperType();
      var wrapper = (W)Reflection.InstantiateByTypeAndArgs(wrapperType);

      return wrapper;
    }

    public static Type GetWrapperType<T>(this T obj)
    {
      return WrapperTypeLookup.GetWrapperTypeFor(obj);
    }
  }
}