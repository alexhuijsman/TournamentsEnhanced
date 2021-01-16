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
    [Test]
    public void DeterministicallyRefreshWinners_ShouldGetDeterministicRandom()
    {
      var sut = new LotteryImpl();
      var mockRandom = new Mock<System.Random>();
      var mockMBMBRandom = new Mock<MBMBRandom>();
      mockMBMBRandom.SetupGet(mbMBRandom => mbMBRandom.DeterministicRandom).Returns(mockRandom.Object);
      sut.MBMBRandom = mockMBMBRandom.Object;

      sut.DeterministicallyRefreshWinners();

      mockMBMBRandom.VerifyGet(mbMBRandom => mbMBRandom.DeterministicRandom, Times.Once);
    }

    [Test]
    public void DeterministicallyRefreshWinners_ShouldCallRandomNext()
    {
      var sut = new LotteryImpl();
      var mockRandom = new Mock<System.Random>();
      var mockMBMBRandom = new Mock<MBMBRandom>();
      mockMBMBRandom.SetupGet(mbMBRandom => mbMBRandom.DeterministicRandom).Returns(mockRandom.Object);
      sut.MBMBRandom = mockMBMBRandom.Object;

      sut.DeterministicallyRefreshWinners();

      mockRandom.Verify(random => random.Next(), Times.Once);
    }

    [Test]
    public void DeterministicallyRefreshWinners_ShouldUpdateModState()
    {
      var sut = new LotteryImpl();
      var mockRandom = new Mock<System.Random>();
      var expectedValue = new System.Random().Next();
      mockRandom.Setup(random => random.Next()).Returns(expectedValue);
      var mockMBMBRandom = new Mock<MBMBRandom>();
      mockMBMBRandom.SetupGet(mbMBRandom => mbMBRandom.DeterministicRandom).Returns(mockRandom.Object);
      var mockModState = new Mock<ModState>();
      mockModState.SetupSet(modState => modState.LotteryWinners = expectedValue);

      sut.MBMBRandom = mockMBMBRandom.Object;
      sut.ModState = mockModState.Object;

      sut.DeterministicallyRefreshWinners();

      mockModState.VerifySet(modState => modState.LotteryWinners = expectedValue, Times.Once);
    }

    private class LotteryImpl : Lottery
    {
      public LotteryImpl() : base() { }
    }
  }
}