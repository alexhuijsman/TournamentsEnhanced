using System.Collections.Generic;
using System.Xml;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Comparers.Clan;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace Tests
{
  public class FindClanResultTests
  {
    [Test]
    public void Success_ShouldReturnSuccess()
    {
      var expectedNominees = new List<MBClan>();
      var result = FindClanResult.Success(expectedNominees);

      result.Status.ShouldBe(ResultStatus.Success);
    }

    [Test]
    public void Success_ShouldReturnExpectedCandidates()
    {
      var expectedNominees = new List<MBClan>();
      var result = FindClanResult.Success(expectedNominees);

      result.AllQualifiedCandidates.ShouldBe(expectedNominees);
    }
  }
}