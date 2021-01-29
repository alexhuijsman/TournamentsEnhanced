using System;
using Moq;
using NUnit.Framework;

using TournamentsEnhanced;


namespace Test
{
  public partial class ModStateTest
  {
    [Test]
    public void DailyTick_ShouldCallIncrementDay()
    {
      SetUpSerializableModState();

      _sut.DailyTick();

      _mockDaysSince.Verify(daysSince => daysSince.IncrementDay(), Times.Once);
    }

    [Test]
    public void DailyTick_ShouldCallNext()
    {
      SetUpSerializableModState();

      _sut.DailyTick();

      _mockRandom.Verify(random => random.Next(), Times.Once);
    }

  }
}