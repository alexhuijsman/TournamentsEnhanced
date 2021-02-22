using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.Abstract;


namespace Test
{
  public class FindOptionsBaseTest : TestBase<FindOptionsBaseImpl>
  {

    [Test]
    public void HasFallbackComparers_NullComparers_ShouldBeFalse()
    {
      SetUp();

      _sut.HasFallbackComparers.ShouldBe(false);
    }


    [Test]
    public void HasFallbackComparers_EmptyComparers_ShouldBeFalse()
    {
      SetUp();

      _sut.FallbackComparers = new IComparer<WrapperBase>[0];

      _sut.HasFallbackComparers.ShouldBe(false);
    }


    [Test]
    public void HasFallbackComparers_NonEmptyComparers_ShouldBeTrue()
    {
      SetUp();

      _sut.FallbackComparers = new IComparer<WrapperBase>[]
      {
        new Mock<IComparer<WrapperBase>>().Object
      };

      _sut.HasFallbackComparers.ShouldBe(true);
    }
  }

  public class FindOptionsBaseImpl : FindOptionsBase<WrapperBase> { }
}