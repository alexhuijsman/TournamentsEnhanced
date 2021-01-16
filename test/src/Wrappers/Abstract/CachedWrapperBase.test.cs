using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.UnitTests
{
  public class CachedWrapperBaseTests
  {
    private object unwrappedObject = new object();

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
      CachedWrapperBaseImpl.Null.IsNull.ShouldBeTrue();
    }

    [Test]
    public void Ctor_ArgBecomesUnwrappedObject()
    {
      var sut = new CachedWrapperBaseImpl(unwrappedObject);

      sut.UnwrappedObject.ShouldBe(unwrappedObject);
    }

    [Test]
    public void Ctor_NoArgsBecomesNullUnwrappedObject()
    {
      var sut = new CachedWrapperBaseImpl();

      sut.IsNull.ShouldBeTrue();
    }

    private class CachedWrapperBaseImpl : CachedWrapperBase<CachedWrapperBaseImpl, object>
    {
      public CachedWrapperBaseImpl() { }
      public CachedWrapperBaseImpl(object obj) : base(obj) { }
    }
  }
}