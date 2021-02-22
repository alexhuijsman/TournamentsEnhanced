using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;


namespace Test
{
  public class ClanFinderTest : TestBase<ClanFinderImpl>
  {
    protected Mock<MBClan> _mockClan;
    protected Mock<MBSettlement> _mockSettlement;
    protected List<MBSettlement> _clanSettlements = new List<MBSettlement>();

    protected override void SetUp()
    {
      base.SetUp();

      _mockSettlement = MockRepository.Create<MBSettlement>();
      _mockSettlement.SetupGet(s => s.IsTown).Returns(true);

      var mockTownlessSettlement = MockRepository.Create<MBSettlement>();
      mockTownlessSettlement.SetupGet(s => s.IsTown).Returns(false);

      _clanSettlements.Clear();
      _clanSettlements.Add(mockTownlessSettlement.Object);
      _clanSettlements.Add(_mockSettlement.Object);

      _mockClan = MockRepository.Create<MBClan>();
      _mockClan.SetupGet(clan => clan.IsNull).Returns(false);
      _mockClan.SetupGet(clan => clan.Settlements).Returns(_clanSettlements);

    }

    [Test]
    public void Instance_ShouldBeSingleton()
    {
      ClanFinder.Instance.ShouldBe(ClanFinder.Instance);
    }

    [Test]
    public void FindClanThatMeetsBasicHostRequirements_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindClanThatMeetsBasicHostRequirements(_mockClan.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockClan.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }
  }

  public class ClanFinderImpl : ClanFinder
  {
    public ClanFinderImpl() { }
  }
}