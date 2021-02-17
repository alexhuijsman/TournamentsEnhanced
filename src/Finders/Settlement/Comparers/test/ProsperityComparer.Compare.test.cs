using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace Test
{
  public partial class ProsperityComparerTest
    : ExistingTournamentComparerTestBase<ProsperityComparerImpl>
  {
    protected const float XMeetsProsperityRequirement = Constants.Settings.Default.MinProsperityRequirement;
    protected const float XExceedsProsperityRequirement = XMeetsProsperityRequirement + 1;
    protected const float XFailsProsperityRequirement = XMeetsProsperityRequirement - 1;
    protected const float YMeetsProsperityRequirement = XMeetsProsperityRequirement;
    protected const float YExceedsProsperityRequirement = XExceedsProsperityRequirement;
    protected const float YFailsProsperityRequirement = XFailsProsperityRequirement;
    protected const bool XMeetsBaseRequirements = MeetsBaseRequirements;
    protected const bool YMeetsBaseRequirements = MeetsBaseRequirements;
    protected const bool XFailsBaseRequirements = FailsBaseRequirements;
    protected const bool YFailsBaseRequirements = FailsBaseRequirements;

    protected Mock<MBSettlement> _mockSettlementX;
    protected Mock<MBSettlement> _mockSettlementY;
    protected MBSettlement _settlementX;
    protected MBSettlement _settlementY;

    protected virtual void SetUpCompare(
      bool requiresMinProsperity,
      bool xMeetsBaseRequirements,
      bool yMeetsBaseRequirements,
      float xProsperity,
      float yProsperity
      )
    {
      SetUp(xMeetsBaseRequirements, requiresMinProsperity);

      if (xMeetsBaseRequirements)
      {
        _mockSettlementX = _mockSettlement;
        _mockSettlementX.SetupGet(settlement => settlement.Prosperity).Returns(xProsperity);
        _settlementX = _mockSettlementX.Object;
      }
      else
      {
        _mockSettlementX = null;
        _settlementX = MBSettlement.Null;
      }

      if (yMeetsBaseRequirements)
      {
        _mockSettlementY = CreateMockSettlement(true, Constants.Settings.Default.FoodStocksDecrease).mockSettlement;
        _mockSettlementY.SetupGet(settlement => settlement.Prosperity).Returns(yProsperity);
        _settlementY = _mockSettlementY.Object;
      }
      else
      {
        _mockSettlementY = null;
        _settlementY = MBSettlement.Null;
      }

      _mockSettings = MockRepository.Create<Settings>();
      _mockSettings.SetupGet(settings => settings.MinProsperityRequirement)
        .Returns(requiresMinProsperity ? Constants.Settings.Default.MinProsperityRequirement : 0);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XAndYFailBaseRequirements_XAndYFailProsperityRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XFailsBaseRequirements,
        YFailsBaseRequirements,
        XFailsProsperityRequirement,
        YFailsProsperityRequirement);

      var x = _sut.Compare(_settlementX, _settlementY);
      x.ShouldBe(Constants.Comparer.BothEqualRank);
    }
  }
}