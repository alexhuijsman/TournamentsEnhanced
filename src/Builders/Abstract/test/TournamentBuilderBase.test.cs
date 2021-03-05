using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Builders.Abstract;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Core;
using TournamentsEnhanced.Wrappers.Localization;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class TournamentBuilderBaseTest : TestBase<TournamentBuilderBaseImpl>
  {
    private const bool ValidOptions = true;
    private const bool InvalidOptions = false;
    private const bool HasInitiatingHero = true;
    private const bool NoInitiatingHero = false;
    private const bool HasExistingTournament = true;
    private const bool NoExistingTournament = false;
    private const bool PayorIsPlayer = true;
    private const bool PayorIsNotPlayer = false;
    private const bool ShowSettlementStatNotification = true;
    private const bool HideSettlementStatNotification = false;
    private const string ExpectedSettlementStringId = "111111111111";
    private const string ExpectedHeroStringId = "222222222222";
    private const string SettlementName = "Test Settlement";

    private Mock<MBHero> _mockHero;
    private Mock<MBTown> _mockTown;
    private Mock<MBTextObject> _mockTextObject;
    private Mock<MBTournamentManager> _mockTournamentManager;
    private Mock<MBCampaign> _mockCampaign;
    private Mock<MBClan> _mockClan;
    private Mock<MBSettlement> _mockSettlement;
    private Mock<ModState> _mockModState;
    private Mock<Settings> _mockSettings;
    Mock<MBInformationManagerFacade> _mockInformationManagerFacade;
    private TournamentRecordDictionary _tournamentRecords = new TournamentRecordDictionary();
    private CreateTournamentOptions _options;

    protected void SetUp(
      bool optionsAreValid,
      TournamentType tournamentType = TournamentType.None,
      bool hasExistingTournament = false,
      bool hasInitiatingHero = false,
      bool payorIsPlayer = false,
      bool showSettlementStatNotification = false)
    {
      base.SetUp();

      if (!optionsAreValid)
      {
        var mockOptions = MockRepository.Create<CreateTournamentOptions>();
        mockOptions.SetupGet(o => o.AreValid).Returns(false);
        _options = mockOptions.Object;
      }
      else
      {
        _tournamentRecords.Clear();

        _mockModState = MockRepository.Create<ModState>();
        _mockModState.SetupGet(m => m.IsProduction).Returns(false);
        _mockModState.SetupGet(m => m.TournamentRecords).Returns(_tournamentRecords);

        _mockHero = MockRepository.Create<MBHero>();
        _mockHero.SetupGet(h => h.IsNull).Returns(false);
        _mockHero.SetupGet(h => h.StringId).Returns(ExpectedHeroStringId);
        _mockHero.SetupGet(h => h.IsHumanPlayerCharacter).Returns(payorIsPlayer);
        _mockHero.Setup(h => h.ChangeHeroGold(-Default.TournamentCost));

        _mockClan = MockRepository.Create<MBClan>();
        _mockClan.SetupGet(c => c.Leader).Returns(_mockHero.Object);

        _mockTown = MockRepository.Create<MBTown>();
        _mockTown.SetupGet(t => t.HasTournament).Returns(hasExistingTournament);
        _mockTown.Setup(m => m.ChangeGold(Default.TournamentCost));
        _mockTown.SetupGet(m => m.Loyalty).Returns(0);
        _mockTown.SetupGet(m => m.Security).Returns(0);
        _mockTown.SetupGet(m => m.FoodStocks).Returns(Default.FoodStocksDecrease);
        _mockTown.SetupGet(m => m.MapFaction).Returns(_mockClan.Object);
        _mockTown.SetupSet(m => m.Loyalty = Default.LoyaltyIncrease);
        _mockTown.SetupSet(m => m.Security = Default.SecurityIncrease);
        _mockTown.SetupSet(m => m.FoodStocks = 0);

        _mockTournamentManager = MockRepository.Create<MBTournamentManager>();
        _mockTournamentManager.Setup(m => m.AddTournament(It.IsAny<MBFightTournamentGame>()));

        _mockCampaign = MockRepository.Create<MBCampaign>();
        _mockCampaign.SetupGet(c => c.Current).Returns(_mockCampaign.Object);
        _mockCampaign.SetupGet(c => c.TournamentManager).Returns(_mockTournamentManager.Object);

        _mockTextObject = MockRepository.Create<MBTextObject>();
        _mockTextObject.Setup<string>(o => o.ToString()).Returns(SettlementName);

        _mockSettlement = MockRepository.Create<MBSettlement>();
        _mockSettlement.SetupGet(s => s.IsNull).Returns(false);
        _mockSettlement.SetupGet(s => s.IsTown).Returns(true);
        _mockSettlement.SetupGet(s => s.Town).Returns(_mockTown.Object);
        _mockSettlement.SetupGet(s => s.StringId).Returns(ExpectedSettlementStringId);
        _mockSettlement.SetupGet(s => s.OwnerClan).Returns(_mockClan.Object);
        _mockSettlement.SetupGet(s => s.Name).Returns(_mockTextObject.Object);
        _mockSettlement.SetupGet(s => s.Prosperity).Returns(0);
        _mockSettlement.SetupSet(s => s.Prosperity = Default.ProsperityIncrease);

        _options = new CreateTournamentOptions()
        {
          InitiatingHero = hasInitiatingHero ? _mockHero.Object : MBHero.Null,
          Settlement = _mockSettlement.Object,
          Type = tournamentType
        };

        _mockSettings = MockRepository.Create<Settings>();
        _mockSettings.SetupGet(s => s.TournamentCost).Returns(Default.TournamentCost);
        _mockSettings.SetupGet(s => s.ProsperityIncrease).Returns(Default.ProsperityIncrease);
        _mockSettings.SetupGet(s => s.LoyaltyIncrease).Returns(Default.LoyaltyIncrease);
        _mockSettings.SetupGet(s => s.SecurityIncrease).Returns(Default.SecurityIncrease);
        _mockSettings.SetupGet(s => s.FoodStocksDecrease).Returns(Default.FoodStocksDecrease);
        _mockSettings.SetupGet(s => s.SettlementStatNotification).Returns(showSettlementStatNotification);

        _mockInformationManagerFacade = MockRepository.Create<MBInformationManagerFacade>();
        _mockInformationManagerFacade.Setup(f => f.DisplayAsQuickBanner(It.IsAny<string>()));
        _mockInformationManagerFacade.Setup(f => f.DisplayAsLogEntry(It.IsAny<string>()));

        _sut.ModState = _mockModState.Object;
        _sut.MBCampaign = _mockCampaign.Object;
        _sut.Settings = _mockSettings.Object;
        _sut.MBInformationManagerFacade = _mockInformationManagerFacade.Object;
      }
    }

    [Test]
    public void CreateTournament_OptionsAreNotValid_ShouldThrowArgumentException()
    {
      SetUp(InvalidOptions);

      Should.Throw<ArgumentException>(
        () => _sut.CreateTournament(_options)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_InitialTournament_NoExistingTournament_HasNoInitiatingHero_ShouldReturnExpected()
    {
      SetUp(
        ValidOptions,
        TournamentType.Initial,
        NoExistingTournament,
        NoInitiatingHero
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Never),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Never),
        () => result.HadExistingTournament.ShouldBe(NoExistingTournament),
        () => result.HasPayor.ShouldBe(false),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.Succeeded.ShouldBe(true)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_HasNoInitiatingHero_PayorIsNotPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        ValidOptions,
        TournamentType.Birth,
        NoExistingTournament,
        NoInitiatingHero,
        PayorIsNotPlayer,
        HideSettlementStatNotification
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Never),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Never),
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.HasPayor.ShouldBe(true),
        () => result.Payor.ShouldBe(_mockHero.Object)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_HasNoInitiatingHero_PayorIsNotPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        ValidOptions,
        TournamentType.Birth,
        NoExistingTournament,
        NoInitiatingHero,
        PayorIsNotPlayer,
        ShowSettlementStatNotification
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Never),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Never),
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.HasPayor.ShouldBe(true),
        () => result.Payor.ShouldBe(_mockHero.Object)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_HasNoInitiatingHero_PayorIsPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        ValidOptions,
        TournamentType.Birth,
        NoExistingTournament,
        NoInitiatingHero,
        PayorIsPlayer,
        HideSettlementStatNotification
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Once),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Never),
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.HasPayor.ShouldBe(true),
        () => result.Payor.ShouldBe(_mockHero.Object)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_HasNoInitiatingHero_PayorIsPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        ValidOptions,
        TournamentType.Birth,
        NoExistingTournament,
        NoInitiatingHero,
        PayorIsPlayer,
        ShowSettlementStatNotification
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Once),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Once),
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.HasPayor.ShouldBe(true),
        () => result.Payor.ShouldBe(_mockHero.Object)
      );
    }
  }

  public class TournamentBuilderBaseImpl : TournamentBuilderBase
  {
    public TournamentBuilderBaseImpl() { }

    public new ModState ModState { set => base.ModState = value; }
    public new Settings Settings { set => base.Settings = value; }
    public new MBCampaign MBCampaign { set => base.MBCampaign = value; }
    public new MBInformationManagerFacade MBInformationManagerFacade { set => base.MBInformationManagerFacade = value; }
    public new CreateTournamentResult CreateTournament(CreateTournamentOptions options)
      => base.CreateTournament(options);
  }
}