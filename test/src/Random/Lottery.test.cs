using System;
using System.Timers;
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
    private System.Random random = new System.Random();
    private LotteryImpl sut = new LotteryImpl();
    private Mock<System.Random> mockRandom;
    private Mock<MBMBRandom> mockMBMBRandom;
    private Mock<ModState> mockModState;
    private int expectedValue;


    [SetUp]
    public void SetUp()
    {
      expectedValue = random.Next();

      mockRandom = new Mock<System.Random>();
      mockRandom.Setup(random => random.Next()).Returns(expectedValue);

      mockMBMBRandom = new Mock<MBMBRandom>();
      mockMBMBRandom.SetupGet(mbMBRandom => mbMBRandom.DeterministicRandom).Returns(mockRandom.Object);

      mockModState = new Mock<ModState>();
      mockModState.SetupSet(modState => modState.LotteryWinners = expectedValue);

      sut = new LotteryImpl();
      sut.MBMBRandom = mockMBMBRandom.Object;
      sut.ModState = mockModState.Object;
    }

    [Test]
    public void DeterministicallyRefreshWinners_ShouldGetDeterministicRandom()
    {
      sut.DeterministicallyRefreshWinners();

      mockMBMBRandom.VerifyGet(mbMBRandom => mbMBRandom.DeterministicRandom, Times.Once);
    }

    [Test]
    public void DeterministicallyRefreshWinners_ShouldCallRandomNext()
    {
      sut.DeterministicallyRefreshWinners();

      mockRandom.Verify(random => random.Next(), Times.Once);
    }

    [Test]
    public void DeterministicallyRefreshWinners_ShouldUpdateModState()
    {
      sut.DeterministicallyRefreshWinners();

      mockModState.VerifySet(modState => modState.LotteryWinners = expectedValue, Times.Once);
    }

    [Test]
    public void DailyTick_ShouldCallDeterministicallyRefreshWinners()
    {
      sut.DailyTick();

      mockMBMBRandom.VerifyGet(mbMBRandom => mbMBRandom.DeterministicRandom, Times.Once);
      mockRandom.Verify(random => random.Next(), Times.Once);
      mockModState.VerifySet(modState => modState.LotteryWinners = expectedValue, Times.Once);
    }

    [Test]
    public void IsWinner()
    {

    }


    private class LotteryImpl : Lottery
    {
      public LotteryImpl() : base() { }
    }
  }
}