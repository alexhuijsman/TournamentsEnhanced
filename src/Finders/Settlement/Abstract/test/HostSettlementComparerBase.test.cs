using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder.Comparers;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace Test
{
  public class HostSettlementComparerBaseTest : TestBase
  {
    private HostSettlementComparerBaseImpl _sut;
    private Mock<ModState> _mockModState;
    private Mock<TournamentRecordDictionary> _mockTournamentRecords;
    private Mock<MBSettlement> _mockSettlement;
    private MBSettlement _settlement;


    private void SetUp(bool hasExistingTournament = false)
    {
      _sut = new HostSettlementComparerBaseImpl();
      _mockModState = MockRepository.Create<ModState>();
      _mockTournamentRecords = MockRepository.Create<TournamentRecordDictionary>();
      _mockSettlement = MockRepository.Create<MBSettlement>();
      _settlement = _mockSettlement.Object;

      _sut.ModState = _mockModState.Object;

      _mockModState.SetupGet(modState => modState.TournamentRecords)
        .Returns(_mockTournamentRecords.Object);

      _mockTournamentRecords.Setup<bool>(
        tournamentRecords => tournamentRecords.ContainsSettlement(_mockSettlement.Object))
          .Returns(hasExistingTournament);
    }

    [Test]
    public void HasExistingTournament_SettlementNotInTournamentRecords_ShouldBeFalse()
    {
      SetUp();

      _sut.HasExistingTournament(_settlement).ShouldBe(false);
    }

    [Test]
    public void HasExistingTournament_SettlementExistsInTournamentRecords_ShouldBeTrue()
    {
      SetUp(true);

      _sut.HasExistingTournament(_settlement).ShouldBe(true);
    }

    private class HostSettlementComparerBaseImpl : HostSettlementComparerBase
    {
      public new ModState ModState { set => base.ModState = value; }

      protected override bool MeetsRequirements(MBSettlement wrapper)
      {
        throw new System.NotImplementedException();
      }

      public new bool HasExistingTournament(MBSettlement settlement)
        => base.HasExistingTournament(settlement);
    }
  }
}
