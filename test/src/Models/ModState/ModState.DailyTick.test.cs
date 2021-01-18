using System;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class ModStateTests
  {
    [Test]
    public void DailyTick_ShouldCallIncrementDay()
    {
      _sut.DailyTick();

      _mockDaysSince.Verify(daysSince => daysSince.IncrementDay(), Times.Once);
    }

    [Test]
    public void DailyTick_ShouldCallNext()
    {
      _sut.DailyTick();

      _mockRandom.Verify(random => random.Next(), Times.Once);
    }
  }
}