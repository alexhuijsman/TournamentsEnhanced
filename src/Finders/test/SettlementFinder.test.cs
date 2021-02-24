using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace Test
{
  public class SettlementFinderTest : TestBase<SettlementFinderImpl>
  {
    protected Mock<MBSettlement> _mockSettlement;

    protected override void SetUp()
    {
      base.SetUp();

      _mockSettlement = MockRepository.Create<MBSettlement>();
      _mockSettlement.SetupGet(s => s.IsTown).Returns(true);
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