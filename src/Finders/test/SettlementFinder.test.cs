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
    protected Mock<MBClan> _mockClan;
    protected Mock<MBClan> _mockOtherClan;
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

      _mockClan = MockRepository.Create<MBClan>();
      _mockClan.SetupGet(c => c.Settlements).Returns(_settlements);

      _mockOtherClan = MockRepository.Create<MBClan>();
      _mockOtherClan.SetupGet(c => c.Settlements).Returns(_otherSettlements);

      _mockSettlement = MockRepository.Create<MBSettlement>();
      _mockSettlement.SetupGet(s => s.IsTown).Returns(true);
      _mockSettlement.SetupGet(s => s.IsNull).Returns(false);

      _mockOtherSettlement = MockRepository.Create<MBSettlement>();
      _mockOtherSettlement.SetupGet(s => s.IsTown).Returns(true);
      _mockOtherSettlement.SetupGet(s => s.IsNull).Returns(false);

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

      _mockOtherHero = MockRepository.Create<MBHero>();
      _mockOtherHero.SetupGet(h => h.IsNull).Returns(false);
      _mockOtherHero.SetupGet(h => h.IsActive).Returns(true);
      _mockOtherHero.SetupGet(h => h.Gold).Returns(Default.TournamentCost);
      _mockOtherHero.SetupGet(h => h.IsFactionLeader).Returns(true);
      _mockOtherHero.SetupGet(h => h.Clan).Returns(_mockOtherClan.Object);

      _mockHero.SetupGet(h => h.Spouse).Returns(_mockOtherHero.Object);

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
            () => result.Nominee.ShouldBe(_mockSettlement.Object),
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