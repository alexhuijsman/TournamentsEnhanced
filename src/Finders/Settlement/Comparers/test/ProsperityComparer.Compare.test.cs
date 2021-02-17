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
    protected const bool XIsNull = FailsBaseRequirements;
    protected const bool YIsNull = FailsBaseRequirements;

    protected Mock<MBSettlement> _mockSettlementX;
    protected Mock<MBSettlement> _mockSettlementY;
    protected MBSettlement _settlementX;
    protected MBSettlement _settlementY;

    protected virtual void SetUpCompare(
      bool requiresMinProsperity,
      float xProsperity,
      float yProsperity,
      bool xMeetsBaseRequirements,
      bool yMeetsBaseRequirements
      )
    {
      SetUp(xMeetsBaseRequirements, requiresMinProsperity);

      _mockSettlementX = _mockSettlement;
      _mockSettlementX.SetupGet(settlement => settlement.IsNull)
        .Returns(!xMeetsBaseRequirements);

      if (xMeetsBaseRequirements)
      {
        _mockSettlementX.SetupGet(settlement => settlement.Prosperity).Returns(xProsperity);
      }

      _mockSettlementY = CreateMockSettlement(true, ExactlyEnoughFood).mockSettlement;
      SetUpContainsSettlement(_mockSettlementY, NoExistingTournament);

      _mockSettlementY.SetupGet(settlement => settlement.IsNull).Returns(!yMeetsBaseRequirements);

      if (yMeetsBaseRequirements)
      {
        _mockSettlementY.SetupGet(settlement => settlement.Prosperity).Returns(yProsperity);
      }

      _settlementX = _mockSettlementX.Object;
      _settlementY = _mockSettlementY.Object;

      _mockSettings.SetupGet(settings => settings.MinProsperityRequirement)
        .Returns(requiresMinProsperity ? Constants.Settings.Default.MinProsperityRequirement : 0);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XAndYFailProsperityRequirements_XAndYFailBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XFailsProsperityRequirement,
        YFailsProsperityRequirement,
        XIsNull,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XAndYFailProsperityRequirements_XFailsBaseRequirements_YShouldOutrankX()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XFailsProsperityRequirement,
        YFailsProsperityRequirement,
        XIsNull,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XAndYFailProsperityRequirements_XMeetsBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XFailsProsperityRequirement,
        YFailsProsperityRequirement,
        XMeetsBaseRequirements,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XAndYFailProsperityRequirements_XAndYMeetBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XFailsProsperityRequirement,
        YFailsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XFailsProsperityRequirements_XAndYFailBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XFailsProsperityRequirement,
        YMeetsProsperityRequirement,
        XIsNull,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XFailsProsperityRequirements_XFailBaseRequirements_YShouldOutrankX()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XFailsProsperityRequirement,
        YMeetsProsperityRequirement,
        XIsNull,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XFailsProsperityRequirements_YFailsBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XFailsProsperityRequirement,
        YMeetsProsperityRequirement,
        XMeetsBaseRequirements,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XFailsProsperityRequirements_XAndYMeetBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XFailsProsperityRequirement,
        YMeetsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_YFailsProsperityRequirements_XAndYFailBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XMeetsProsperityRequirement,
        YFailsProsperityRequirement,
        XIsNull,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_YFailsProsperityRequirements_XFailsBaseRequirements_YShouldOutrankX()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XMeetsProsperityRequirement,
        YFailsProsperityRequirement,
        XIsNull,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_YFailsProsperityRequirements_YFailsBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XMeetsProsperityRequirement,
        YFailsProsperityRequirement,
        XMeetsBaseRequirements,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_YFailsProsperityRequirements_XAndYMeetBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XMeetsProsperityRequirement,
        YFailsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XAndYMeetProsperityRequirements_XAndYFailBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XMeetsProsperityRequirement,
        YMeetsProsperityRequirement,
        XIsNull,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XAndYMeetProsperityRequirements_XFailsBaseRequirements_YShouldOutrankX()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XMeetsProsperityRequirement,
        YMeetsProsperityRequirement,
        XIsNull,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XAndYMeetProsperityRequirements_YFailsBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XMeetsProsperityRequirement,
        YMeetsProsperityRequirement,
        XMeetsBaseRequirements,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_DoesNotRequireMinProsperity_XAndYMeetProsperityRequirements_XAndYMeetBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        DoesNotRequireMinProsperity,
        XMeetsProsperityRequirement,
        YMeetsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XAndYFailProsperityRequirements_XAndYFailBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XFailsProsperityRequirement,
        YFailsProsperityRequirement,
        XIsNull,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XAndYFailProsperityRequirements_XFailsBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XFailsProsperityRequirement,
        YFailsProsperityRequirement,
        XIsNull,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XAndYFailProsperityRequirements_XMeetsBaseRequirements_YShouldOutrankX()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XFailsProsperityRequirement,
        YFailsProsperityRequirement,
        XMeetsBaseRequirements,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XAndYFailProsperityRequirements_XAndYMeetBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XFailsProsperityRequirement,
        YFailsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XFailsProsperityRequirements_XAndYFailBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XFailsProsperityRequirement,
        YMeetsProsperityRequirement,
        XIsNull,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XFailsProsperityRequirements_XFailBaseRequirements_YShouldOutrankX()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XFailsProsperityRequirement,
        YMeetsProsperityRequirement,
        XIsNull,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XFailsProsperityRequirements_YFailsBaseRequirements_YShouldOutrankX()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XFailsProsperityRequirement,
        YMeetsProsperityRequirement,
        XMeetsBaseRequirements,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XFailsProsperityRequirements_XAndYMeetBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XFailsProsperityRequirement,
        YMeetsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_RequiresMinProsperity_YFailsProsperityRequirements_XAndYFailBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XMeetsProsperityRequirement,
        YFailsProsperityRequirement,
        XIsNull,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_RequiresMinProsperity_YFailsProsperityRequirements_XFailsBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XMeetsProsperityRequirement,
        YFailsProsperityRequirement,
        XIsNull,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_RequiresMinProsperity_YFailsProsperityRequirements_YFailsBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XMeetsProsperityRequirement,
        YFailsProsperityRequirement,
        XMeetsBaseRequirements,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_RequiresMinProsperity_YFailsProsperityRequirements_XAndYMeetBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XMeetsProsperityRequirement,
        YFailsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XAndYMeetProsperityRequirements_XAndYFailBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XMeetsProsperityRequirement,
        YMeetsProsperityRequirement,
        XIsNull,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XAndYMeetProsperityRequirements_XFailsBaseRequirements_YShouldOutrankX()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XMeetsProsperityRequirement,
        YMeetsProsperityRequirement,
        XIsNull,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XAndYMeetProsperityRequirements_YFailsBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XMeetsProsperityRequirement,
        YMeetsProsperityRequirement,
        XMeetsBaseRequirements,
        YIsNull);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XAndYMeetProsperityRequirements_XAndYMeetBaseRequirements_ShouldBeEqualRank()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XMeetsProsperityRequirement,
        YMeetsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.BothEqualRank);
    }

    [Test]
    public void Compare_RequiresMinProsperity_YExceedsProsperityRequirements_XAndYMeetBaseRequirements_YShouldOutrankX()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XMeetsProsperityRequirement,
        YExceedsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.YOutranksX);
    }

    [Test]
    public void Compare_RequiresMinProsperity_XExceedsProsperityRequirements_XAndYMeetBaseRequirements_XShouldOutrankY()
    {
      SetUpCompare(
        RequiresMinProsperity,
        XExceedsProsperityRequirement,
        YMeetsProsperityRequirement,
        XMeetsBaseRequirements,
        YMeetsBaseRequirements);

      _sut.Compare(_settlementX, _settlementY).ShouldBe(Constants.Comparer.XOutranksY);
    }

  }
}