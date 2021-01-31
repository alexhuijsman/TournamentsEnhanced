using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class BasicHeroHostRequirementsComparerTest : TestBase
  {
    private const bool HeroIsNotActive = false;
    private const bool HeroIsActive = true;
    private const bool HeroCanNotAffordCost = false;
    private const bool HeroCanAffordCost = true;

    private BasicHeroHostRequirementsComparerImpl _sut;
    private Mock<Settings> _mockSettings;
    private Mock<MBHero> _mockHero;
    private MBHero _hero;

    public void SetUp(bool isHeroActive, bool canHeroAffordCost)
    {
      _sut = new BasicHeroHostRequirementsComparerImpl();
      _mockSettings = MockRepository.Create<Settings>();
      _mockSettings.SetupGet(settings => settings.TournamentCost)
        .Returns(Default.TournamentCost);
      _sut.Settings = _mockSettings.Object;
      _mockHero = MockRepository.Create<MBHero>();
      _mockHero.SetupGet(hero => hero.IsActive).Returns(isHeroActive);
      _mockHero.SetupGet(hero => hero.Gold).Returns(canHeroAffordCost ? Default.TournamentCost : 0);
      _hero = _mockHero.Object;
    }

    [Test]
    public void Instance_IsSingleton()
    {
      BasicHeroHostRequirementsComparer.Instance.ShouldBe(BasicHeroHostRequirementsComparer.Instance);
    }

    [Test]
    public void MeetsRequirements_IsNotActive_CanNotAfford_ShouldReturnFalse()
    {
      SetUp(HeroIsNotActive, HeroCanNotAffordCost);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_IsNotActive_CanAfford_ShouldReturnFalse()
    {
      SetUp(HeroIsNotActive, HeroCanAffordCost);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_IsActive_CanNotAfford_ShouldReturnFalse()
    {
      SetUp(HeroIsActive, HeroCanNotAffordCost);

      _sut.MeetsRequirements(_hero).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_IsActive_CanAfford_ShouldReturnTrue()
    {
      SetUp(HeroIsActive, HeroCanAffordCost);

      _sut.MeetsRequirements(_hero).ShouldBe(true);
    }

    private class BasicHeroHostRequirementsComparerImpl : BasicHeroHostRequirementsComparer
    {
      public new Settings Settings { set => base.Settings = value; }
      public new bool MeetsRequirements(MBHero Hero) => base.MeetsRequirements(Hero);
    }

  }
}