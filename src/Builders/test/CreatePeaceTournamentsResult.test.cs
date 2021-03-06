using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Localization;

namespace Test
{
  public class CreatePeaceTournamentsResultTest : TestBase
  {
    protected Mock<CreateTournamentResult> _mockResultA;
    protected Mock<CreateTournamentResult> _mockResultB;
    protected Mock<MBSettlement> _mockSettlementA;
    protected Mock<MBSettlement> _mockSettlementB;
    protected Mock<MBTextObject> _mockTextObjectA;
    protected Mock<MBTextObject> _mockTextObjectB;
    protected const string SettlementNameA = "Test Settlement A";
    protected const string SettlementNameB = "Test Settlement B";

    protected enum ResultA
    {
      Failed,
      Succeeded
    }

    protected enum ResultB
    {
      Failed,
      Succeeded
    }

    protected void SetUp(ResultA resultA, ResultB resultB)
    {
      _mockTextObjectA = MockRepository.Create<MBTextObject>();
      _mockTextObjectA.Setup(a => a.ToString()).Returns(SettlementNameA);

      _mockSettlementA = MockRepository.Create<MBSettlement>();
      _mockSettlementA.SetupGet(a => a.Name).Returns(_mockTextObjectA.Object);

      _mockResultA = MockRepository.Create<CreateTournamentResult>();
      _mockResultA.SetupGet(a => a.Failed).Returns(resultA == ResultA.Failed);
      _mockResultA.SetupGet(a => a.Succeeded).Returns(resultA == ResultA.Succeeded);
      _mockResultA.SetupGet(a => a.HostSettlement).Returns(resultA == ResultA.Succeeded ? _mockSettlementA.Object : MBSettlement.Null);

      _mockTextObjectB = MockRepository.Create<MBTextObject>();
      _mockTextObjectB.Setup(b => b.ToString()).Returns(SettlementNameB);

      _mockSettlementB = MockRepository.Create<MBSettlement>();
      _mockSettlementB.SetupGet(b => b.Name).Returns(_mockTextObjectB.Object);

      _mockResultB = MockRepository.Create<CreateTournamentResult>();
      _mockResultB.SetupGet(b => b.Failed).Returns(resultB == ResultB.Failed);
      _mockResultB.SetupGet(b => b.Succeeded).Returns(resultB == ResultB.Succeeded);
      _mockResultB.SetupGet(b => b.HostSettlement).Returns(resultB == ResultB.Succeeded ? _mockSettlementB.Object : MBSettlement.Null);

    }

    [Test]
    public void Success_ResultAFailed_ResultBSucceeded_ShouldReturnExpected()
    {
      SetUp(ResultA.Failed, ResultB.Succeeded);

      var result = CreatePeaceTournamentsResult.Success(_mockResultA.Object, _mockResultB.Object);

      result.ShouldSatisfyAllConditions(
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlementNames.ShouldBe(SettlementNameB)
      );
    }

    [Test]
    public void Success_ResultASucceeded_ResultBFailed_ShouldReturnExpected()
    {
      SetUp(ResultA.Succeeded, ResultB.Failed);

      var result = CreatePeaceTournamentsResult.Success(_mockResultA.Object, _mockResultB.Object);

      result.ShouldSatisfyAllConditions(
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlementNames.ShouldBe(SettlementNameA)
      );
    }

    [Test]
    public void Success_ResultASucceeded_ResultBSucceeded_ShouldReturnExpected()
    {
      SetUp(ResultA.Succeeded, ResultB.Succeeded);

      var result = CreatePeaceTournamentsResult.Success(_mockResultA.Object, _mockResultB.Object);

      result.ShouldSatisfyAllConditions(
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlementNames.ShouldBe($"{SettlementNameA} and {SettlementNameB}")
      );
    }

  }
}