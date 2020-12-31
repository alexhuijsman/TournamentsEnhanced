using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.Abstract
{
  public abstract class WrapperBase<T>
  {
    internal T UnwrappedObject { get; set; }

    public WrapperBase() { }
    public WrapperBase(T obj) => UnwrappedObject = obj;

    public void Wrap(T obj) => UnwrappedObject = obj;
    public T Unwrap() => UnwrappedObject;
  }
}