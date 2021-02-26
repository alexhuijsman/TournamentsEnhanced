using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class SettlementFinderTest : TestBase<SettlementFinderImpl>
  {
    protected Mock<IMBFaction> _mockFaction;
    protected Mock<IMBFaction> _mockOtherFaction;
    protected Mock<MBClan> _mockClan;
    protected Mock<MBClan> _mockOtherClan;
    protected Mock<MBTown> _mockTown;
    protected Mock<MBTown> _mockOtherTown;
    protected Mock<MBSettlement> _mockSettlement;
    protected List<MBSettlement> _settlements = new List<MBSettlement>();
    protected List<MBSettlement> _otherSettlements = new List<MBSettlement>();
    protected Mock<MBSettlement> _mockOtherSettlement;
    protected Mock<MBHero> _mockHero;
    protected Mock<MBHero> _mockOtherHero;

    protected override void SetUp()
    {
      base.SetUp();

      _mockFaction = MockRepository.Create<IMBFaction>();
      _mockFaction.SetupGet(f => f.IsKingdomFaction).Returns(true);
      _mockFaction.SetupGet(f => f.IsClan).Returns(false);
      _mockFaction.SetupGet(c => c.Settlements).Returns(_settlements);

      _mockOtherFaction = MockRepository.Create<IMBFaction>();
      _mockOtherFaction.SetupGet(f => f.IsKingdomFaction).Returns(true);
      _mockOtherFaction.SetupGet(f => f.IsClan).Returns(false);
      _mockOtherFaction.SetupGet(c => c.Settlements).Returns(_otherSettlements);

      _mockClan = MockRepository.Create<MBClan>();
      _mockClan.SetupGet(c => c.Settlements).Returns(_settlements);

      _mockOtherClan = MockRepository.Create<MBClan>();
      _mockOtherClan.SetupGet(c => c.Settlements).Returns(_otherSettlements);

      _mockTown = MockRepository.Create<MBTown>();
      _mockTown.SetupGet(t => t.FoodStocks).Returns(Default.FoodStocksDecrease);

      _mockOtherTown = MockRepository.Create<MBTown>();
      _mockOtherTown.SetupGet(t => t.FoodStocks).Returns(Default.FoodStocksDecrease);

      _mockSettlement = MockRepository.Create<MBSettlement>();
      _mockSettlement.SetupGet(s => s.IsTown).Returns(true);
      _mockSettlement.SetupGet(s => s.IsNull).Returns(false);
      _mockSettlement.SetupGet(s => s.Town).Returns(_mockTown.Object);
      _mockSettlement.SetupGet(s => s.StringId).Returns("settlement");

      _mockOtherSettlement = MockRepository.Create<MBSettlement>();
      _mockOtherSettlement.SetupGet(s => s.IsTown).Returns(true);
      _mockOtherSettlement.SetupGet(s => s.IsNull).Returns(false);
      _mockOtherSettlement.SetupGet(s => s.Town).Returns(_mockOtherTown.Object);
      _mockOtherSettlement.SetupGet(s => s.StringId).Returns("otherSettlement");

      _settlements.Clear();
      _settlements.Add(_mockSettlement.Object);

      _otherSettlements.Clear();
      _otherSettlements.Add(_mockOtherSettlement.Object);

      _mockHero = MockRepository.Create<MBHero>();
      _mockHero.SetupGet(h => h.IsNull).Returns(false);
      _mockHero.SetupGet(h => h.IsActive).Returns(true);
      _mockHero.SetupGet(h => h.Gold).Returns(Default.TournamentCost);
      _mockHero.SetupGet(h => h.IsFactionLeader).Returns(false);
      _mockHero.SetupGet(h => h.Clan).Returns(_mockClan.Object);
      _mockHero.SetupGet(h => h.MapFaction).Returns(_mockFaction.Object);

      _mockOtherHero = MockRepository.Create<MBHero>();
      _mockOtherHero.SetupGet(h => h.IsNull).Returns(false);
      _mockOtherHero.SetupGet(h => h.IsActive).Returns(true);
      _mockOtherHero.SetupGet(h => h.Gold).Returns(Default.TournamentCost);
      _mockOtherHero.SetupGet(h => h.IsFactionLeader).Returns(true);
      _mockOtherHero.SetupGet(h => h.Clan).Returns(_mockOtherClan.Object);
      _mockOtherHero.SetupGet(h => h.MapFaction).Returns(_mockOtherFaction.Object);

      _mockHero.SetupGet(h => h.Spouse).Returns(_mockOtherHero.Object);
      _mockOtherHero.SetupGet(h => h.Spouse).Returns(_mockHero.Object);
    }

    [Test]
    public void Instance_ShouldBeSingleton()
    {
      SettlementFinder.Instance.ShouldBe(SettlementFinder.Instance);
    }

    [Test]
    public void FindForBirthTournament_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindForBirthTournament(_mockHero.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockOtherSettlement.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }

    [Test]
    public void FindForHighbornTournament_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindForHighbornTournament();

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockSettlement.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }

    [Test]
    public void FindForInvitationTournament_ShouldReturnExpected()
    {
      Assert.Fail();

      SetUp();

      var result = _sut.FindForInvitationTournament();

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockSettlement.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }

    [Test]
    public void FindForPeaceTournament_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindForPeaceTournament(_mockFaction.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockSettlement.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }

    [Test]
    public void FindForProsperityTournament_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindForProsperityTournament();

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockSettlement.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }

    [Test]
    public void FindForWeddingTournament_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindForWeddingTournament(
        _mockHero.Object,
        _mockOtherHero.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockSettlement.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }
  }

  public class SettlementFinderImpl : SettlementFinder
  {
    public SettlementFinderImpl() { }
  }
}