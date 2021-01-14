using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.UnitTests
{
  public class WrapperBaseTests
  {
    private WrapperBaseImpl sut;
    private object unwrappedObject = new object();

    [SetUp]
    public void SetUp()
    {
      sut = new WrapperBaseImpl(unwrappedObject);
    }

    [Test]
    public void Null_IsStatic()
    {
      WrapperBaseImpl.Null.ShouldBe(WrapperBaseImpl.Null);
    }

    [Test]
    public void Null_IsNotNull()
    {
      WrapperBaseImpl.Null.ShouldNotBeNull();
    }

    [Test]
    public void Null_HasNullUnwrappedObject()
    {
      WrapperBaseImpl.Null.UnwrappedObject.ShouldBeNull();
    }

    [Test]
    public void Ctor_ArgBecomesUnwrappedObject()
    {
      sut.UnwrappedObject.ShouldBe(unwrappedObject);
    }

    private class WrapperBaseImpl : WrapperBase<WrapperBaseImpl, object>
    {
      public WrapperBaseImpl() { }
      public WrapperBaseImpl(object obj) : base(obj) { }
    }
  }
}