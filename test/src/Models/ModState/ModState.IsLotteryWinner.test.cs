using System;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class ModStateTests
  {
    [Test]
    public void IsLotteryWinner_ShouldBeFalse_NoWinners()
    {
      SetUpWithLotteryResults(Constants.LotteryResults.NoWinners);

      TestEnum testEnum;
      for (int i = 0; i <= (int)TestEnum.HighestValidValue; i++)
      {
        testEnum = (TestEnum)i;

        _sut.IsLotteryWinner(testEnum).ShouldBeFalse($"{((TestEnum)i)}");
      }
    }

    [Test]
    public void IsLotteryWinner_ShouldBeTrue_AllWinners()
    {
      SetUpWithLotteryResults(Constants.LotteryResults.AllWinners);

      TestEnum testEnum;
      for (int i = 0; i <= (int)TestEnum.HighestValidValue; i++)
      {
        testEnum = (TestEnum)i;

        _sut.IsLotteryWinner(testEnum).ShouldBeTrue($"{((TestEnum)i)}");
      }
    }

    [Test]
    public void IsLotteryWinner_ShouldBeTrue_OnlyFirstIsWinner()
    {
      SetUpWithLotteryResults(TestConstants.LotteryResults.OnlyFirstIsWinner);

      _sut.IsLotteryWinner(TestEnum.N0).ShouldBeTrue();
    }

    [Test]
    public void IsLotteryWinner_ShouldBeFalse_OnlyFirstIsWinner()
    {
      SetUpWithLotteryResults(TestConstants.LotteryResults.OnlyFirstIsWinner);

      TestEnum testEnum;
      for (int i = 1; i <= (int)TestEnum.HighestValidValue; i++)
      {
        testEnum = (TestEnum)i;

        _sut.IsLotteryWinner(testEnum).ShouldBeFalse($"{((TestEnum)i)}");
      }
    }

    [Test]
    public void IsLotteryWinner_ShouldThrow_Exception()
    {
      Should.Throw<ArgumentOutOfRangeException>(() => _sut.IsLotteryWinner(TestEnum.InvalidValue))
        .Message.ShouldBe($"value {(int)TestEnum.InvalidValue} was greater than maximum permitted value of {Constants.ModState.MaxIsWinnerArgValue}");
    }
  }
}