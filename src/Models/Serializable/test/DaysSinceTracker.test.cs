using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.DaysSinceTracker;

namespace Test
{
  public class DaysSinceTrackerTest : TestBase<DaysSinceTrackerImpl>
  {
    protected const int SomeIntValue = 8192;
    protected readonly int NumberOfTestTypeValues = Enum.GetNames(typeof(TestType)).Length;

    public enum TestType
    {
      First,
      Second,
      Third
    }

    protected TestType[] TestTypes =
    {
      TestType.First,
      TestType.Second,
      TestType.Third
    };

    protected override void SetUp()
    {
      _sut = new DaysSinceTrackerImpl(TestTypes);
    }

    [Test]
    public void Ctor_ShouldInitializeDictionaryToExpected()
    {
      SetUp();

      _sut.ShouldSatisfyAllConditions(
        () => _sut.Count.ShouldBe(NumberOfTestTypeValues),
        () => _sut.ShouldContainKeyAndValue(TestType.First, Default.DictionaryValue),
        () => _sut.ShouldContainKeyAndValue(TestType.Second, Default.DictionaryValue),
        () => _sut.ShouldContainKeyAndValue(TestType.Third, Default.DictionaryValue)
      );
    }

    [Test]
    public void IncrementDay_ShouldNotIncrementDefaultDictionaryValues()
    {
      SetUp();

      _sut.IncrementDay();

      _sut.ShouldSatisfyAllConditions(
        () => _sut.Count.ShouldBe(NumberOfTestTypeValues),
        () => _sut.ShouldContainKeyAndValue(TestType.First, Default.DictionaryValue),
        () => _sut.ShouldContainKeyAndValue(TestType.Second, Default.DictionaryValue),
        () => _sut.ShouldContainKeyAndValue(TestType.Third, Default.DictionaryValue)
      );
    }

    [Test]
    public void IncrementDay_ShouldIncrementNonDefaultDictionaryValues()
    {
      SetUp();

      _sut[TestType.First] = 0;
      _sut[TestType.Third] = SomeIntValue;

      _sut.IncrementDay();

      _sut.ShouldSatisfyAllConditions(
        () => _sut.Count.ShouldBe(NumberOfTestTypeValues),
        () => _sut.ShouldContainKeyAndValue(TestType.First, 1),
        () => _sut.ShouldContainKeyAndValue(TestType.Second, Default.DictionaryValue),
        () => _sut.ShouldContainKeyAndValue(TestType.Third, SomeIntValue + 1)
      );
    }

    [Test]
    public void Reset_ShouldResetDictionaryToDefaultValues()
    {
      SetUp();

      _sut[TestType.First] = 0;
      _sut[TestType.Third] = SomeIntValue;

      _sut.Reset();

      _sut.ShouldSatisfyAllConditions(
        () => _sut.Count.ShouldBe(NumberOfTestTypeValues),
        () => _sut.ShouldContainKeyAndValue(TestType.First, Default.DictionaryValue),
        () => _sut.ShouldContainKeyAndValue(TestType.Second, Default.DictionaryValue),
        () => _sut.ShouldContainKeyAndValue(TestType.Third, Default.DictionaryValue)
      );
    }
  }

  public class DaysSinceTrackerImpl : DaysSinceTracker<DaysSinceTrackerTest.TestType>
  {


    public DaysSinceTrackerImpl() { }

    public DaysSinceTrackerImpl(params DaysSinceTrackerTest.TestType[] types) : base(types)
    {
    }
  }
}