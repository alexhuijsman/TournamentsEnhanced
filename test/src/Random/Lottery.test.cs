using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Random;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.UnitTests
{
  public class LotteryTests
  {
    private static readonly int NoWinners = 0;
    private static readonly int OneWinner = 1;
    private static readonly int AllWinners = int.MaxValue;

    private LotteryImpl _sut = new LotteryImpl();
    private Mock<System.Random> _mockRandom;
    private Mock<MBMBRandom> _mockMBMBRandom;
    private Mock<ModState> _mockModState;
    private int _expectedLotteryWinners;


    [SetUp]
    public void SetUp()
    {
      SetUp(NoWinners);
    }

    public void SetUp(int winners)
    {
      _expectedLotteryWinners = winners;

      _mockRandom = new Mock<System.Random>();
      _mockRandom.Setup(random => random.Next()).Returns(_expectedLotteryWinners);

      _mockMBMBRandom = new Mock<MBMBRandom>();
      _mockMBMBRandom.SetupGet(mbMBRandom => mbMBRandom.DeterministicRandom).Returns(_mockRandom.Object);

      _mockModState = new Mock<ModState>();
      _mockModState.SetupSet(modState => modState.LotteryWinners = _expectedLotteryWinners);
      _mockModState.SetupGet(modState => modState.LotteryWinners).Returns(_expectedLotteryWinners);

      _sut = new LotteryImpl();
      _sut.MBMBRandom = _mockMBMBRandom.Object;
      _sut.ModState = _mockModState.Object;
    }

    [Test]
    public void DeterministicallyRefreshWinners_ShouldGetDeterministicRandom()
    {
      _sut.DeterministicallyRefreshWinners();

      _mockMBMBRandom.VerifyGet(mbMBRandom => mbMBRandom.DeterministicRandom, Times.Once);
    }

    [Test]
    public void DeterministicallyRefreshWinners_ShouldCallRandomNext()
    {
      _sut.DeterministicallyRefreshWinners();

      _mockRandom.Verify(random => random.Next(), Times.Once);
    }

    [Test]
    public void DeterministicallyRefreshWinners_ShouldUpdateModState()
    {
      _sut.DeterministicallyRefreshWinners();

      _mockModState.VerifySet(modState => modState.LotteryWinners = _expectedLotteryWinners, Times.Once);
    }

    [Test]
    public void DailyTick_ShouldCallDeterministicallyRefreshWinners()
    {
      _sut.DailyTick();

      _mockMBMBRandom.VerifyGet(mbMBRandom => mbMBRandom.DeterministicRandom, Times.Once);
      _mockRandom.Verify(random => random.Next(), Times.Once);
      _mockModState.VerifySet(modState => modState.LotteryWinners = _expectedLotteryWinners, Times.Once);
    }

    [Test]
    public void IsWinner_ShouldBeFalse_NoWinners()
    {
      TestEnum testEnum;
      for (int i = 0; i <= (int)TestEnum.Last; i++)
      {
        testEnum = (TestEnum)i;

        _sut.IsWinner<TestEnum>(testEnum).ShouldBeFalse($"{((TestEnum)i)}");
      }
    }

    [Test]
    public void IsWinner_ShouldBeTrue_AllWinners()
    {
      SetUp(AllWinners);

      TestEnum testEnum;
      for (int i = 0; i <= (int)TestEnum.Last; i++)
      {
        testEnum = (TestEnum)i;

        _sut.IsWinner<TestEnum>(testEnum).ShouldBeTrue($"{((TestEnum)i)}");
      }
    }

    [Test]
    public void IsWinner_ShouldBeTrue_OneWinner()
    {
      SetUp(OneWinner);

      _sut.IsWinner<TestEnum>(TestEnum.N0).ShouldBeTrue();
    }

    [Test]
    public void IsWinner_ShouldBeFalse_OneWinner()
    {
      SetUp(OneWinner);

      TestEnum testEnum;
      for (int i = 1; i <= (int)TestEnum.Last; i++)
      {
        testEnum = (TestEnum)i;

        _sut.IsWinner<TestEnum>(testEnum).ShouldBeFalse($"{((TestEnum)i)}");
      }
    }

    private enum TestEnum
    {
      N0, N1, N2, N3, N4, N5, N6, N7, N8, N9, N10, N11, N12, N13, N14, N15,
      N16, N17, N18, N19, N20, N21, N22, N23, N24, N25, N26, N27, N28, N29, N30,
      Last = N30
    }

    private class LotteryImpl : Lottery
    {
      public LotteryImpl() : base() { }
    }
  }
}