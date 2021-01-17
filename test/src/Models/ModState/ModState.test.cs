using System;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Models.ModState;

namespace TournamentsEnhanced.UnitTests
{
  public class ModStateTests
  {
    private ModStateImpl sut;

    [SetUp]
    public void SetUp()
    {
      sut = new ModStateImpl();
    }

    [Test]
    public void Ctor_TournamentRecords_IsNotNull()
    {
      Assert.IsNotNull(sut.TournamentRecords);
    }

    [Test]
    public void Ctor_TournamentRecords_IsEmpty()
    {
      sut.TournamentRecords.Count.ShouldBe(0);
    }

    [Test]
    public void Ctor_DaysSince_IsNotNull()
    {
      Assert.IsNotNull(sut.DaysSince);
    }

    [Test]
    public void Ctor_DaysSince_ContainsExpectedNumberOfEntries()
    {
      sut.DaysSince.Count.ShouldBe(Constants.DaysSince.TournamentTypes.Length);
    }

    [Test]
    public void Ctor_DaysSince_ContainsExpectedKeys()
    {
      foreach (var key in Constants.DaysSince.TournamentTypes)
      {
        sut.DaysSince.ContainsKey(key).ShouldBeTrue($"{key}");
      }
    }

    [Test]
    public void Ctor_DaysSince_ContainsExpectedValues()
    {
      foreach (var keyValuePair in sut.DaysSince)
      {
        keyValuePair.Value.ShouldBe(Int32.MaxValue, $"{keyValuePair.Key}");
      }
    }

    private class ModStateImpl : ModState
    {
      public ModStateImpl() : base() { }
    }
  }
}