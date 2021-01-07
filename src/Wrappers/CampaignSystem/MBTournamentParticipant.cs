using System.Collections.Generic;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTournamentParticipant : MBWrapperBase<MBTournamentParticipant, TournamentParticipant>
  {
    public static implicit operator TournamentParticipant(MBTournamentParticipant wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentParticipant(TournamentParticipant obj) => MBTournamentParticipant.GetWrapperFor(obj);
  }

  public class MBTournamentParticipantList : MBListBase<MBTournamentParticipant, MBTournamentParticipantList>
  {
    public MBTournamentParticipantList(params MBTournamentParticipant[] wrappers) : this((IEnumerable<MBTournamentParticipant>)wrappers) { }
    public MBTournamentParticipantList(IEnumerable<MBTournamentParticipant> wrappers) => AddRange(wrappers);
    public MBTournamentParticipantList(MBTournamentParticipant wrapper) => Add(wrapper);
    public MBTournamentParticipantList() { }

    public static implicit operator List<TournamentParticipant>(MBTournamentParticipantList wrapperList) => wrapperList.Unwrap<MBTournamentParticipant, TournamentParticipant>();
    public static implicit operator MBTournamentParticipantList(List<TournamentParticipant> objectList) => (MBTournamentParticipantList)objectList.Wrap<MBTournamentParticipant, TournamentParticipant>();
    public static implicit operator MBTournamentParticipant[](MBTournamentParticipantList wrapperList) => wrapperList.ToArray();
  }
}
