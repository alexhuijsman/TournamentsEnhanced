using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class FactionFinderTest : TestBase<FactionFinderImpl>
  {
    protected Mock<MBFaction> _mockFaction;
    protected Mock<MBClan> _mockClan;
    protected Mock<MBHero> _mockLeader;
    protected Mock<MBSettlement> _mockSettlement;
    protected List<MBSettlement> _factionSettlements = new List<MBSettlement>();

    protected override void SetUp()
    {
      base.SetUp();

      _mockLeader = MockRepository.Create<MBHero>();
      _mockLeader.SetupGet(h => h.IsNull).Returns(false);
      _mockLeader.SetupGet(h => h.IsActive).Returns(true);
      _mockLeader.SetupGet(h => h.Gold).Returns(Default.TournamentCost);

      _mockClan = MockRepository.Create<MBClan>();
      _mockClan.SetupGet(c => c.Leader).Returns(_mockLeader.Object);

      _mockSettlement = MockRepository.Create<MBSettlement>();
      _mockSettlement.SetupGet(s => s.OwnerClan).Returns(_mockClan.Object);
      _mockSettlement.SetupGet(s => s.IsTown).Returns(true);

      var mockTownlessSettlement = MockRepository.Create<MBSettlement>();
      mockTownlessSettlement.SetupGet(s => s.IsTown).Returns(false);

      _factionSettlements.Clear();
      _factionSettlements.Add(mockTownlessSettlement.Object);
      _factionSettlements.Add(_mockSettlement.Object);

      _mockFaction = MockRepository.Create<MBFaction>();
      _mockFaction.SetupGet(faction => faction.IsNull).Returns(false);
      _mockFaction.SetupGet(faction => faction.Settlements).Returns(_factionSettlements);

    }

    [Test]
    public void Instance_ShouldBeSingleton()
    {
      FactionFinder.Instance.ShouldBe(FactionFinder.Instance);
    }

    [Test]
    public void FindFactionThatMeetsBasicHostRequirements_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindFactionThatMeetBasicHostRequirements(_mockFaction.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockFaction.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }
  }

  public class FactionFinderImpl : FactionFinder
  {
    public FactionFinderImpl() { }
  }
}