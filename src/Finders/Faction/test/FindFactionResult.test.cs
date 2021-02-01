using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;


namespace Test
{
  public class FindFactionResultTest : TestBase
  {
    [Test]
    public void Success_ShouldReturnSuccess()
    {
      var expectedNominees = new List<MBFaction>();
      var result = FindFactionResult.Success(expectedNominees);

      result.Status.ShouldBe(ResultStatus.Success);
    }

    [Test]
    public void Success_ShouldReturnExpectedCandidates()
    {
      var expectedNominees = new List<MBFaction>();
      var result = FindFactionResult.Success(expectedNominees);

      result.AllQualifiedCandidates.ShouldBe(expectedNominees);
    }

  }
}