using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

    private static class Reflection
    {
      public static List<Type> GetAllImplementingTypesOfBaseClassFrom<T>()
      {
        return (from x in Assembly.GetAssembly(typeof(T)).GetTypes()
                let y = x.BaseType
                where !x.IsAbstract && !x.IsInterface &&
                y != null && y.IsGenericType &&
                y.GetGenericTypeDefinition() == typeof(T)
                select x).ToList();
      }

      public static object InstantiateByTypeAndArgs(Type type, params object[] args)
      {
        var ctors = type.GetConstructors(BindingFlags.Public);
        var obj = (IWrapperBase)ctors[0].Invoke(args);

        return obj;
      }

      public static Type GetUnwrappedTypeFor(Type wrapperType)
      {
        var wrapper = (IWrapperBase)InstantiateByTypeAndArgs(wrapperType, null);

        return wrapper.UnwrappedType;
      }
    }
  }
}