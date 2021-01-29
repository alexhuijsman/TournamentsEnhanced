using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.Abstract;


namespace Test
{
  public class FindOptionsBaseTest : TestBase
  {
    private FindOptionsBaseImpl _sut;

    [SetUp]
    public void SetUp()
    {
      _sut = new FindOptionsBaseImpl();
    }

    [Test]
    public void HasFallbackComparers_NullComparers_ShouldBeFalse()
    {
      _sut.HasFallbackComparers.ShouldBe(false);
    }


    [Test]
    public void HasFallbackComparers_EmptyComparers_ShouldBeFalse()
    {
      _sut.FallbackComparers = new IComparer<WrapperBase>[0];

      _sut.HasFallbackComparers.ShouldBe(false);
    }


    [Test]
    public void HasFallbackComparers_NonEmptyComparers_ShouldBeTrue()
    {
      _sut.FallbackComparers = new IComparer<WrapperBase>[]
      {
        new Mock<IComparer<WrapperBase>>().Object
      };

      _sut.HasFallbackComparers.ShouldBe(true);
    }


    private class FindOptionsBaseImpl : FindOptionsBase<WrapperBase> { }

  }
}