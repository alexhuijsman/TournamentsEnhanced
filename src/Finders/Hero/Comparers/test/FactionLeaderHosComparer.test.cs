using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class FactionLeaderHostComparerTest : TestBase
  {
    private const bool HeroDoesNotMeetBaseRequirements = false;
    private const bool HeroMeetsBaseRequirements = true;
    private const bool HeroIsNotFactionLeader = false;
    private const bool HeroIsFactionLeader = true;

    private FactionLeaderHostComparerImpl _sut;
    private Mock<Settings> _mockSettings;
    private Mock<MBHero> _mockHero;
    private MBHero _hero;

    public void SetUp(bool doesHeroMeetBaseRequirements, bool IsHeroFactionLeader)
    {
      _sut = new FactionLeaderHostComparerImpl();
      _mockSettings = MockRepository.Create<Settings>();
      _mockSettings.SetupGet(settings => settings.TournamentCost)
        .Returns(Default.TournamentCost);
      _sut.Settings = _mockSettings.Object;
      _mockHero = MockRepository.Create<MBHero>();
      _mockHero.SetupGet(hero => hero.IsFactionLeader).Returns(IsHeroFactionLeader);
      _mockHero.SetupGet(hero => hero.IsActive).Returns(doesHeroMeetBaseRequirements);
      _mockHero.SetupGet(hero => hero.Gold).Returns(Default.TournamentCost);
      _hero = _mockHero.Object;
    }

    [Test]
    public void Instance_IsSingleton()
    {
      FactionLeaderHostComparer.Instance.ShouldBe(FactionLeaderHostComparer.Instance);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsNotFactionLeader_ShouldReturnFalse()
    {
      SetUp(HeroDoesNotMeetBaseRequirements, HeroIsNotFactionLeader);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsFactionLeader_ShouldReturnFalse()
    {
      SetUp(HeroDoesNotMeetBaseRequirements, HeroIsFactionLeader);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsNotFactionLeader_ShouldReturnFalse()
    {
      SetUp(HeroMeetsBaseRequirements, HeroIsNotFactionLeader);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsFactionLeader_ShouldReturnTrue()
    {
      SetUp(HeroMeetsBaseRequirements, HeroIsFactionLeader);

      _sut.MeetsRequirements(_hero).ShouldBe(true);
    }

    private class FactionLeaderHostComparerImpl : FactionLeaderHostComparer
    {
      public new Settings Settings { set => base.Settings = value; }
      public new bool MeetsRequirements(MBHero Hero) => base.MeetsRequirements(Hero);
    }

  }
}