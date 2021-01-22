using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.UnitTests
{
  public class FinderBaseTests
  {
    private FinderBaseImpl _sut;
    private Mock<FindOptionBaseImpl> _mockFindOptions;
    private List<Mock<MBWrapperBaseImpl>> _mockCandidates = new List<Mock<MBWrapperBaseImpl>>();

    [SetUp]
    public void SetUp()
    {
      _sut = new FinderBaseImpl();
      _mockCandidates.Clear();
      _mockFindOptions = new Mock<FindOptionBaseImpl>();
      _mockFindOptions.SetupGet(findOptions => findOptions.Candidates).Returns(_mockCandidates.ConvertAll<MBWrapperBaseImpl>(mockCandidate => mockCandidate.Object));
    }

    [Test]
    public void Find_EmptyOptions_DoesNotThrowException()
    {
      Should.NotThrow(() => _sut.Find(_mockFindOptions.Object));
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