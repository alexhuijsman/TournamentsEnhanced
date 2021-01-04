using System;

namespace TournamentsEnhanced.Wrappers.Abstract
{

  public interface IWrapperBase
  {
    Type UnwrappedType { get; }
  }

  public abstract class WrapperBase : IWrapperBase
  {
    public abstract Type UnwrappedType { get; }
  }

  public abstract class WrapperBase<T> : WrapperBase
  {
    internal T UnwrappedObject { get; set; }

    public override Type UnwrappedType => typeof(T);

    public bool IsNull => UnwrappedObject == null;

    public WrapperBase() { }
    public WrapperBase(T obj) => UnwrappedObject = obj;
  }

  public abstract class AbstractWrapperImpl : WrapperBase<object> { }
}