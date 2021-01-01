using System;
using System.Collections.Generic;

using TournamentsEnhanced.Utils;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Collections
{
  public class WrapperTypeDictionary : Dictionary<Type, Type> { public WrapperTypeDictionary(int count) : base(count) { } }
  public class WrapperInstanceDictionary : Dictionary<Type, IWrapperBase> { public WrapperInstanceDictionary(int count) : base(count) { } }

  public static partial class WrapperTypeLookup
  {
    private static WrapperTypeDictionary WrappedTypeDictionary { get; }
    private static WrapperInstanceDictionary WrapperInstanceDictionary { get; }

    static WrapperTypeLookup()
    {
      WrappedTypeDictionary = BuildWrappedTypeDictionary();
    }

    private static WrapperTypeDictionary BuildWrappedTypeDictionary()
    {
      var wrappedTypes = Reflection.GetAllImplementingTypesOfBaseClassFrom<AbstractWrapperImpl>();
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
      var instance = WrapperInstanceDictionary[type] ?? Reflection.InstantiateByTypeAndArgs(type, null);
      WrappedTypeDictionary[type];
    }
  }
}