using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace Test
{
  public class BasicSettlementHostRequirementsComparerTest
    : BasicSettlementHostRequirementsComparerTestBase<BasicSettlementHostRequirementsComparerImpl>
  {
    protected override void SetUp(bool isTown, float foodStockValue = 0)
    {
      base.SetUp(isTown, foodStockValue);

      _sut.Settings = _mockSettings.Object;
    }

    [Test]
    public void Instance_ShouldBeSingleton()
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
  }

  public class BasicSettlementHostRequirementsComparerImpl : BasicSettlementHostRequirementsComparer
  {
    public new Settings Settings { set => base.Settings = value; }
    public new bool MeetsRequirements(MBSettlement Settlement) => base.MeetsRequirements(Settlement);
  }
}