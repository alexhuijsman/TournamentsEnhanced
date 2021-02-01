using HarmonyLib;
using Moq;
using NUnit.Framework;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TournamentsEnhanced;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models;


namespace Test
{
  public class SubModuleTest : TestBase
  {
    private const bool UseCampaignGameType = true;
    private const bool UseNonCampaignGameType = false;
    private SubModuleImpl _sut;
    private Mock<TournamentBuilder> _mockTournamentBuilder;
    private Mock<ModState> _mockModState;
    private object _object;
    private Game _game;

    public void SetUp(bool useCampaignGameType)
    {
      _mockTournamentBuilder = MockRepository.Create<TournamentBuilder>();
      _mockTournamentBuilder.Setup(tournamentBuilder => tournamentBuilder.CreateInitialTournaments());
      _mockModState = MockRepository.Create<ModState>();
      _mockModState.Setup(modState => modState.Reset());
      _object = MockRepository.Create<object>().Object;
      GameType gameType;

      if (useCampaignGameType)
      {
        var mockCampaignGameType = MockRepository.Create<Campaign>(CampaignGameMode.Campaign);
        gameType = mockCampaignGameType.Object;
      }
      else
      {
        var mockNonCampaignGameType = MockRepository.Create<GameType>();
        gameType = mockNonCampaignGameType.Object;
      }

      _game = (Game)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(Game));
      _game.GetType().GetProperty("GameType").SetValue(_game, gameType);

      _sut = new SubModuleImpl();
      _sut.ModState = _mockModState.Object;
      _sut.TournamentBuilder = _mockTournamentBuilder.Object;
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsCampaign_ModStateIsReset()
    {
      SetUp(UseCampaignGameType);

      _sut.OnNewGameCreated(_game, _object);

      _mockModState.Verify(modState => modState.Reset(), Times.Once);
      _mockModState.VerifyNoOtherCalls();
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsCampaign_CreateInitialTournamentsIsCalled()
    {
      SetUp(UseCampaignGameType);

      _sut.OnNewGameCreated(_game, _object);

      _mockTournamentBuilder.Verify(tournamentBuilder => tournamentBuilder.CreateInitialTournaments(), Times.Once);
      _mockTournamentBuilder.VerifyNoOtherCalls();
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsNotCampaign_ModStateIsNotReset()
    {
      SetUp(UseNonCampaignGameType);

      _sut.OnNewGameCreated(_game, _object);

      _mockModState.VerifyNoOtherCalls();
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsNotCampaign_CreateInitialTournamentsIsNotCalled()
    {
      SetUp(UseNonCampaignGameType);

      _sut.OnNewGameCreated(_game, _object);

      _mockTournamentBuilder.VerifyNoOtherCalls();
    }

    private class SubModuleImpl : SubModule
    {
      public new TournamentBuilder TournamentBuilder { set => base.TournamentBuilder = value; }
      public new ModState ModState { set => base.ModState = value; }
    }
    private class HarmonyImpl : Harmony
    {
      public HarmonyImpl(string id) : base(id)
      {
      }
    }
  }
}