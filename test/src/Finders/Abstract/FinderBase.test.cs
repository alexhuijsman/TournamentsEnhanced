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
    private Mock<MBWrapperBaseImpl> _mockCandidate;
    private Mock<IMBWrapperComparer> _mockQualifyAllComparer;
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

      _mockQualifyAllComparer = new Mock<IMBWrapperComparer>();
      _mockQualifyAllComparer.Setup(
        comparer => comparer.Compare(
          It.IsAny<MBWrapperBaseImpl>(),
          It.IsAny<MBWrapperBaseImpl>()))
        .Returns((MBWrapperBaseImpl x, MBWrapperBaseImpl y) => Compare_QualifyAll(x, y));

      _mockFindOptions = new Mock<FindOptionBaseImpl>();
      _mockFindOptions.SetupGet(findOptions => findOptions.Candidates).Returns(_candidates);
      _mockFindOptions.SetupGet(findOptions => findOptions.Comparers).Returns(_comparers);
      _mockFindOptions.SetupGet(findOptions => findOptions.FallbackComparers).Returns(_fallbackComparers);
    }

    private void SetUpSingleCandidate()
    {
      _mockCandidate = new Mock<MBWrapperBaseImpl>();
      _mockCandidate.SetupGet(candidate => candidate.IsNull).Returns(false);
      _candidates.Add(_mockCandidate.Object);
    }

    private void SetUpSingleComparer()
    {
      _comparers = new IComparer<MBWrapperBaseImpl>[]
      {
        _mockQualifyAllComparer.Object
      };
    }

    private int Compare_QualifyAll(MBWrapperBaseImpl x, MBWrapperBaseImpl y)
    {
      if (x.IsNull) return Constants.Comparer.XIsLessThanY;
      if (y.IsNull) return Constants.Comparer.XIsGreaterThanY;
      return Constants.Comparer.XIsEqualToY;
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

    public interface IMBWrapperComparer : IComparer<MBWrapperBaseImpl> { }

    public class MBWrapperBaseImpl : MBWrapperBase<MBWrapperBaseImpl, object>
    {
      public MBWrapperBaseImpl() { }
      public MBWrapperBaseImpl(object obj) : base(obj) { }
    }
  }
}