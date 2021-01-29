using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Abstract;
using TournamentsEnhanced.Wrappers.Abstract;


namespace Test
{
  public class ComparerBaseTest : TestBase
  {
    private ComparerBaseImpl _sut;
    private Mock<WrapperBase> _mockX;
    private Mock<WrapperBase> _mockY;
    private bool _xMeetsRequirements;
    private bool _yMeetsRequirements;

    private bool MeetsRequirements(WrapperBase wrapper)
    {
      var mockWrapper = Mock.Get(wrapper);

      return (mockWrapper == _mockX && _xMeetsRequirements) ||
             (mockWrapper == _mockY && _yMeetsRequirements);
    }

    [SetUp]
    public void SetUp()
    {
      _mockX = MockRepository.Create<WrapperBase>();
      _mockY = MockRepository.Create<WrapperBase>();

      _xMeetsRequirements = false;
      _yMeetsRequirements = false;

      _sut = new ComparerBaseImpl(MeetsRequirements);
    }

    [Test]
    public void Compare_XShouldBeGreaterThanY_YIsNull()
    {
      _mockX.SetupGet(x => x.IsNull).Returns(false);
      _mockY.SetupGet(y => y.IsNull).Returns(true);
      _xMeetsRequirements = true;

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_XShouldBeLessThanY_YIsNull()
    {
      _mockX.SetupGet(x => x.IsNull).Returns(false);
      _mockY.SetupGet(y => y.IsNull).Returns(true);

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_XShouldBeGreaterThanY_XIsNull()
    {
      _mockX.SetupGet(x => x.IsNull).Returns(true);
      _mockY.SetupGet(y => y.IsNull).Returns(false);

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_XShouldBeLessThanY_XIsNull()
    {
      _mockX.SetupGet(x => x.IsNull).Returns(true);
      _mockY.SetupGet(y => y.IsNull).Returns(false);
      _yMeetsRequirements = true;

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_XShouldBeGreaterThanY_XMeetsRequirements()
    {
      _mockX.SetupGet(x => x.IsNull).Returns(false);
      _mockY.SetupGet(y => y.IsNull).Returns(false);
      _xMeetsRequirements = true;

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_XShouldBeLessThanY_YMeetsRequirements()
    {
      _mockX.SetupGet(x => x.IsNull).Returns(false);
      _mockY.SetupGet(y => y.IsNull).Returns(false);
      _yMeetsRequirements = true;

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_XShouldBeEqualToY_XAndYMeetRequirements()
    {
      _mockX.SetupGet(x => x.IsNull).Returns(false);
      _mockY.SetupGet(y => y.IsNull).Returns(false);
      _xMeetsRequirements = true;
      _yMeetsRequirements = true;

      var result = _sut.Compare(_mockX.Object, _mockY.Object);

      result.ShouldBe(Constants.Comparer.BothEqualRank);
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