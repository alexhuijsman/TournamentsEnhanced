using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.UnitTests
{
  public class CachedWrapperBaseTests
  {
    private CachedWrapperBaseImpl sut;
    private object unwrappedObject = new object();

    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void GetWrapper_UsesCache()
    {
      var wrapper = CachedWrapperBaseImpl.GetWrapper(unwrappedObject);
      wrapper.ShouldBe(CachedWrapperBaseImpl.GetWrapper(unwrappedObject));
    }

    [Test]
    public void GetWrapper_CacheIsCleaned()
    {
      CachedWrapperBaseImpl.Null.ShouldNotBeNull();
    }

    [Test]
    public void Null_HasNullUnwrappedObject()
    {
      CachedWrapperBaseImpl.Null.UnwrappedObject.ShouldBeNull();
    }

    [Test]
    public void Ctor_ArgBecomesUnwrappedObject()
    {
      sut.UnwrappedObject.ShouldBe(unwrappedObject);
    }

    private class CachedWrapperBaseImpl : CachedWrapperBase<CachedWrapperBaseImpl, object>
    {
      public CachedWrapperBaseImpl() { }
      public CachedWrapperBaseImpl(object obj) : base(obj) { }
    }
  }
}