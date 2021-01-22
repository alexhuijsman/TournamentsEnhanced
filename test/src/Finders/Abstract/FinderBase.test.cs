using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    private FinderBaseImpl _sut;
    private Mock<FindOptionBaseImpl> _mockFindOptions;
    private List<MBWrapperBaseImpl> _candidates = new List<MBWrapperBaseImpl>();
    private IComparer<MBWrapperBaseImpl>[] _comparers;
    private IComparer<MBWrapperBaseImpl>[] _fallbackComparers;

    [SetUp]
    public void SetUp()
    {
      _sut = new FinderBaseImpl();
      _candidates.Clear();
      _comparers = new IComparer<MBWrapperBaseImpl>[] { };
      _fallbackComparers = new IComparer<MBWrapperBaseImpl>[] { };
      _mockFindOptions = new Mock<FindOptionBaseImpl>();
      _mockFindOptions.SetupGet(findOptions => findOptions.Candidates).Returns(_candidates);
      _mockFindOptions.SetupGet(findOptions => findOptions.Comparers).Returns(_comparers);
      _mockFindOptions.SetupGet(findOptions => findOptions.FallbackComparers).Returns(_fallbackComparers);
    }

    public class FinderBaseImpl : FinderBase<FindResultBaseImpl, FindOptionBaseImpl, MBWrapperBaseImpl, object>
    {
    }
    public class FindResultBaseImpl : FindResultBase<FindResultBaseImpl, MBWrapperBaseImpl, object>
    {
    }
    public class FindOptionBaseImpl : FindOptionsBase<MBWrapperBaseImpl>
    {
    }

    public class MBWrapperBaseImpl : MBWrapperBase<MBWrapperBaseImpl, object>
    {
      public MBWrapperBaseImpl() { }
      public MBWrapperBaseImpl(object obj) : base(obj) { }
    }
  }
}