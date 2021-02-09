using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class KingdomLeaderHostComparerTest : TestBase
  {
    private const bool HeroDoesNotMeetBaseRequirements = false;
    private const bool HeroMeetsBaseRequirements = true;
    private const bool HeroIsInClanFaction = false;
    private const bool HeroIsInKingdomFaction = true;

    private KingdomLeaderHostComparerImpl _sut;
    private Mock<Settings> _mockSettings;
    private Mock<MBHero> _mockHero;
    private Mock<IMBFaction> _mockFaction;
    private MBHero _hero;

    public void SetUp(bool doesHeroMeetBaseRequirements, bool isHeroInKingdomFaction)
    {
      _sut = new KingdomLeaderHostComparerImpl();
      _mockSettings = MockRepository.Create<Settings>();
      _mockSettings.SetupGet(settings => settings.TournamentCost)
        .Returns(Default.TournamentCost);
      _sut.Settings = _mockSettings.Object;
      _mockHero = MockRepository.Create<MBHero>();
      _mockFaction = MockRepository.Create<IMBFaction>();
      _mockFaction.SetupGet(faction => faction.IsKingdomFaction).Returns(isHeroInKingdomFaction);
      _mockHero.SetupGet(hero => hero.IsFactionLeader).Returns(doesHeroMeetBaseRequirements);
      _mockHero.SetupGet(hero => hero.MapFaction).Returns(_mockFaction.Object);
      _mockHero.SetupGet(hero => hero.IsActive).Returns(true);
      _mockHero.SetupGet(hero => hero.Gold).Returns(Default.TournamentCost);
      _hero = _mockHero.Object;
    }

    [Test]
    public void Instance_ShouldBeSingleton()
    {
      KingdomLeaderHostComparer.Instance.ShouldBe(KingdomLeaderHostComparer.Instance);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsClanFaction_ShouldReturnFalse()
    {
      SetUp(HeroDoesNotMeetBaseRequirements, HeroIsInClanFaction);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsKingdomFaction_ShouldReturnFalse()
    {
      SetUp(HeroDoesNotMeetBaseRequirements, HeroIsInKingdomFaction);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsClanFaction_ShouldReturnFalse()
    {
      SetUp(HeroMeetsBaseRequirements, HeroIsInClanFaction);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsKingdomFaction_ShouldReturnTrue()
    {
      SetUp(HeroMeetsBaseRequirements, HeroIsInKingdomFaction);

      _sut.MeetsRequirements(_hero).ShouldBe(true);
    }

    private class KingdomLeaderHostComparerImpl : KingdomLeaderHostComparer
    {
      public new Settings Settings { set => base.Settings = value; }
      public new bool MeetsRequirements(MBHero Hero) => base.MeetsRequirements(Hero);
    }

  }
}