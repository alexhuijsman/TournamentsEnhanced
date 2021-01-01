using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTournamentTeam : CachedWrapperBase<MBTournamentTeam, TournamentTeam>
  {
    public static implicit operator  TournamentTeam(MBTournamentTeam wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentTeam(TournamentTeam obj) => MBTournamentTeam.GetWrapperFor(obj);
  }

  public class MBTournamentTeamList : List<MBTournamentTeam>
  {
    public static implicit operator List<TournamentTeam>(MBTournamentTeamList wrapperList) => wrapperList.Unwrap<MBTournamentTeam, TournamentTeam>();
    public static implicit operator MBTournamentTeamList(List<TournamentTeam> objectList) => (MBTournamentTeamList)objectList.Wrap<MBTournamentTeam, TournamentTeam>();
  }
}
