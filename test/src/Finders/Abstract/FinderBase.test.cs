using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    private const int NumberOfUnqualifiedCandidates = 100;
    private const int NumberOfQualifiedCandidates = 50;
    private const int NumberOfIdealCandidates = 10;
    private const int TotalNumberOfCandidates = NumberOfUnqualifiedCandidates + NumberOfQualifiedCandidates + NumberOfIdealCandidates;

    private FinderBaseImpl _sut;
    private Mock<FindOptionBaseImpl> _mockFindOptions;
    private Mock<CandidateImpl>[] _mockCandidates;
    private List<CandidateImpl> _candidates;
    private Mock<IMBWrapperComparer>[] _mockComparers;
    private IComparer<CandidateImpl>[] _comparers;
    private Mock<IMBWrapperComparer>[] _mockFallbackComparers;
    private IComparer<CandidateImpl>[] _fallbackComparers;
    private Random _random = new Random();

    [SetUp]
    public void SetUp()
    {
      _sut = new FinderBaseImpl();
      _mockFindOptions = new Mock<FindOptionBaseImpl>();

      var mockMBMBRandom = new Mock<MBMBRandom>();
      mockMBMBRandom
        .Setup(mbMbRandom => mbMbRandom.DeterministicRandomInt(It.IsAny<int>()))
        .Returns((int maxValue) => _random.Next(maxValue));
      ListExtensions.MBMBRandom = mockMBMBRandom.Object;

      SetUpMockCandidate();
      SetUpMockComparers();
      SetUpMockFallbackComparers();
    }

    private void SetUpManyMockCandidates()
    {
      var mockCandidates = new List<Mock<CandidateImpl>>(TotalNumberOfCandidates);

      for (int i = 0; i < NumberOfUnqualifiedCandidates; i++)
      {
        mockCandidates.Add(GetMockCandidateByType(MockCandidateType.Unqualified));
      }
      for (int i = 0; i < NumberOfQualifiedCandidates; i++)
      {
        mockCandidates.Add(GetMockCandidateByType(MockCandidateType.Qualified));
      }
      for (int i = 0; i < NumberOfIdealCandidates; i++)
      {
        mockCandidates.Add(GetMockCandidateByType(MockCandidateType.Ideal));
      }

      _mockCandidates = mockCandidates.Shuffle().ToArray();

      _candidates = new List<CandidateImpl>(_mockCandidates.Length);
      for (int i = 0; i < _mockCandidates.Length; i++)
      {
        _candidates.Add(_mockCandidates[i].Object);
      }

      _mockFindOptions.SetupGet(findOptions => findOptions.Candidates).Returns(_candidates);
    }
    private void SetUpMockCandidate(MockCandidateType candidateType = MockCandidateType.None)
    {
      var wantsCandidate = candidateType != MockCandidateType.None;

      _candidates = new List<CandidateImpl>(wantsCandidate ? 1 : 0);

      if (wantsCandidate)
      {
        _mockCandidates = new Mock<CandidateImpl>[] { GetMockCandidateByType(candidateType) };
        _candidates.Add(_mockCandidates[0].Object);
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
        case MockComparerType.FailUnqualified:
          mockComparer = InstantiateMockComparer(Compare_DisqualifyUnqualifiedCandidates);
          break;
        case MockComparerType.FailLeastQualified:
          mockComparer = InstantiateMockComparer(Compare_DisqualifyQualifiedCandidates);
          break;
        case MockComparerType.FailQualified:
          mockComparer = InstantiateMockComparer(Compare_DisqualifyQualifiedCandidates);
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

    private int Compare_DisqualifyUnqualifiedCandidates(CandidateImpl x, CandidateImpl y) => Compare(x, y, MockCandidateType.Unqualified);

    private int Compare_DisqualifyQualifiedCandidates(CandidateImpl x, CandidateImpl y) => Compare(x, y, MockCandidateType.Qualified);

    private int Compare(CandidateImpl x, CandidateImpl y, MockCandidateType CandidateTypeFailureValue)
    {
      int result;

      if (x.IsNull)
      {
        result = y.MockCandidateType <= CandidateTypeFailureValue ?
          Constants.Comparer.XIsGreaterThanY :
          Constants.Comparer.XIsLessThanY;
      }

      if (y.IsNull)
      {
        result = x.MockCandidateType <= CandidateTypeFailureValue ?
          Constants.Comparer.XIsLessThanY :
          Constants.Comparer.XIsGreaterThanY;
      }

      if (x.MockCandidateType > y.MockCandidateType)
      {
        result = Constants.Comparer.XIsGreaterThanY;
      }
      else if (x.MockCandidateType < y.MockCandidateType)
      {
        result = Constants.Comparer.XIsLessThanY;
      }
      else
      {
        result = Constants.Comparer.XIsEqualToY;
      }

      return result;
    }

    public enum MockComparerType
    {
      None,
      FailUnqualified,
      FailLeastQualified,
      FailQualified,
    }

    public enum MockCandidateType
    {
      None,
      Unqualified,
      Qualified,
      Ideal,
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