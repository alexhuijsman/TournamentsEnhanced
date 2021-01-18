using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class ModStateTests
  {
    [Test]
    public void Ctor_DaysSince_ShouldNotBe_Null()
    {
      Assert.IsNotNull(_sut.DaysSince);
    }

    [Test]
    public void Ctor_DaysSince_ShouldContain_DefaultNumberOfEntries()
    {
      _sut.DaysSince.Count.ShouldBe(Constants.DaysSinceTracker.TournamentTypes.Length);
    }

    [Test]
    public void Ctor_DaysSince_ShouldContain_DefaultKeys()
    {
      foreach (var key in Constants.DaysSinceTracker.TournamentTypes)
      {
        _sut.DaysSince.ContainsKey(key).ShouldBeTrue($"{key}");
      }
    }

    [Test]
    public void Ctor_DaysSince_ShouldContain_DefaultValues()
    {
      AssertDaysSinceContainsDefaultValues();
    }


    [Test]
    public void Ctor_LotteryResults_ShouldBe_NoWinners()
    {
      _sut = new ModStateImpl();

      _sut.LotteryResults.ShouldBe(Constants.LotteryResults.NoWinners);
    }

    [Test]
    public void Ctor_TournamentRecords_ShouldNotBeNull()
    {
      _sut = new ModStateImpl();

      Assert.IsNotNull(_sut.TournamentRecords);
    }

    [Test]
    public void Ctor_TournamentRecords_ShouldBeEmpty()
    {
      _sut = new ModStateImpl();

      _sut.TournamentRecords.Count.ShouldBe(0);
    }
  }
}