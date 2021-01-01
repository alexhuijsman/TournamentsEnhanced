using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTournamentGame : CachedWrapperBase<MBTournamentGame, TournamentGame>
  {
    public static implicit operator TournamentGame(MBTournamentGame wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentGame(TournamentGame obj) => MBTournamentGame.GetWrapperFor(obj);
  }

  public class MBTournamentGameList : List<MBTournamentGame>
  {
    public static implicit operator List<TournamentGame>(MBTournamentGameList wrapperList) => wrapperList.Unwrap<MBTournamentGame, TournamentGame>();
    public static implicit operator MBTournamentGameList(List<TournamentGame> objectList) => (MBTournamentGameList)objectList.Wrap<MBTournamentGame, TournamentGame>();
  }
}
