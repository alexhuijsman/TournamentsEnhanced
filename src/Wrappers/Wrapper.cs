using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers
{
  public class Wrapper<T>
  {
    internal T UnwrappedObject { get; set; }

    public Wrapper() { }
    public Wrapper(T obj) => UnwrappedObject = obj;

    public void Wrap(T obj) => UnwrappedObject = obj;
    public T Unwrap() => UnwrappedObject;
  }
}