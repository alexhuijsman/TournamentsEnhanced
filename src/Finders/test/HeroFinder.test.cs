using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class HeroFinderTest : TestBase<HeroFinderImpl>
  {
    protected Mock<MBHero> _mockHero;
    protected Mock<IMBFaction> _mockFaction;

    protected override void SetUp()
    {
      base.SetUp();

      _mockFaction = MockRepository.Create<IMBFaction>();
      _mockFaction.SetupGet(f => f.IsKingdomFaction).Returns(true);
      _mockFaction.SetupGet(f => f.IsClan).Returns(true);

      _mockHero = MockRepository.Create<MBHero>();
      _mockHero.SetupGet(Hero => Hero.IsNull).Returns(false);
      _mockHero.SetupGet(Hero => Hero.IsActive).Returns(true);
      _mockHero.SetupGet(Hero => Hero.IsFactionLeader).Returns(true);
      _mockHero.SetupGet(Hero => Hero.Gold).Returns(Default.TournamentCost);
      _mockHero.SetupGet(Hero => Hero.MapFaction).Returns(_mockFaction.Object);
    }

    [Test]
    public void Instance_ShouldBeSingleton()
    {
      HeroFinder.Instance.ShouldBe(HeroFinder.Instance);
    }

    [Test]
    public void FindHeroThatMeetsBasicHostRequirements_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindHostsThatMeetBasicRequirements(_mockHero.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockHero.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }

    [Test]
    public void FindKingdomLeaders_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindKingdomLeaders(_mockHero.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockHero.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }

    [Test]
    public void FindClanLeaders_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindClanLeaders(_mockHero.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockHero.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }

    [Test]
    public void FindFactionLeaders_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindFactionLeaders(_mockHero.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockHero.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }

    [Test]
    public void FindHostsFromWeddedHeroes_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindHostsFromWeddedHeroes(_mockHero.Object, _mockHero.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockHero.Object),
            () => result.HasRunnerUp.ShouldBe(true),
            () => result.RunnerUp.ShouldBe(_mockHero.Object)
        );
    }
  }

  public class HeroFinderImpl : HeroFinder
  {
    public HeroFinderImpl() { }
  }
}