using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Comparers.Clan;
using TournamentsEnhanced.Finder.Comparers.Faction;
using TournamentsEnhanced.Wrappers.CampaignSystem;


namespace Test
{
  public class BasicFactionHostRequirementsComparerTest : TestBase
  {
    private BasicFactionHostRequirementsComparerImpl _sut;
    private Mock<MBFaction> _mockFaction;
    private MBFaction _faction;
    private Mock<HeroFinder> _mockHeroFinder;

    public void SetUp(Func<List<MBSettlement>> factionSettlementsFunc)
    {
      _sut = new BasicFactionHostRequirementsComparerImpl();
      _mockFaction = MockRepository.Create<MBFaction>();
      _faction = _mockFaction.Object;
      _mockHeroFinder = MockRepository.Create<HeroFinder>();
      _sut.HeroFinder = _mockHeroFinder.Object;

      _mockFaction.SetupGet(faction => faction.Settlements).Returns(factionSettlementsFunc);
    }

    [Test]
    public void Instance_IsSingleton()
    {
      BasicClanHostRequirementsComparer.Instance.ShouldBe(BasicClanHostRequirementsComparer.Instance);
    }

    [Test]
    public void MeetsRequirements_NoSettlements_ShouldReturnFalse()
    {
      SetUp(NoSettlementsScenario);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTownsScenario1_ShouldReturnFalse()
    {
      SetUp(NoTownsScenario1);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTownsScenario2_ShouldReturnFalse()
    {
      SetUp(NoTownsScenario2);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTownsScenario3_ShouldReturnFalse()
    {
      SetUp(NoTownsScenario3);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario1_ShouldReturnFalse()
    {
      SetUp(WithTownsScenario1);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario2_ShouldReturnFalse()
    {
      SetUp(WithTownsScenario2);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario3_ShouldReturnFalse()
    {
      SetUp(WithTownsScenario3);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario4_ShouldReturnFalse()
    {
      SetUp(WithTownsScenario4);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario5_ShouldReturnFalse()
    {
      SetUp(WithTownsScenario5);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario6_ShouldReturnFalse()
    {
      SetUp(WithTownsScenario6);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario7_ShouldReturnFalse()
    {
      SetUp(WithTownsScenario7);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario8_ShouldReturnFalse()
    {
      SetUp(WithTownsScenario8);

      _sut.MeetsRequirements(_faction).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario1_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario1);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario2_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario2);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario3_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario3);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario4_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario4);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario5_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario5);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario6_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario6);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario7_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario7);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario8_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario8);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario9_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario9);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsAndPayorsScenario10_ShouldReturnTrue()
    {
      SetUp(WithTownsAndPayorsScenario10);

      _sut.MeetsRequirements(_faction).ShouldBe(true);
    }

    private List<MBSettlement> NoSettlementsScenario() => new List<MBSettlement>();
    private List<MBSettlement> NoTownsScenario1() => new List<MBSettlement>()
    {
      GetSettlement(),
    };

    private List<MBSettlement> NoTownsScenario2() => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(),
    };

    private List<MBSettlement> NoTownsScenario3() => new List<MBSettlement>()
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

    private List<MBSettlement> WithTownsScenario1() => new List<MBSettlement>()
    {
      GetSettlement(true),
    };

    private List<MBSettlement> WithTownsScenario2() => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(true),
    };

    private List<MBSettlement> WithTownsScenario3() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(),
    };

    private List<MBSettlement> WithTownsScenario4() => new List<MBSettlement>()
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

    private List<MBSettlement> WithTownsScenario5() => new List<MBSettlement>()
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

    private List<MBSettlement> WithTownsScenario6() => new List<MBSettlement>()
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

    private List<MBSettlement> WithTownsScenario7() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(true),
      GetSettlement(true),
    };

    private List<MBSettlement> WithTownsScenario8() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(true),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario1() => new List<MBSettlement>()
    {
      GetSettlement(true, true),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario2() => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(true, true),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario3() => new List<MBSettlement>()
    {
      GetSettlement(true, true),
      GetSettlement(),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario4() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(true, true),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario5() => new List<MBSettlement>()
    {
      GetSettlement(true, true),
      GetSettlement(true),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario6() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(true),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(true),
      GetSettlement(true, true),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario7() => new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(true, true),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario8() => new List<MBSettlement>()
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
      GetSettlement(true, true),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario9() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(true, true),
      GetSettlement(true),
    };

    private List<MBSettlement> WithTownsAndPayorsScenario10() => new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(true, true),
    };

    private MBSettlement GetSettlement(bool isTown = false, bool ownerMeetsRequirements = false)
    {
      var mockClanLeader = MockRepository.Create<MBHero>();
      var mockOwnerClan = MockRepository.Create<MBClan>();
      mockOwnerClan.SetupGet(ownerClan => ownerClan.Leader).Returns(mockClanLeader.Object);
      var mockSettlement = MockRepository.Create<MBSettlement>();
      mockSettlement.SetupGet(settlement => settlement.IsTown).Returns(isTown);
      mockSettlement.SetupGet(settlement => settlement.OwnerClan).Returns(mockOwnerClan.Object);

      var mockFindHeroResults = MockRepository.Create<FindHeroResult>();
      mockFindHeroResults
        .SetupGet(findHeroResults => findHeroResults.Succeeded)
        .Returns(ownerMeetsRequirements);

      _mockHeroFinder.Setup(
        heroFinder => heroFinder.FindHostsThatMeetBasicRequirements(mockClanLeader.Object))
          .Returns(mockFindHeroResults.Object);

      return mockSettlement.Object;
    }

    private class BasicFactionHostRequirementsComparerImpl : BasicFactionHostRequirementsComparer
    {
      public new HeroFinder HeroFinder { set => base.HeroFinder = value; }
      public new bool MeetsRequirements(MBFaction faction) => base.MeetsRequirements(faction);
    }
  }
}