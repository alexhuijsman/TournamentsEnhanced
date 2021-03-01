using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Builders.Abstract;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;


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
    private Mock<MBHero> _mockHero;
    private Mock<MBTown> _mockTown;
    private Mock<MBTournamentManager> _mockTournamentManager;
    private Mock<MBCampaign> _mockCampaign;
    private Mock<MBSettlement> _mockSettlement;
    private Mock<ModState> _mockModState;
    private CreateTournamentOptions _options;
    protected void SetUp(
      bool optionsAreValid,
      bool hasExistingTournament = false,
      bool hasInitiatingHero = false,
      TournamentType tournamentType = TournamentType.None)
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
        _mockModState = MockRepository.Create<ModState>();
        _mockModState.SetupGet(m => m.IsProduction).Returns(false);

        _mockHero = MockRepository.Create<MBHero>();
        _mockHero.SetupGet(s => s.IsNull).Returns(false);

        _mockTown = MockRepository.Create<MBTown>();
        _mockTown.SetupGet(t => t.HasTournament).Returns(hasExistingTournament);

        _mockTournamentManager = MockRepository.Create<MBTournamentManager>();

        _mockCampaign = MockRepository.Create<MBCampaign>();
        _mockCampaign.SetupGet(c => c.Current).Returns(_mockCampaign.Object);
        _mockCampaign.SetupGet(c => c.TournamentManager).Returns(_mockTournamentManager.Object);

        _mockSettlement = MockRepository.Create<MBSettlement>();
        _mockSettlement.SetupGet(s => s.IsNull).Returns(false);
        _mockSettlement.SetupGet(s => s.IsTown).Returns(true);
        _mockSettlement.SetupGet(s => s.Town).Returns(_mockTown.Object);

        _options = new CreateTournamentOptions()
        {
          InitiatingHero = hasInitiatingHero ? _mockHero.Object : MBHero.Null,
          Settlement = _mockSettlement.Object,
          Type = tournamentType
        };

        _sut.ModState = _mockModState.Object;
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
    public void CreateTournament_OptionsAreValid_HasNoInitiatingHero_InitialTournament_ShouldReturnSuccess()
    {
      SetUp(ValidOptions, NoExistingTournament, NoInitiatingHero, TournamentType.Initial);

      var result = _sut.CreateTournament(_options);

      result.Succeeded.ShouldBe(true);
    }
  }

  public class TournamentBuilderBaseImpl : TournamentBuilderBase
  {
    public TournamentBuilderBaseImpl() { }

    public new ModState ModState { set => base.ModState = value; }
    public new CreateTournamentResult CreateTournament(CreateTournamentOptions options)
      => base.CreateTournament(options);
  }
}