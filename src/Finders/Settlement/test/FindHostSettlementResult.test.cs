using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;


namespace Test
{
  public class FindHostSettlementResultTest : TestBase
  {
    [Test]
    public void Success_ShouldReturnSuccess()
    {
      var expectedNominees = new List<MBSettlement>();

      var result = FindHostSettlementResult.Success(expectedNominees);

      result.Status.ShouldBe(ResultStatus.Success);
    }

    [Test]
    public void Success_ShouldReturnExpectedCandidates()
    {
      var expectedNominees = new List<MBSettlement>();

      var result = FindHostSettlementResult.Success(expectedNominees);

      result.AllQualifiedCandidates.ShouldBe(expectedNominees);
    }

    [Test]
    public void Success_ShouldReturnExpectedInitiatingHero()
    {
      var expectedInitiatingHero = MockRepository.Create<MBHero>().Object;

      var result = FindHostSettlementResult.Success(null, expectedInitiatingHero);

      result.InitiatingHero.ShouldBe(expectedInitiatingHero);
    }
  }
}