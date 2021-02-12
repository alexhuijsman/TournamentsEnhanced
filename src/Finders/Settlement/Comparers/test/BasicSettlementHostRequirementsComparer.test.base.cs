using Moq;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public abstract class BasicSettlementHostRequirementsComparerTestBase<T> : TestBase
  where T : BasicSettlementHostRequirementsComparer, new()
  {
    protected const bool MeetsBaseRequirements = true;
    protected const bool FailsBaseRequirements = false;
    protected const bool SettlementIsTown = true;
    protected const bool SettlementIsNotTown = false;
    protected const float ExactlyEnoughFood = Default.FoodStocksDecrease;
    protected const float MoreThanEnoughFood = Default.FoodStocksDecrease + 1;
    protected const float NotEnoughFood = Default.FoodStocksDecrease - 1;
    protected const bool HasExistingTournament = true;
    protected const bool NoExistingTournament = false;

    protected T _sut;
    protected Mock<MBSettlement> _mockSettlement;
    protected MBSettlement _settlement;
    protected Mock<Settings> _mockSettings;
    protected Mock<MBTown> _mockTown;

    protected virtual void SetUp(bool isTown, float foodStockValue = 0)
    {
      _sut = new T();
      _mockSettlement = MockRepository.Create<MBSettlement>();
      _settlement = _mockSettlement.Object;
      _mockTown = MockRepository.Create<MBTown>();
      _mockSettings = MockRepository.Create<Settings>();

      _mockSettings.SetupGet(settings => settings.FoodStocksDecrease)
        .Returns(Default.FoodStocksDecrease);

      _mockSettlement.SetupGet(settlement => settlement.IsTown).Returns(isTown);
      if (isTown)
      {
        _mockSettlement.SetupGet(settlement => settlement.Town).Returns(_mockTown.Object);
        _mockTown.SetupGet(town => town.FoodStocks).Returns(foodStockValue);
      }
    }
  }
}