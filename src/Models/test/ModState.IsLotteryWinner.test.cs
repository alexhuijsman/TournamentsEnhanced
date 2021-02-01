using System;
using Moq;
using NUnit.Framework;
using Shouldly;

using TournamentsEnhanced;


namespace Test
{
  public partial class ModStateTest : TestBase
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
      SetUpSerializableModState(Constants.LotteryResults.AllWinners);

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
      SetUpSerializableModState(TestConstants.LotteryResults.OnlyFirstIsWinner);

      _sut.IsLotteryWinner(TestEnum.N0).ShouldBeTrue();
    }

    [Test]
    public void IsLotteryWinner_ShouldBeFalse_OnlyFirstIsWinner()
    {
      SetUpWithLotteryResults(TestConstants.LotteryResults.OnlyFirstIsWinner);
      SetUpSerializableModState(TestConstants.LotteryResults.OnlyFirstIsWinner);

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
      Should.Throw<ArgumentOutOfRangeException>(() => _sut.IsLotteryWinner(TestEnum.InvalidValue));
    }

  }
}