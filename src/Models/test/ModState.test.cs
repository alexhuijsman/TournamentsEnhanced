using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.Core;


namespace Test
{
  public partial class ModStateTest : TestBase
  {
    private ModStateImpl _sut;
    private Mock<DaysSinceTournamentTracker> _mockDaysSince;
    private Mock<TournamentRecordDictionary> _mockTournamentRecords;
    private Mock<MBMBRandom> _mockMBMBRandom;
    private Mock<System.Random> _mockRandom;

    //TODO remove use of [SetUp]
    [SetUp]
    public void SetUp()
    {
      SetUpWithLotteryResults(Constants.LotteryResults.NoWinners);
    }

    private void SetUpWithLotteryResults(int lotteryResults)
    {
      _mockDaysSince = MockRepository.Create<DaysSinceTournamentTracker>();
      _mockDaysSince.Setup(daysSince => daysSince.Reset());
      _mockDaysSince.Setup(daysSince => daysSince.IncrementDay());

      _mockTournamentRecords = MockRepository.Create<TournamentRecordDictionary>();

      _mockRandom = new Mock<System.Random>();
      _mockRandom.Setup(random => random.Next()).Returns(lotteryResults);

      _mockMBMBRandom = MockRepository.Create<MBMBRandom>();
      _mockMBMBRandom.SetupGet(mbMBRandom => mbMBRandom.DeterministicRandom)
        .Returns(_mockRandom.Object);

      _sut = new ModStateImpl();
      _sut.MBMBRandom = _mockMBMBRandom.Object;
    }

    private void SetUpSerializableModState(int lotteryResults = Constants.LotteryResults.NoWinners)
    {
      _sut.SerializableObject = new SerializableModState()
      {
        lotteryResults = lotteryResults,
        daysSince = _mockDaysSince.Object,
        tournamentRecords = _mockTournamentRecords.Object
      };
    }

    private void AssertDaysSinceContainsDefaultValues()
    {
      foreach (var keyValuePair in _sut.DaysSince)
      {
        keyValuePair.Value.ShouldBe(Int32.MaxValue, $"{keyValuePair.Key}");
      }
    }

    private enum TestEnum
    {
      N0, N1, N2, N3, N4, N5, N6, N7, N8, N9, N10, N11, N12, N13, N14, N15,
      N16, N17, N18, N19, N20, N21, N22, N23, N24, N25, N26, N27, N28, N29, N30,
      HighestValidValue = N30,
      InvalidValue = HighestValidValue + 1
    }

    private class ModStateImpl : ModState
    {
      public new MBMBRandom MBMBRandom { set => base.MBMBRandom = value; }
    }
  }
}