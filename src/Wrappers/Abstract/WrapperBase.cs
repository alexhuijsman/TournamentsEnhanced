using System;

namespace TournamentsEnhanced.Wrappers.Abstract
{

  public abstract class WrapperBase
  {
    public object UnwrappedObject { get; set; }
    public bool IsNull => UnwrappedObject == null;

  }

  public abstract class WrapperBase<W, T> : WrapperBase
  where W : WrapperBase<W, T>, new()
  {

    public static readonly W Null = new W();
    public new T UnwrappedObject { get; set; }

    public WrapperBase() { }
    public WrapperBase(T obj) => UnwrappedObject = obj;
  }
}