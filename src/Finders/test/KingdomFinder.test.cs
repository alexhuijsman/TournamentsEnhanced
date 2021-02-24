using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class KingdomFinderTest : TestBase<KingdomFinderImpl>
  {
    protected Mock<MBKingdom> _mockKingdom;
    protected Mock<MBClan> _mockClan;
    protected Mock<MBHero> _mockLeader;
    protected Mock<MBSettlement> _mockSettlement;
    protected List<MBSettlement> _KingdomSettlements = new List<MBSettlement>();

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

      _KingdomSettlements.Clear();
      _KingdomSettlements.Add(mockTownlessSettlement.Object);
      _KingdomSettlements.Add(_mockSettlement.Object);

      _mockKingdom = MockRepository.Create<MBKingdom>();
      _mockKingdom.SetupGet(Kingdom => Kingdom.IsNull).Returns(false);
      _mockKingdom.SetupGet(Kingdom => Kingdom.Settlements).Returns(_KingdomSettlements);

    }

    [Test]
    public void Instance_ShouldBeSingleton()
    {
      KingdomFinder.Instance.ShouldBe(KingdomFinder.Instance);
    }

    [Test]
    public void FindKingdomThatMeetsBasicHostRequirements_ShouldReturnExpected()
    {
      SetUp();

      var result = _sut.FindKingdomThatMeetBasicHostRequirements(_mockKingdom.Object);

      result.ShouldSatisfyAllConditions
        (
            () => result.Nominee.ShouldBe(_mockKingdom.Object),
            () => result.HasRunnerUp.ShouldBe(false)
        );
    }
  }

  public class KingdomFinderImpl : KingdomFinder
  {
    public KingdomFinderImpl() { }
  }
}