using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class ClanLeaderHostComparerTest : TestBase
  {
    private const bool HeroDoesNotMeetBaseRequirements = false;
    private const bool HeroMeetsBaseRequirements = true;
    private const bool HeroIsInKingdomFaction = false;
    private const bool HeroIsInClanFaction = true;

    private ClanLeaderHostComparerImpl _sut;
    private Mock<Settings> _mockSettings;
    private Mock<MBHero> _mockHero;
    private Mock<IMBFaction> _mockFaction;
    private MBHero _hero;

    public void SetUp(bool isHeroInClanFaction, bool doesHeroMeetBaseRequirements)
    {
      _sut = new ClanLeaderHostComparerImpl();
      _mockSettings = MockRepository.Create<Settings>();
      _mockSettings.SetupGet(settings => settings.TournamentCost)
        .Returns(Default.TournamentCost);
      _sut.Settings = _mockSettings.Object;
      _mockHero = MockRepository.Create<MBHero>();
      _mockFaction = MockRepository.Create<IMBFaction>();
      _mockFaction.SetupGet(faction => faction.IsClan).Returns(isHeroInClanFaction);
      _mockHero.SetupGet(hero => hero.IsFactionLeader).Returns(doesHeroMeetBaseRequirements);
      _mockHero.SetupGet(hero => hero.MapFaction).Returns(_mockFaction.Object);
      _mockHero.SetupGet(hero => hero.IsActive).Returns(true);
      _mockHero.SetupGet(hero => hero.Gold).Returns(Default.TournamentCost);
      _hero = _mockHero.Object;
    }

    [Test]
    public void Instance_IsSingleton()
    {
      ClanLeaderHostComparer.Instance.ShouldBe(ClanLeaderHostComparer.Instance);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsKingdomFaction_ShouldReturnFalse()
    {
      SetUp(HeroDoesNotMeetBaseRequirements, HeroIsInKingdomFaction);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsClanFaction_ShouldReturnFalse()
    {
      SetUp(HeroDoesNotMeetBaseRequirements, HeroIsInClanFaction);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsKingdomFaction_ShouldReturnFalse()
    {
      SetUp(HeroMeetsBaseRequirements, HeroIsInKingdomFaction);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsClanFaction_ShouldReturnTrue()
    {
      SetUp(HeroMeetsBaseRequirements, HeroIsInClanFaction);

      _sut.MeetsRequirements(_hero).ShouldBe(true);
    }

    private class ClanLeaderHostComparerImpl : ClanLeaderHostComparer
    {
      public new Settings Settings { set => base.Settings = value; }
      public new bool MeetsRequirements(MBHero Hero) => base.MeetsRequirements(Hero);
    }

  }
}