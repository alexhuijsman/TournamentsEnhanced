using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Comparers.Kingdom;
using TournamentsEnhanced.Wrappers.CampaignSystem;


namespace Test
{
  public class BasicKingdomHostRequirementsComparerTest : TestBase
  {
    private const bool PayorMeetsRequirements = true;
    private const bool PayorFailsRequirements = false;

    private BasicKingdomHostRequirementsComparerImpl _sut;
    private Mock<HeroFinder> _mockHeroFinder;
    private Mock<MBKingdom> _mockKingdom;
    private MBKingdom _kingdom;

    public void SetUp(Func<bool, List<MBSettlement>> getSettlementsFunc, bool payorMeetsRequirements = false)
    {
      _sut = new BasicKingdomHostRequirementsComparerImpl();
      _mockHeroFinder = MockRepository.Create<HeroFinder>();
      _sut.HeroFinder = _mockHeroFinder.Object;
      _mockKingdom = MockRepository.Create<MBKingdom>();
      _mockKingdom.SetupGet(Kingdom => Kingdom.Settlements)
        .Returns(getSettlementsFunc(payorMeetsRequirements));
      _kingdom = _mockKingdom.Object;
    }

    [Test]
    public void Instance_ShouldBeSingleton()
    {
      BasicKingdomHostRequirementsComparer.Instance.ShouldBe(BasicKingdomHostRequirementsComparer.Instance);
    }

    [Test]
    public void MeetsRequirements_NoSettlements_ShouldReturnFalse()
    {
      SetUp(GetNoSettlements);

      _sut.MeetsRequirements(_kingdom).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTowns_ShouldReturnFalse()
    {
      SetUp(GetSettlementsNoTowns);

      _sut.MeetsRequirements(_kingdom).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTowns_PayorFailsRequirements_ShouldReturnFalse()
    {
      SetUp(GetSettlementsWithTowns, PayorFailsRequirements);

      _sut.MeetsRequirements(_kingdom).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTowns_PayorMeetsRequirements_ShouldReturnTrue()
    {
      SetUp(GetSettlementsWithTowns, PayorMeetsRequirements);

      _sut.MeetsRequirements(_kingdom).ShouldBe(true);
    }

    private List<MBSettlement> GetNoSettlements(bool payorMeetsRequirements = false) => new List<MBSettlement>();
    private List<MBSettlement> GetSettlementsNoTowns(bool payorMeetsRequirements = false) => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(false, payorMeetsRequirements),
      GetSettlement(),
    };
    private List<MBSettlement> GetSettlementsWithTowns(bool payorMeetsRequirements = false) => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(true, payorMeetsRequirements),
      GetSettlement(),
    };

    private MBSettlement GetSettlement(bool isTown = false, bool payorMeetsRequirements = false)
    {
      var mockSettlement = MockRepository.Create<MBSettlement>();
      var mockClan = MockRepository.Create<MBClan>();
      var mockHero = MockRepository.Create<MBHero>();
      var mockFindHeroResult = MockRepository.Create<FindHeroResult>();
      mockFindHeroResult.SetupGet(findHeroResult => findHeroResult.Succeeded).Returns(payorMeetsRequirements);
      _mockHeroFinder.Setup<FindHeroResult>(
        heroFinder => heroFinder.FindHostsThatMeetBasicRequirements(mockHero.Object))
          .Returns(mockFindHeroResult.Object);
      mockClan.SetupGet(clan => clan.Leader).Returns(mockHero.Object);
      mockSettlement.SetupGet(settlement => settlement.IsTown).Returns(isTown);
      mockSettlement.SetupGet(settlement => settlement.OwnerClan).Returns(mockClan.Object);
      return mockSettlement.Object;
    }

    private class BasicKingdomHostRequirementsComparerImpl : BasicKingdomHostRequirementsComparer
    {
      public new HeroFinder HeroFinder { set => base.HeroFinder = value; }
      public new bool MeetsRequirements(MBKingdom Kingdom) => base.MeetsRequirements(Kingdom);
    }

  }
}