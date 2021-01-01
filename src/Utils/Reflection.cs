using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Utils
{

  public static class Reflection
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
