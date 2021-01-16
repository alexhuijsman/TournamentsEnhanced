using System;

namespace TournamentsEnhanced.Wrappers.Abstract
{

  public abstract class WrapperBase
  {
    public object UnwrappedObject { get; set; }
    public virtual bool IsNull => UnwrappedObject == null;

    public WrapperBase() { }
    public WrapperBase(object obj) => UnwrappedObject = obj;
  }

  public abstract class WrapperBase<W, T> : WrapperBase
  where W : WrapperBase<W, T>, new()
  {
    public static readonly W Null = new W();

    public new T UnwrappedObject { get; set; }
    public override bool IsNull => UnwrappedObject == null;

    public WrapperBase() { }
    public WrapperBase(T obj) => UnwrappedObject = obj;
  }
}