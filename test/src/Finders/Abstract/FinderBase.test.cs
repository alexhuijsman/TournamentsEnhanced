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
    private const int NumberOfUnqualifiedCandidates = 11;
    private const int NumberOfQualifiedCandidates = 7;
    private const int NumberOfIdealCandidates = 3;
    private const int TotalNumberOfCandidates = NumberOfUnqualifiedCandidates + NumberOfQualifiedCandidates + NumberOfIdealCandidates;
    private const int NumberOfFailUnqualifiedComparers = 2;
    private const int NumberOfFailQualifiedComparers = 1;
    private const int TotalNumberOfComparers = NumberOfFailUnqualifiedComparers + NumberOfFailQualifiedComparers;
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

    private void SetUpOneComparerWithoutCandidates()
    {
      SetUpMockComparers(MockComparerType.FailUnqualified);
    }

    private void SetUpOneComparerAndOneFallbackWithoutCandidates()
    {
      SetUpMockComparers(MockComparerType.FailUnqualified);
      SetUpMockFallbackComparers(MockComparerType.FailUnqualified);
    }

    private void SetUpManyMockComparers(bool areFallbackComparers = false)
    {
      ref var mockComparersRef = ref _mockComparers;
      ref var comparersRef = ref _comparers;

      if (areFallbackComparers)
      {
        mockComparersRef = ref _mockFallbackComparers;
        comparersRef = ref _fallbackComparers;

      }

      var mockComparers = new List<Mock<IMBWrapperComparer>>(TotalNumberOfComparers);

      for (int i = 0; i < NumberOfFailUnqualifiedComparers; i++)
      {
        mockComparers.Add(GetMockComparerByType(MockComparerType.FailUnqualified));
      }

      for (int i = 0; i < NumberOfFailQualifiedComparers; i++)
      {
        mockComparers.Add(GetMockComparerByType(MockComparerType.FailQualified));
      }

      mockComparersRef = mockComparers.Shuffle().ToArray();

      comparersRef = new IComparer<CandidateImpl>[mockComparersRef.Length];
      for (int i = 0; i < mockComparersRef.Length; i++)
      {
        comparersRef[i] = mockComparersRef[i].Object;
      }

      _mockFindOptions.SetupGet(findOptions => findOptions.Comparers).Returns(comparersRef);
    }

    private void SetUpManyMockFailUnqualifiedComparers(bool areFallbackComparers = false)
    {
      ref var mockComparersRef = ref _mockComparers;
      ref var comparersRef = ref _comparers;

      if (areFallbackComparers)
      {
        mockComparersRef = ref _mockFallbackComparers;
        comparersRef = ref _fallbackComparers;

      }

      var mockComparers = new List<Mock<IMBWrapperComparer>>(TotalNumberOfComparers);

      for (int i = 0; i < NumberOfFailUnqualifiedComparers; i++)
      {
        mockComparers.Add(GetMockComparerByType(MockComparerType.FailUnqualified));
      }

      mockComparersRef = mockComparers.Shuffle().ToArray();

      comparersRef = new IComparer<CandidateImpl>[mockComparersRef.Length];
      for (int i = 0; i < mockComparersRef.Length; i++)
      {
        comparersRef[i] = mockComparersRef[i].Object;
      }

      _mockFindOptions.SetupGet(findOptions => findOptions.Comparers).Returns(comparersRef);
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

    private void SetUpManyMockNonIdealCandidates()
    {
      var mockCandidates = new List<Mock<CandidateImpl>>(TotalNumberOfCandidates - NumberOfIdealCandidates);

      for (int i = 0; i < NumberOfUnqualifiedCandidates; i++)
      {
        mockCandidates.Add(GetMockCandidateByType(MockCandidateType.Unqualified));
      }
      for (int i = 0; i < NumberOfQualifiedCandidates; i++)
      {
        mockCandidates.Add(GetMockCandidateByType(MockCandidateType.Qualified));
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
      _mockFindOptions.SetupGet(findOptions => findOptions.HasFallbackComparers).Returns(_fallbackComparers.Length > 0 ? true : false);
    }

    private Mock<IMBWrapperComparer> GetMockComparerByType(MockComparerType mockComparerType)
    {
      Mock<IMBWrapperComparer> mockComparer;

      switch (mockComparerType)
      {
        case MockComparerType.FailUnqualified:
          mockComparer = InstantiateMockComparer(Compare_DisqualifyUnqualifiedCandidates);
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
          Constants.Comparer.XOutranksY :
          Constants.Comparer.YOutranksX;
      }
      else if (y.IsNull)
      {
        result = x.MockCandidateType <= CandidateTypeFailureValue ?
          Constants.Comparer.YOutranksX :
          Constants.Comparer.XOutranksY;
      }
      else if (x.MockCandidateType > y.MockCandidateType)
      {
        result = Constants.Comparer.XOutranksY;
      }
      else if (x.MockCandidateType < y.MockCandidateType)
      {
        result = Constants.Comparer.YOutranksX;
      }
      else
      {
        result = Constants.Comparer.BothEqualRank;
      }

      return result;
    }

    public enum MockComparerType
    {
      None,
      FailUnqualified,
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

    public class CandidateImpl : MBWrapperBase<CandidateImpl, object>, IComparable
    {
      public virtual MockCandidateType MockCandidateType { get; }

      public CandidateImpl() { }
      public CandidateImpl(object obj) : base(obj) { }

      public int CompareTo(object obj)
      {
        var other = (CandidateImpl)obj;

        return MockCandidateType > other.MockCandidateType ?
          Constants.Comparer.XOutranksY :
          MockCandidateType < other.MockCandidateType ?
            Constants.Comparer.YOutranksX :
            Constants.Comparer.BothEqualRank;

      }
    }
  }
}