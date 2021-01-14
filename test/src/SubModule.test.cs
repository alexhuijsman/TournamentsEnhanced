using Moq;

using NUnit.Framework;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models.ModState;

namespace TournamentsEnhanced.UnitTests
{
  public class SubModuleTest
  {
    private SubModule sut;
    private Mock<TournamentBuilder> mockTournamentBuilder;
    private Mock<ModState> mockModState;
    private Mock<Campaign> mockCampaignGameType;
    private Mock<GameType> mockNonCampaignGameType;
    private Game game;
    private Mock<object> mockObject;

    [SetUp]
    public void SetUp()
    {
      mockTournamentBuilder = new Mock<TournamentBuilder>(MockBehavior.Strict);
      mockTournamentBuilder.Setup(tournamentBuilder => tournamentBuilder.CreateInitialTournaments());
      mockModState = new Mock<ModState>(MockBehavior.Strict);
      mockModState.Setup(modState => modState.Reset());
      mockObject = new Mock<object>(MockBehavior.Strict);
      mockCampaignGameType = new Mock<Campaign>(MockBehavior.Strict, CampaignGameMode.Campaign);
      mockNonCampaignGameType = new Mock<GameType>(MockBehavior.Strict);

      game = (Game)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(Game));

      sut = new SubModule();
      sut.ModState = mockModState.Object;
      sut.TournamentBuilder = mockTournamentBuilder.Object;
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsCampaign_ModStateIsReset()
    {
      SetGameType(mockCampaignGameType.Object);

      sut.OnNewGameCreated(game, mockObject.Object);

      mockModState.Verify(modState => modState.Reset());
      mockModState.VerifyNoOtherCalls();
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsCampaign_CreateInitialTournamentsIsCalled()
    {
      SetGameType(mockCampaignGameType.Object);

      sut.OnNewGameCreated(game, mockObject.Object);

      mockTournamentBuilder.Verify(tournamentBuilder => tournamentBuilder.CreateInitialTournaments());
      mockTournamentBuilder.VerifyNoOtherCalls();
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsNotCampaign_ModStateIsNotReset()
    {
      SetGameType(mockNonCampaignGameType.Object);

      sut.OnNewGameCreated(game, mockObject.Object);

      mockModState.VerifyNoOtherCalls();
    }

    [Test]
    public void OnNewGameCreated_GameTypeIsNotCampaign_CreateInitialTournamentsIsNotCalled()
    {
      SetGameType(mockNonCampaignGameType.Object);

      sut.OnNewGameCreated(game, mockObject.Object);

      mockTournamentBuilder.VerifyNoOtherCalls();
    }

    private void SetGameType(GameType gameType)
    {
      game.GetType().GetProperty("GameType").SetValue(game, gameType);
    }
  }
}
