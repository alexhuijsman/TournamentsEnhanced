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
    private SubModuleImpl _sut;
    private Mock<TournamentBuilder> _mockTournamentBuilder;
    private Mock<ModState> _mockModState;
    private Mock<Campaign> _mockCampaignGameType;
    private Mock<GameType> _mockNonCampaignGameType;
    private Game _game;
    private Mock<object> _mockObject;

    [SetUp]
    public void SetUp()
    {
      _mockTournamentBuilder = MockRepository.Create<TournamentBuilder>();
      _mockTournamentBuilder.Setup(tournamentBuilder => tournamentBuilder.CreateInitialTournaments());
      _mockModState = MockRepository.Create<ModState>();
      _mockModState.Setup(modState => modState.Reset());
      _mockObject = MockRepository.Create<object>();
      _mockCampaignGameType = MockRepository.Create<Campaign>(CampaignGameMode.Campaign);
      _mockNonCampaignGameType = MockRepository.Create<GameType>();

      _game = (Game)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(Game));

      _sut = new SubModuleImpl();
      _sut.ModState = _mockModState.Object;
      _sut.TournamentBuilder = _mockTournamentBuilder.Object;
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsCampaign_ModStateIsReset()
    {
      SetGameType(_mockCampaignGameType.Object);

      _sut.OnNewGameCreated(_game, _mockObject.Object);

      _mockModState.Verify(modState => modState.Reset(), Times.Once);
      _mockModState.VerifyNoOtherCalls();
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsCampaign_CreateInitialTournamentsIsCalled()
    {
      SetGameType(_mockCampaignGameType.Object);

      _sut.OnNewGameCreated(_game, _mockObject.Object);

      _mockTournamentBuilder.Verify(tournamentBuilder => tournamentBuilder.CreateInitialTournaments(), Times.Once);
      _mockTournamentBuilder.VerifyNoOtherCalls();
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsNotCampaign_ModStateIsNotReset()
    {
      SetGameType(_mockNonCampaignGameType.Object);

      _sut.OnNewGameCreated(_game, _mockObject.Object);

      _mockModState.VerifyNoOtherCalls();
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsNotCampaign_CreateInitialTournamentsIsNotCalled()
    {
      SetGameType(_mockNonCampaignGameType.Object);

      _sut.OnNewGameCreated(_game, _mockObject.Object);

      _mockTournamentBuilder.VerifyNoOtherCalls();
    }

    private void SetGameType(GameType gameType)
    {
      _game.GetType().GetProperty("GameType").SetValue(_game, gameType);
    }

    private class SubModuleImpl : SubModule
    {
      public new TournamentBuilder TournamentBuilder { set => base.TournamentBuilder = value; }
      public new ModState ModState { set => base.ModState = value; }
    }
  }
}