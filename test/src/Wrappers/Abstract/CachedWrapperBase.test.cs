using System;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.UnitTests
{
  public class CachedWrapperBaseTests
  {
    [Test]
    public void GetWrapper_UsesCache()
    {
      var unwrappedObject = new object();

      var expectedWrapper = CachedWrapperBaseImpl.GetWrapper(unwrappedObject);
      var actualWrapper = CachedWrapperBaseImpl.GetWrapper(unwrappedObject);

      actualWrapper.ShouldBe(expectedWrapper);
    }

    [Test]
    public void GetWrapper_CachedObjectsAreWeaklyReferenced()
    {
      var unwrappedObject = new object();
      var wrapperWithDereferencedObject = CachedWrapperBaseImpl.GetWrapper(unwrappedObject);

      wrapperWithDereferencedObject.IsNull.ShouldBeFalse();

      unwrappedObject = null;
      GC.Collect();

      wrapperWithDereferencedObject.IsNull.ShouldBeTrue();
    }

    [Test]
    public void GetWrapper_ReferencedObjectSurvivesGarbageCollection()
    {
      var unwrappedObject = new object();

      var wrapper = CachedWrapperBaseImpl.GetWrapper(unwrappedObject);

      wrapper.IsNull.ShouldBeFalse();

      GC.Collect();

      wrapper.IsNull.ShouldBeFalse();
    }

    private class CachedWrapperBaseImpl : CachedWrapperBase<CachedWrapperBaseImpl, object>
    {
      public CachedWrapperBaseImpl() { }
      public CachedWrapperBaseImpl(object obj) : base(obj) { }
    }
  }
}