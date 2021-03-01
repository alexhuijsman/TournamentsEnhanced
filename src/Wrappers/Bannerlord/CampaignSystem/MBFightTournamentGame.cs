using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBFightTournamentGame : MBTournamentGame
  {
    public MBFightTournamentGame(bool shouldInstantiate, MBTown town)
    {
      UnwrappedObject = shouldInstantiate ? new FightTournamentGame(town) : MBFightTournamentGame.Null;
    }

    public MBFightTournamentGame() { }

    public int MaxTeamSize => UnwrappedObject.MaxTeamSize;
    public int MaxTeamNumberPerMatch => UnwrappedObject.MaxTeamNumberPerMatch;

    public MBTextObject GetMenuText() => UnwrappedObject.GetMenuText();
    public void OpenMission(MBSettlement settlement, bool isPlayerParticipating) => UnwrappedObject.OpenMission(settlement, isPlayerParticipating);

    public static implicit operator FightTournamentGame(MBFightTournamentGame wrapper) => (FightTournamentGame)wrapper.UnwrappedObject;
    public static implicit operator MBFightTournamentGame(FightTournamentGame obj) => (MBFightTournamentGame)GetWrapper(obj);
  }
}
