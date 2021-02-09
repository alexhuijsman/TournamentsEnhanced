using System;
using Moq;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Models.Serializable;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class ExistingTournamentComparerTestBase<T>
    : BasicSettlementHostRequirementsComparerTestBase<T>
    where T : ExistingTournamentComparer, new()
  {
    protected const bool CanOverrideExisting = true;
    protected const bool CannotOverrideExisting = false;

    protected override void SetUp(bool isTown, float foodStockValue = 0)
    {
      throw new InvalidOperationException();
    }

    protected virtual void SetUp(bool meetsBaseRequirements, bool hasExistingTournament)
    {
      base.SetUp(
        meetsBaseRequirements ? SettlementIsTown : SettlementIsNotTown,
        meetsBaseRequirements ? ExactlyEnoughFood : NotEnoughFood);

      _mockModState = MockRepository.Create<ModState>();
      _mockTournamentRecords = MockRepository.Create<TournamentRecordDictionary>();

      _mockModState.SetupGet(modState => modState.TournamentRecords)
        .Returns(_mockTournamentRecords.Object);

      _mockTournamentRecords.Setup<bool>(
        tournamentRecords => tournamentRecords.ContainsSettlement(_mockSettlement.Object))
          .Returns(hasExistingTournament);

      _mockSettings.SetupGet(settings => settings.FoodStocksDecrease)
        .Returns(Default.FoodStocksDecrease);
    }
  }
}