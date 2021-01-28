using Moq;
using NUnit.Framework;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TournamentsEnhanced;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models;

namespace Tests
{
  public class SubModuleTest
  {
    private SubModule _sut;
    private Mock<TournamentBuilder> _mockTournamentBuilder;
    private Mock<ModState> _mockModState;
    private Mock<Campaign> _mockCampaignGameType;
    private Mock<GameType> _mockNonCampaignGameType;
    private Game _game;
    private Mock<object> _mockObject;

    [SetUp]
    public void SetUp()
    {
      _mockTournamentBuilder = new Mock<TournamentBuilder>(MockBehavior.Strict);
      _mockTournamentBuilder.Setup(tournamentBuilder => tournamentBuilder.CreateInitialTournaments());
      _mockModState = new Mock<ModState>(MockBehavior.Strict);
      _mockModState.Setup(modState => modState.Reset());
      _mockObject = new Mock<object>(MockBehavior.Strict);
      _mockCampaignGameType = new Mock<Campaign>(MockBehavior.Strict, CampaignGameMode.Campaign);
      _mockNonCampaignGameType = new Mock<GameType>(MockBehavior.Strict);

      _game = (Game)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(Game));

      _sut = new SubModule();
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
  }
}
