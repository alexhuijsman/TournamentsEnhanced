using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.UnitTests
{
  public class FinderBaseTests
  {
    private FinderBaseImpl _sut = new FinderBaseImpl();
    private class FinderBaseImpl : FinderBase<FindResultBaseImpl, FindOptionBaseImpl, MBWrapperBaseImpl, object>
    {
    }
    private class FindResultBaseImpl : FindResultBase<FindResultBaseImpl, MBWrapperBaseImpl, object>
    {
    }
    private class FindOptionBaseImpl : FindResultBase<FindResultBaseImpl, MBWrapperBaseImpl, object>
    {
    }

    private class MBWrapperBaseImpl : MBWrapperBase<MBWrapperBaseImpl, object>
    {
      public MBWrapperBaseImpl() { }
      public MBWrapperBaseImpl(object obj) : base(obj) { }
    }
  }
}