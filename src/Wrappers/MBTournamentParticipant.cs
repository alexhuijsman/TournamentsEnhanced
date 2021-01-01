using System.Collections.Generic;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTournamentParticipant : CachedWrapperBase<MBTournamentParticipant, TournamentParticipant>
  {
    public static implicit operator TournamentParticipant(MBTournamentParticipant wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBTournamentParticipant(TournamentParticipant obj) => MBTournamentParticipant.GetWrapperFor(obj);
  }

  public class MBTournamentParticipantList : List<MBTournamentParticipant>
  {
    public static implicit operator List<TournamentParticipant>(MBTournamentParticipantList wrapperList) => wrapperList.Unwrap<MBTournamentParticipant, TournamentParticipant>();
    public static implicit operator MBTournamentParticipantList(List<TournamentParticipant> objectList) => (MBTournamentParticipantList)objectList.Wrap<MBTournamentParticipant, TournamentParticipant>();
  }
}
