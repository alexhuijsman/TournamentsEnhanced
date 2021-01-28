using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.UnitTests
{
  public class WrapperBaseTests
  {
    private WrapperBaseImpl sut;
    private readonly object unwrappedObject = new object();

    [Test]
    public virtual void Ctor_ArgBecomesUnwrappedObject()
    {
      sut = new WrapperBaseImpl(unwrappedObject);

      sut.UnwrappedObject.ShouldBe(unwrappedObject);
    }

    [Test]
    public virtual void Ctor_NoArgsBecomesNullUnwrappedObject()
    {
      sut = new WrapperBaseImpl();

      sut.IsNull.ShouldBeTrue();
    }

    [Test]
    public virtual void Ctor_IsNullShouldBeFalse()
    {
      sut = new WrapperBaseImpl(unwrappedObject);

      sut.IsNull.ShouldBeFalse();
    }

    private class WrapperBaseImpl : WrapperBase
    {
      public WrapperBaseImpl() { }
      public WrapperBaseImpl(object obj) : base(obj) { }
    }
  }
}