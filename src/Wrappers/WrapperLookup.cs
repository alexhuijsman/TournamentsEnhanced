using System;
using System.Collections.Generic;
using TournamentsEnhanced.Utils;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class WrapperTypeDictionary : Dictionary<Type, Type> { public WrapperTypeDictionary(int count) : base(count) { } }
  public class WrapperInstanceDictionary : Dictionary<Type, IWrapperBase> { public WrapperInstanceDictionary(int count) : base(count) { } }

  public static partial class WrapperLookup
  {
    private static WrapperTypeDictionary WrappedTypeDictionary { get; }
    private static WrapperInstanceDictionary WrapperInstanceDictionary { get; }

    static WrapperLookup()
    {
      WrappedTypeDictionary = BuildWrappedTypeDictionary();
    }

    private static WrapperTypeDictionary BuildWrappedTypeDictionary()
    {
      var wrappedTypes = Reflection.GetAllImplementingTypesOfBaseClassFrom<WrapperBaseImpl>();
      var result = new WrapperTypeDictionary(wrappedTypes.Count);

      Type unwrappedType;
      foreach (var wrappedType in wrappedTypes)
      {
        unwrappedType = Reflection.GetUnwrappedTypeFor(wrappedType);

        WrappedTypeDictionary.Add(unwrappedType, wrappedType);
      }

      return result;
    }

    public static Type GetWrapperTypeFor<T>(T obj)
    {
      var type = obj.GetType();

      return WrappedTypeDictionary[type];
    }

    public static W GetWrapperFor<T, W>(T obj)
      where W : WrapperBase<W, T>, new()
    {
      return CachedWrapperBase<W, T>.GetWrapperFor(obj);
    }
  }
}