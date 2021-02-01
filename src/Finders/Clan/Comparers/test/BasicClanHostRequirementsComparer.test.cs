using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder.Comparers.Clan;
using TournamentsEnhanced.Wrappers.CampaignSystem;


namespace Test
{
  public class BasicClanHostRequirementsComparerTest : TestBase
  {
    private BasicClanHostRequirementsComparerImpl _sut;
    private Mock<MBClan> _mockClan;
    private MBClan _clan;

    public void SetUp(List<MBSettlement> clanSettlements)
    {
      _sut = new BasicClanHostRequirementsComparerImpl();
      _mockClan = MockRepository.Create<MBClan>();
      _mockClan.SetupGet(clan => clan.Settlements).Returns(clanSettlements);
      _clan = _mockClan.Object;
    }

    [Test]
    public void Instance_IsSingleton()
    {
      BasicClanHostRequirementsComparer.Instance.ShouldBe(BasicClanHostRequirementsComparer.Instance);
    }

    [Test]
    public void MeetsRequirements_NoSettlements_ShouldReturnFalse()
    {
      SetUp(GetNoSettlementsScenario());

      _sut.MeetsRequirements(_clan).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTownsScenario1_ShouldReturnFalse()
    {
      SetUp(GetNoTownsScenario1());

      _sut.MeetsRequirements(_clan).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTownsScenario2_ShouldReturnFalse()
    {
      SetUp(GetNoTownsScenario2());

      _sut.MeetsRequirements(_clan).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTownsScenario3_ShouldReturnFalse()
    {
      SetUp(GetNoTownsScenario3());

      _sut.MeetsRequirements(_clan).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario2_ShouldReturnTrue()
    {
      SetUp(GetWithTownsScenario2());

      _sut.MeetsRequirements(_clan).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario3_ShouldReturnTrue()
    {
      SetUp(GetWithTownsScenario3());

      _sut.MeetsRequirements(_clan).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario4_ShouldReturnTrue()
    {
      SetUp(GetWithTownsScenario4());

      _sut.MeetsRequirements(_clan).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario5_ShouldReturnTrue()
    {
      SetUp(GetWithTownsScenario5());

      _sut.MeetsRequirements(_clan).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario6_ShouldReturnTrue()
    {
      SetUp(GetWithTownsScenario6());

      _sut.MeetsRequirements(_clan).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario7_ShouldReturnTrue()
    {
      SetUp(GetWithTownsScenario7());

      _sut.MeetsRequirements(_clan).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario8_ShouldReturnTrue()
    {
      SetUp(GetWithTownsScenario8());

      _sut.MeetsRequirements(_clan).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario1_ShouldReturnTrue()
    {
      SetUp(GetWithTownsScenario1());

      _sut.MeetsRequirements(_clan).ShouldBe(true);
    }

    private List<MBSettlement> GetNoSettlementsScenario() => new List<MBSettlement>();
    private List<MBSettlement> GetNoTownsScenario1() => new List<MBSettlement>()
    {
      GetSettlement(),
    };

    private List<MBSettlement> GetNoTownsScenario2() => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(),
    };

    private List<MBSettlement> GetNoTownsScenario3() => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
    };

    private List<MBSettlement> GetWithTownsScenario1() => new List<MBSettlement>()
    {
      GetSettlement(true),
    };

    private List<MBSettlement> GetWithTownsScenario2() => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(true),
    };

    private List<MBSettlement> GetWithTownsScenario3() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(),
    };

    private List<MBSettlement> GetWithTownsScenario4() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
    };

    private List<MBSettlement> GetWithTownsScenario5() => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(true),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
    };

    private List<MBSettlement> GetWithTownsScenario6() => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(true),
    };

    private List<MBSettlement> GetWithTownsScenario7() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(true),
      GetSettlement(true),
    };

    private List<MBSettlement> GetWithTownsScenario8() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(true),
    };

    private MBSettlement GetSettlement(bool isTown = false)
    {
      var mockSettlement = MockRepository.Create<MBSettlement>();
      mockSettlement.SetupGet(settlement => settlement.IsTown).Returns(isTown);
      return mockSettlement.Object;
    }

    private class BasicClanHostRequirementsComparerImpl : BasicClanHostRequirementsComparer
    {
      public new bool MeetsRequirements(MBClan clan) => base.MeetsRequirements(clan);
    }

  }
}