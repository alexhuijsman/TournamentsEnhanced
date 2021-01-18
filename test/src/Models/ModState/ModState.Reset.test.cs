using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class ModStateTests
  {

    [Test]
    public void Reset_LotteryResults_ShouldBe_NoWinners()
    {
      SetUpWithLotteryResults(Constants.LotteryResults.AllWinners);

      _sut.Reset();

      _sut.LotteryResults.ShouldBe(Constants.LotteryResults.NoWinners);
    }

    [Test]
    public void Reset_TournamentRecords_ShouldNotBe_Null()
    {
      _sut.Reset();

      Assert.IsNotNull(_sut.TournamentRecords);
    }

    [Test]
    public void Reset_TournamentRecords_ShouldBe_Empty()
    {
      _sut.Reset();

      _sut.TournamentRecords.Count.ShouldBe(0);
    }

    [Test]
    public void Reset_DaysSince_ShouldNotBe_Null()
    {
      _sut.Reset();

      Assert.IsNotNull(_sut.DaysSince);
    }

    [Test]
    public void Reset_DaysSince_ShouldContainDefaultNumberOfEntries()
    {
      _sut.Reset();

      _sut.DaysSince.Count.ShouldBe(Constants.DaysSinceTracker.TournamentTypes.Length);
    }

    [Test]
    public void Reset_DaysSince_ShouldContainDefaultKeys()
    {
      _sut.Reset();

      foreach (var key in Constants.DaysSinceTracker.TournamentTypes)
      {
        _sut.DaysSince.ContainsKey(key).ShouldBeTrue($"{key}");
      }
    }

    [Test]
    public void Reset_DaysSince_ShouldContainDefaultValues()
    {
      _sut.Reset();

      AssertDaysSinceContainsDefaultValues();
    }
  }
}