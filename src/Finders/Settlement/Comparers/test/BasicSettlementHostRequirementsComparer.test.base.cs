using Moq;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Settlement;
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

      CreateMockSettlementResults results = CreateMockSettlement(isTown, foodStockValue);
      _mockSettlement = results.mockSettlement;
      _settlement = _mockSettlement.Object;
      _mockTown = results.mockTown;
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

    protected CreateMockSettlementResults CreateMockSettlement(bool isTown, float foodStockValue)
    {
      var mockSettlement = MockRepository.Create<MBSettlement>();
      var mockTown = MockRepository.Create<MBTown>();

      mockSettlement.SetupGet(settlement => settlement.IsTown).Returns(isTown);

      if (isTown)
      {
        mockSettlement.SetupGet(settlement => settlement.Town).Returns(mockTown.Object);
        mockTown.SetupGet(town => town.FoodStocks).Returns(foodStockValue);
      }

      return new CreateMockSettlementResults()
      {
        mockSettlement = mockSettlement,
        mockTown = mockTown
      };
    }

    protected struct CreateMockSettlementResults
    {
      public Mock<MBSettlement> mockSettlement;
      public Mock<MBTown> mockTown;
    }
  }
}