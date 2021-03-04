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
    private const bool HeroIsHumanPlayerCharacter = true;
    private const bool HeroIsNotHumanPlayerCharacter = false;
    private const string ExpectedSettlementStringId = "111111111111";
    private const string ExpectedHeroStringId = "222222222222";

    private Mock<MBHero> _mockHero;
    private Mock<MBTown> _mockTown;
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
      bool hasExistingTournament = false,
      bool hasInitiatingHero = false,
      TournamentType tournamentType = TournamentType.None,
      bool heroIsHumanPlayerCharacter = false)
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
        _mockHero.SetupGet(h => h.IsHumanPlayerCharacter).Returns(heroIsHumanPlayerCharacter);
        _mockHero.Setup(h => h.ChangeHeroGold(Default.TournamentCost));

        _mockTown = MockRepository.Create<MBTown>();
        _mockTown.SetupGet(t => t.HasTournament).Returns(hasExistingTournament);
        _mockTown.Setup(m => m.ChangeGold(It.IsAny<int>()));

        _mockTournamentManager = MockRepository.Create<MBTournamentManager>();
        _mockTournamentManager.Setup(m => m.AddTournament(It.IsAny<MBFightTournamentGame>()));

        _mockCampaign = MockRepository.Create<MBCampaign>();
        _mockCampaign.SetupGet(c => c.Current).Returns(_mockCampaign.Object);
        _mockCampaign.SetupGet(c => c.TournamentManager).Returns(_mockTournamentManager.Object);

        _mockClan = MockRepository.Create<MBClan>();
        _mockClan.SetupGet(c => c.Leader).Returns(_mockHero.Object);

        _mockSettlement = MockRepository.Create<MBSettlement>();
        _mockSettlement.SetupGet(s => s.IsNull).Returns(false);
        _mockSettlement.SetupGet(s => s.IsTown).Returns(true);
        _mockSettlement.SetupGet(s => s.Town).Returns(_mockTown.Object);
        _mockSettlement.SetupGet(s => s.StringId).Returns(ExpectedSettlementStringId);
        _mockSettlement.SetupGet(s => s.OwnerClan).Returns(_mockClan.Object);

        _options = new CreateTournamentOptions()
        {
          InitiatingHero = hasInitiatingHero ? _mockHero.Object : MBHero.Null,
          Settlement = _mockSettlement.Object,
          Type = tournamentType
        };

        _mockSettings = MockRepository.Create<Settings>();
        _mockSettings.SetupGet(s => s.TournamentCost).Returns(Default.TournamentCost);

        _mockInformationManagerFacade = MockRepository.Create<MBInformationManagerFacade>();
        _mockInformationManagerFacade.Setup(f => f.DisplayAsQuickBanner(It.IsAny<string>()));

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
    public void CreateTournament_OptionsAreValid_HasNoInitiatingHero_InitialTournament_ShouldReturnExpected()
    {
      SetUp(ValidOptions, NoExistingTournament, NoInitiatingHero, TournamentType.Initial);

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
    public void CreateTournament_OptionsAreValid_HasNoInitiatingHero_BirthTournament_HeroIsNotPlayerCharacter_ShouldReturnExpected()
    {
      SetUp(ValidOptions, NoExistingTournament, NoInitiatingHero, TournamentType.Birth, HeroIsNotHumanPlayerCharacter);

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Never),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Never),
        () => result.HasPayor.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.Payor.ShouldBe(_mockHero.Object),
        () => result.Succeeded.ShouldBe(true)
      );
    }

    [Test]
    public void CreateTournament_OptionsAreValid_HasNoInitiatingHero_BirthTournament_HeroIsPlayerCharacter_ShouldReturnExpected()
    {
      SetUp(ValidOptions, NoExistingTournament, NoInitiatingHero, TournamentType.Birth, HeroIsHumanPlayerCharacter);

      var result = _sut.CreateTournament(_options);

      result.ShouldSatisfyAllConditions(
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsQuickBanner(It.IsAny<string>()), Times.Once),
        () => _mockInformationManagerFacade.Verify(f => f.DisplayAsLogEntry(It.IsAny<string>()), Times.Never),
        () => result.HasPayor.ShouldBe(true),
        () => result.HostSettlement.ShouldBe(_mockSettlement.Object),
        () => result.Payor.ShouldBe(_mockHero.Object),
        () => result.Succeeded.ShouldBe(true)
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