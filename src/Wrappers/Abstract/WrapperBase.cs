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

  public abstract class WrapperBase<W, T> : WrapperBase
  where W : WrapperBase<W, T>, new()
  {

    public static W Null = new W();
    internal T UnwrappedObject { get; set; }

    public override Type UnwrappedType => typeof(T);

    public bool IsNull => UnwrappedObject == null;

    public WrapperBase() { }
    public WrapperBase(T obj) => UnwrappedObject = obj;
  }

  public class WrapperBaseImpl : WrapperBase<WrapperBaseImpl, object> { }
}