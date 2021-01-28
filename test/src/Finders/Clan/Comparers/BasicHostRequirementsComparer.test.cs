using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder.Comparers.Clan;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.UnitTests
{
  public class BasicHostRequirementsComparerTests
  {
    private BasicHostRequirementsComparerImpl _sut;
    private readonly List<MBSettlement> NoSettlements = new List<MBSettlement>();
    private readonly List<MBSettlement> NoTownsScenario1 = new List<MBSettlement>()
    {
      GetSettlement(),
    };

    private readonly List<MBSettlement> NoTownsScenario2 = new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(),
    };

    private readonly List<MBSettlement> NoTownsScenario3 = new List<MBSettlement>()
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

    private readonly List<MBSettlement> WithTownsScenario1 = new List<MBSettlement>()
    {
      GetSettlement(true),
    };

    private readonly List<MBSettlement> WithTownsScenario2 = new List<MBSettlement>()
    {
      GetSettlement(),
      GetSettlement(true),
    };

    private readonly List<MBSettlement> WithTownsScenario3 = new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(),
    };

    private readonly List<MBSettlement> WithTownsScenario4 = new List<MBSettlement>()
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

    private readonly List<MBSettlement> WithTownsScenario5 = new List<MBSettlement>()
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

    private readonly List<MBSettlement> WithTownsScenario6 = new List<MBSettlement>()
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

    private readonly List<MBSettlement> WithTownsScenario7 = new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(true),
      GetSettlement(true),
    };

    private readonly List<MBSettlement> WithTownsScenario8 = new List<MBSettlement>()
    {
      GetSettlement(true),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(),
      GetSettlement(true),
    };

    [SetUp]
    public void SetUp()
    {
      _sut = new BasicHostRequirementsComparerImpl();
    }

    [Test]
    public void Instance_IsSingleton()
    {
      BasicHostRequirementsComparer.Instance.ShouldBe(BasicHostRequirementsComparer.Instance);
    }

    [Test]
    public void MeetsRequirements_NoSettlements_ShouldReturnFalse()
    {
      var mockClan = new Mock<MBClan>();
      mockClan.SetupGet(clan => clan.Settlements).Returns(NoSettlements);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTownsScenario1_ShouldReturnFalse()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(NoTownsScenario1);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTownsScenario2_ShouldReturnFalse()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(NoTownsScenario2);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_NoTownsScenario3_ShouldReturnFalse()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(NoTownsScenario3);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario2_ShouldReturnTrue()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(WithTownsScenario2);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario3_ShouldReturnTrue()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(WithTownsScenario3);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario4_ShouldReturnTrue()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(WithTownsScenario4);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario5_ShouldReturnTrue()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(WithTownsScenario5);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario6_ShouldReturnTrue()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(WithTownsScenario6);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario7_ShouldReturnTrue()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(WithTownsScenario7);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario8_ShouldReturnTrue()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(WithTownsScenario8);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_WithTownsScenario1_ShouldReturnTrue()
    {
      var mockClan = new Mock<MBClan>();

      mockClan.SetupGet(clan => clan.Settlements).Returns(WithTownsScenario1);

      _sut.MeetsRequirements(mockClan.Object).ShouldBe(true);
    }

    private static MBSettlement GetSettlement(bool isTown = false)
    {
      var mockSettlement = new Mock<MBSettlement>();
      mockSettlement.SetupGet(settlement => settlement.IsTown).Returns(isTown);
      return mockSettlement.Object;
    }

    private class BasicHostRequirementsComparerImpl : BasicHostRequirementsComparer
    {
      public new bool MeetsRequirements(MBClan clan) => base.MeetsRequirements(clan);
    }
  }
}