using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class BasicSettlementHostRequirementsComparerTest : TestBase
  {
    private const bool SettlementIsTown = true;
    private const bool SettlementIsNotTown = false;
    private const float ExactlyEnoughFood = Default.FoodStocksDecrease;
    private const float MoreThanEnoughFood = Default.FoodStocksDecrease + 1;
    private const float NotEnoughFood = Default.FoodStocksDecrease - 1;

    private BasicSettlementHostRequirementsComparerImpl _sut;
    private Mock<MBSettlement> _mockSettlement;
    private MBSettlement _settlement;
    private Mock<MBTown> _mockTown;
    private Mock<Settings> _mockSettings;

    public void SetUp(bool isTown, float foodStockValue = 0)
    {
      _sut = new BasicSettlementHostRequirementsComparerImpl();
      _mockSettlement = MockRepository.Create<MBSettlement>();
      _mockTown = MockRepository.Create<MBTown>();
      _mockSettings = MockRepository.Create<Settings>();
      _settlement = _mockSettlement.Object;

      _sut.Settings = _mockSettings.Object;

      _mockSettlement.SetupGet(settlement => settlement.IsTown).Returns(isTown);
      if (isTown)
      {
        _mockSettlement.SetupGet(settlement => settlement.Town).Returns(_mockTown.Object);
        _mockTown.SetupGet(town => town.FoodStocks).Returns(foodStockValue);
      }

      _mockSettings.SetupGet(settings => settings.FoodStocksDecrease).Returns(Default.FoodStocksDecrease);
    }

    [Test]
    public void Instance_IsSingleton()
    {
      BasicSettlementHostRequirementsComparer.Instance.ShouldBe(BasicSettlementHostRequirementsComparer.Instance);
    }

    [Test]
    public void MeetsRequirements_SettlementIsNotTown_ShouldReturnFalse()
    {
      SetUp(SettlementIsNotTown);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_SettlementIsTown_NotEnoughFood_ShouldReturnFalse()
    {
      SetUp(SettlementIsTown, NotEnoughFood);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_SettlementIsTown_ExactlyEnoughFood_ShouldReturnTrue()
    {
      SetUp(SettlementIsTown, ExactlyEnoughFood);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_SettlementIsTown_MoreThanEnoughFood_ShouldReturnTrue()
    {
      SetUp(SettlementIsTown, MoreThanEnoughFood);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    private class BasicSettlementHostRequirementsComparerImpl : BasicSettlementHostRequirementsComparer
    {
      public new Settings Settings { set => base.Settings = value; }

      public new bool MeetsRequirements(MBSettlement Settlement) => base.MeetsRequirements(Settlement);
    }

  }
}