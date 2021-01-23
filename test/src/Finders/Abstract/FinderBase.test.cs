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
    private Mock<CandidateImpl>[] _mockCandidates;
    private List<CandidateImpl> _candidates;
    private Mock<IMBWrapperComparer>[] _mockComparers;
    private IComparer<CandidateImpl>[] _comparers;
    private Mock<IMBWrapperComparer>[] _mockFallbackComparers;
    private IComparer<CandidateImpl>[] _fallbackComparers;

    [SetUp]
    public void SetUp()
    {
      _sut = new FinderBaseImpl();
      _mockFindOptions = new Mock<FindOptionBaseImpl>();

      SetUpMockCandidates();
      SetUpMockComparers();
      SetUpMockFallbackComparers();
    }

    private void SetUpMockCandidates(params MockCandidateType[] candidateTypes)
    {
      _mockCandidates = new Mock<CandidateImpl>[candidateTypes.Length];
      for (int i = 0; i < _mockCandidates.Length; i++)
      {
        _mockCandidates[i] = GetMockCandidateByType(candidateTypes[i]);
      }

      _candidates = new List<CandidateImpl>(_mockCandidates.Length);
      for (int i = 0; i < _mockCandidates.Length; i++)
      {
        _candidates.Add(_mockCandidates[i].Object);
      }

      _mockFindOptions.SetupGet(findOptions => findOptions.Candidates).Returns(_candidates);
    }

    private void SetUpMockComparers(params MockComparerType[] comparerTypes)
    {
      _mockComparers = new Mock<IMBWrapperComparer>[comparerTypes.Length];
      for (int i = 0; i < _mockComparers.Length; i++)
      {
        _mockComparers[i] = GetMockComparerByType(comparerTypes[i]);
      }

      _comparers = new IComparer<CandidateImpl>[_mockComparers.Length];
      for (int i = 0; i < _mockComparers.Length; i++)
      {
        _comparers[i] = _mockComparers[i].Object;
      }

      _mockFindOptions.SetupGet(findOptions => findOptions.Comparers).Returns(_comparers);
    }

    private void SetUpMockFallbackComparers(params MockComparerType[] comparerTypes)
    {

      _mockFallbackComparers = new Mock<IMBWrapperComparer>[comparerTypes.Length];
      for (int i = 0; i < _mockFallbackComparers.Length; i++)
      {
        _mockFallbackComparers[i] = GetMockComparerByType(comparerTypes[i]);
      }

      _fallbackComparers = new IComparer<CandidateImpl>[_mockFallbackComparers.Length];
      for (int i = 0; i < _mockFallbackComparers.Length; i++)
      {
        _fallbackComparers[i] = _mockFallbackComparers[i].Object;
      }

      _mockFindOptions.SetupGet(findOptions => findOptions.FallbackComparers).Returns(_fallbackComparers);
    }

    private Mock<IMBWrapperComparer> GetMockComparerByType(MockComparerType mockComparerType)
    {
      Mock<IMBWrapperComparer> mockComparer;

      switch (mockComparerType)
      {
        case MockComparerType.QualifyAll:
          mockComparer = InstantiateMockComparer(Compare_QualifyAll);
          break;
        case MockComparerType.QualifyNone:
          mockComparer = InstantiateMockComparer(Compare_QualifyNone);
          break;
        default:
          throw new ArgumentOutOfRangeException("mockComparerType");
      }

      return mockComparer;
    }

    private Mock<CandidateImpl> GetMockCandidateByType(MockCandidateType mockCandidateType)
    {
      var mockCandidate = new Mock<CandidateImpl>();
      mockCandidate.SetupGet(candidate => candidate.MockCandidateType).Returns(mockCandidateType);

      return mockCandidate;
    }

    private Mock<IMBWrapperComparer> InstantiateMockComparer(Func<CandidateImpl, CandidateImpl, int> compareFunc)
    {
      var mockComparer = new Mock<IMBWrapperComparer>();

      mockComparer.Setup(
        comparer => comparer.Compare(
          It.IsAny<CandidateImpl>(),
          It.IsAny<CandidateImpl>()))
        .Returns(compareFunc);

      return mockComparer;
    }

    private Mock<CandidateImpl> InstantiateMockCandidate(MockCandidateType mockCandidateType)
    {
      var mockCandidate = new Mock<CandidateImpl>();

      mockCandidate.SetupGet(
        candidate => candidate.MockCandidateType).Returns(mockCandidateType);

      return mockCandidate;
    }

    private int Compare_QualifyAll(CandidateImpl x, CandidateImpl y)
    {
      if (x.IsNull) return Constants.Comparer.XIsLessThanY;
      if (y.IsNull) return Constants.Comparer.XIsGreaterThanY;
      return Constants.Comparer.XIsEqualToY;
    }

    private int Compare_QualifyNone(CandidateImpl x, CandidateImpl y)
    {
      if (x.IsNull) return Constants.Comparer.XIsGreaterThanY;
      if (y.IsNull) return Constants.Comparer.XIsLessThanY;
      return Constants.Comparer.XIsEqualToY;
    }

    public enum MockComparerType
    {
      QualifyAll,
      QualifyNone
    }

    public enum MockCandidateType
    {
      MostQualified,
      Qualified,
      LeastQualified,
      Unqualified,
    }

    public class FinderBaseImpl : FinderBase<FindResultBaseImpl, FindOptionBaseImpl, CandidateImpl, object>
    {
    }
    public class FindResultBaseImpl : FindResultBase<FindResultBaseImpl, CandidateImpl, object>
    {
    }
    public class FindOptionBaseImpl : FindOptionsBase<CandidateImpl>
    {
    }

    public interface IMBWrapperComparer : IComparer<CandidateImpl> { }

    public class CandidateImpl : MBWrapperBase<CandidateImpl, object>
    {
      public virtual MockCandidateType MockCandidateType { get; }

      public CandidateImpl() { }
      public CandidateImpl(object obj) : base(obj) { }
    }
  }
}