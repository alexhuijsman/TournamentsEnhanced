using System;
using System.Collections.Generic;
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
    protected enum OptionsAre { Valid, Invalid }
    protected enum InitiatingHero { IsSpecified, IsNotSpecified }
    protected enum TownHas { ExistingTournament, NoExistingTournament }
    protected enum PayorIs { HumanPlayer, NotHumanPlayer }
    protected enum SettlementStatNotification { Show, Hide }

    protected const string ExpectedSettlementStringId = "111111111111";
    protected const string ExpectedHeroStringId = "222222222222";
    protected const string SettlementName = "Test Settlement";
    protected const int NotableBaseHeroRelation = 11;

    protected Mock<MBHero> _mockHero;
    protected Mock<MBTown> _mockTown;
    protected Mock<MBTextObject> _mockTextObject;
    protected Mock<MBTournamentManager> _mockTournamentManager;
    protected Mock<MBCampaign> _mockCampaign;
    protected Mock<MBClan> _mockClan;
    protected Mock<MBSettlement> _mockSettlement;
    protected Mock<ModState> _mockModState;
    protected Mock<Settings> _mockSettings;
    protected Mock<MBInformationManagerFacade> _mockInformationManagerFacade;
    protected Mock<MBHero> _mockNotableOne;
    protected Mock<MBHero> _mockNotableTwo;
    protected Mock<MBHero> _mockNotableThree;
    protected List<MBHero> _notables = new List<MBHero>();
    protected TournamentRecordDictionary _tournamentRecords = new TournamentRecordDictionary();
    protected CreateTournamentOptions _options;

    protected void SetUp(
      OptionsAre optionsType,
      TournamentType tournamentType = TournamentType.None,
      TownHas townType = TownHas.NoExistingTournament,
      InitiatingHero intiatingHeroType = InitiatingHero.IsNotSpecified,
      PayorIs payorType = PayorIs.NotHumanPlayer,
      SettlementStatNotification settlementStatNotificationType = SettlementStatNotification.Hide)
    {
      base.SetUp();

      if (optionsType == OptionsAre.Invalid)
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
        _mockHero.SetupGet(h => h.IsHumanPlayerCharacter).Returns(payorType == PayorIs.HumanPlayer);
        _mockHero.Setup(h => h.ChangeHeroGold(-Default.TournamentCost));
        _mockHero.SetupGet(h => h.MainHero).Returns(_mockHero.Object);

        _mockClan = MockRepository.Create<MBClan>();
        _mockClan.SetupGet(c => c.Leader).Returns(_mockHero.Object);

        _mockTown = MockRepository.Create<MBTown>();
        _mockTown.SetupGet(t => t.HasTournament).Returns(townType == TownHas.ExistingTournament);
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

        _mockNotableOne = CreateMockNotable();
        _mockNotableTwo = CreateMockNotable();
        _mockNotableThree = CreateMockNotable();

        _notables.Clear();
        _notables.Add(_mockNotableOne.Object);
        _notables.Add(_mockNotableTwo.Object);
        _notables.Add(_mockNotableThree.Object);
        _notables.Add(_mockHero.Object);

        _mockSettlement = MockRepository.Create<MBSettlement>();
        _mockSettlement.SetupGet(s => s.IsNull).Returns(false);
        _mockSettlement.SetupGet(s => s.IsTown).Returns(true);
        _mockSettlement.SetupGet(s => s.Town).Returns(_mockTown.Object);
        _mockSettlement.SetupGet(s => s.StringId).Returns(ExpectedSettlementStringId);
        _mockSettlement.SetupGet(s => s.OwnerClan).Returns(_mockClan.Object);
        _mockSettlement.SetupGet(s => s.Name).Returns(_mockTextObject.Object);
        _mockSettlement.SetupGet(s => s.Prosperity).Returns(0);
        _mockSettlement.SetupSet(s => s.Prosperity = Default.ProsperityIncrease);
        _mockSettlement.SetupGet(s => s.Notables).Returns(_notables);

        _options = new CreateTournamentOptions()
        {
          InitiatingHero =
            intiatingHeroType == InitiatingHero.IsSpecified
              ? _mockHero.Object
              : MBHero.Null,
          Settlement = _mockSettlement.Object,
          Type = tournamentType
        };

        _mockSettings = MockRepository.Create<Settings>();
        _mockSettings.SetupGet(s => s.TournamentCost).Returns(Default.TournamentCost);
        _mockSettings.SetupGet(s => s.ProsperityIncrease).Returns(Default.ProsperityIncrease);
        _mockSettings.SetupGet(s => s.LoyaltyIncrease).Returns(Default.LoyaltyIncrease);
        _mockSettings.SetupGet(s => s.SecurityIncrease).Returns(Default.SecurityIncrease);
        _mockSettings.SetupGet(s => s.FoodStocksDecrease).Returns(Default.FoodStocksDecrease);
        _mockSettings.SetupGet(s => s.SettlementStatNotification).Returns(settlementStatNotificationType == SettlementStatNotification.Show);
        _mockSettings.SetupGet(s => s.NoblesRelationIncrease).Returns(Default.NoblesRelationIncrease);

        _mockInformationManagerFacade = MockRepository.Create<MBInformationManagerFacade>();
        _mockInformationManagerFacade.Setup(f => f.DisplayAsQuickBanner(It.IsAny<string>()));
        _mockInformationManagerFacade.Setup(f => f.DisplayAsLogEntry(It.IsAny<string>()));

        _sut.ModState = _mockModState.Object;
        _sut.MBCampaign = _mockCampaign.Object;
        _sut.Settings = _mockSettings.Object;
        _sut.MBInformationManagerFacade = _mockInformationManagerFacade.Object;
        _sut.MBHero = _mockHero.Object;
      }
    }

    private Mock<MBHero> CreateMockNotable()
    {
      var mockNotable = MockRepository.Create<MBHero>();
      mockNotable.Setup(n => n.GetBaseHeroRelation(_mockHero.Object)).Returns(NotableBaseHeroRelation);
      mockNotable.Setup(n => n.SetPersonalRelation(_mockHero.Object, NotableBaseHeroRelation + Default.NoblesRelationIncrease));

      return mockNotable;
    }

    [Test]
    public void CreateTournament_OptionsAreNotValid_ShouldThrowArgumentException()
    {
      SetUp(OptionsAre.Invalid);

      Should.Throw<ArgumentException>(
        () => _sut.CreateTournament(_options)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_InitialTournament_NoExistingTournament_NoInitiatingHero_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Initial,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Never),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Never),
        () => result.HadExistingTournament.ShouldBe(false),
        () => result.HasPayor.ShouldBe(false),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.Succeeded.ShouldBe(true)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_BirthTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_BirthTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_BirthTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_BirthTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_BirthTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_BirthTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_BirthTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_BirthTournament_HasExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Birth,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_HighbornTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_HighbornTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_HighbornTournament_HasExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Highborn,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_InvitationTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_InvitationTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_InvitationTournament_HasExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Invitation,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_PeaceTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_PeaceTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PeaceTournament_HasExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Peace,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Exactly(2)),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Never),
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.HasPayor.ShouldBe(true),
        () => result.Payor.ShouldBe(_mockHero.Object)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Exactly(2)),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Once),
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.HasPayor.ShouldBe(true),
        () => result.Payor.ShouldBe(_mockHero.Object)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Exactly(2)),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Never),
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.HasPayor.ShouldBe(true),
        () => result.Payor.ShouldBe(_mockHero.Object)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
      );

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Exactly(2)),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Once),
        () => result.Succeeded.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.HasPayor.ShouldBe(true),
        () => result.Payor.ShouldBe(_mockHero.Object)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_PlayerInitiatedTournament_HasExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.PlayerInitiated,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_ProsperityTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_ProsperityTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_ProsperityTournament_HasExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Prosperity,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_NoExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_NoExistingTournament_NoInitiatingHero_PayorIsPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.NoExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_WeddingTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_NoExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_NoExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.NoExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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

    [Test]
    public void CreateTournament_OptionsAreValid_WeddingTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_HasExistingTournament_NoInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_HasExistingTournament_NoInitiatingHero_PayorIsHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.ExistingTournament,
        InitiatingHero.IsNotSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Hide
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_HasExistingTournament_HasInitiatingHero_PayorIsNotHumanPlayer_ShowSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.NotHumanPlayer,
        SettlementStatNotification.Show
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
    public void CreateTournament_OptionsAreValid_WeddingTournament_HasExistingTournament_HasInitiatingHero_PayorIsHumanPlayer_HideSettlementStatNotification_ShouldReturnExpected()
    {
      SetUp(
        OptionsAre.Valid,
        TournamentType.Wedding,
        TownHas.ExistingTournament,
        InitiatingHero.IsSpecified,
        PayorIs.HumanPlayer,
        SettlementStatNotification.Hide
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
  }

  public class TournamentBuilderBaseImpl : TournamentBuilderBase
  {
    public TournamentBuilderBaseImpl() { }

    public new ModState ModState { set => base.ModState = value; }
    public new Settings Settings { set => base.Settings = value; }
    public new MBCampaign MBCampaign { set => base.MBCampaign = value; }
    public new MBInformationManagerFacade MBInformationManagerFacade { set => base.MBInformationManagerFacade = value; }
    public new MBHero MBHero { set => base.MBHero = value; }
    public new CreateTournamentResult CreateTournament(CreateTournamentOptions options)
      => base.CreateTournament(options);
  }
}