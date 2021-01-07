using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTournamentGame : MBWrapperBase<MBTournamentGame, TournamentGame>
  {
    public MBTown Town => UnwrappedObject.Town;

    public static implicit operator TournamentGame(MBTournamentGame wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentGame(TournamentGame obj) => MBTournamentGame.GetWrapperFor(obj);
  }

  public class MBTournamentGameList : MBListBase<MBTournamentGame, MBTournamentGameList>
  {
    public MBTournamentGameList(params MBTournamentGame[] wrappers) : this((IEnumerable<MBTournamentGame>)wrappers) { }
    public MBTournamentGameList(IEnumerable<MBTournamentGame> wrappers) => AddRange(wrappers);
    public MBTournamentGameList(MBTournamentGame wrapper) => Add(wrapper);
    public MBTournamentGameList() { }

    public static implicit operator List<TournamentGame>(MBTournamentGameList wrapperList) => wrapperList.Unwrap<MBTournamentGame, TournamentGame>();
    public static implicit operator MBTournamentGameList(List<TournamentGame> objectList) => (MBTournamentGameList)objectList.Wrap<MBTournamentGame, TournamentGame>();
    public static implicit operator MBTournamentGame[](MBTournamentGameList wrapperList) => wrapperList.ToArray();
  }
}
