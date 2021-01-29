using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Abstract;
using TournamentsEnhanced.Wrappers.Abstract;
using static TournamentsEnhanced.Constants;

namespace Test
{
  public class ComparerBaseTest : TestBase
  {
    private const int XIsNull = 1 << 1;
    private const int YIsNull = 1 << 2;
    private const int XMeetsRequirements = 1 << 3;
    private const int YMeetsRequirements = 1 << 4;

    private ComparerBaseImpl _sut;
    private Mock<WrapperBase> _mockX;
    private Mock<WrapperBase> _mockY;
    private bool _xMeetsRequirements;
    private bool _yMeetsRequirements;

    public void SetUp(int flags)
    {
      _mockX = MockRepository.Create<WrapperBase>();
      _mockX.SetupGet(x => x.IsNull).Returns((flags & XIsNull) != 0);

      _mockY = MockRepository.Create<WrapperBase>();
      _mockY.SetupGet(y => y.IsNull).Returns((flags & YIsNull) != 0);

      _xMeetsRequirements = (flags & XMeetsRequirements) != 0;
      _yMeetsRequirements = (flags & YMeetsRequirements) != 0;

      _sut = new ComparerBaseImpl(MeetsRequirements);
    }

    [Test]
    public void Compare_YIsNull_XMeetsRequirements_XShouldOutrankY()
    {
      SetUp(YIsNull | XMeetsRequirements);

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Comparer.XOutranksY);
    }

    [Test]
    public void Compare_YIsNull_YShouldOutrankX()
    {
      SetUp(YIsNull);

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Comparer.YOutranksX);
    }

    [Test]
    public void Compare_XIsNull_XShouldOutrankY()
    {
      SetUp(XIsNull);

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Comparer.XOutranksY);
    }

    [Test]
    public void Compare_XIsNull_YMeetsRequirements_YShouldOutrankX()
    {
      SetUp(XIsNull | YMeetsRequirements);

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Comparer.YOutranksX);
    }

    [Test]
    public void Compare_XMeetsRequirements_XShouldOutrankY()
    {
      SetUp(XMeetsRequirements);

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Comparer.XOutranksY);
    }

    [Test]
    public void Compare_YMeetsRequirements_YShouldOutrankX()
    {
      SetUp(YMeetsRequirements);

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Comparer.YOutranksX);
    }

    [Test]
    public void Compare_XAndYMeetRequirements_XAndYShouldBeEqualRank()
    {
      SetUp(XMeetsRequirements | YMeetsRequirements);

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Comparer.BothEqualRank);
    }

    private bool MeetsRequirements(WrapperBase wrapper)
    {
      var mockWrapper = Mock.Get(wrapper);

      return (mockWrapper == _mockX && _xMeetsRequirements) ||
             (mockWrapper == _mockY && _yMeetsRequirements);
    }

    private class ComparerBaseImpl : ComparerBase<WrapperBase>
    {
      public bool ShouldMeetRequirements { get; set; }
      public Func<WrapperBase, bool> MeetsRequirementsFunc { get; }

      public ComparerBaseImpl(Func<WrapperBase, bool> meetsRequirementsFunc)
      {
        MeetsRequirementsFunc = meetsRequirementsFunc;
      }

      protected override bool MeetsRequirements(WrapperBase wrapper)
      {
        return MeetsRequirementsFunc(wrapper);
      }
    }
  }
}