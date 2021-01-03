using System.Collections.Generic;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{

  public interface ITournamentTeam
  {
    int TeamSize { get; }
    uint TeamColor { get; }
    MBBanner TeamBanner { get; }
    bool IsPlayerTeam { get; }
    MBTournamentParticipantList Participants { get; }
    int Score { get; }

    void AddParticipant(MBTournamentParticipant participant);
    bool IsParticipantRequired();
  }

  public class MBTournamentTeam : CachedWrapperBase<MBTournamentTeam, TournamentTeam>, ITournamentTeam
  {
    public int TeamSize => UnwrappedObject.TeamSize;

    public uint TeamColor => UnwrappedObject.TeamColor;

    public MBBanner TeamBanner => UnwrappedObject.TeamBanner;

    public bool IsPlayerTeam => UnwrappedObject.IsPlayerTeam;

    public MBTournamentParticipantList Participants => UnwrappedObject.Participants.ToList();

    public int Score => UnwrappedObject.Score;

    public void AddParticipant(MBTournamentParticipant participant) => UnwrappedObject.AddParticipant(participant);

    public bool IsParticipantRequired() => UnwrappedObject.IsParticipantRequired();
    public static implicit operator TournamentTeam(MBTournamentTeam wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentTeam(TournamentTeam obj) => MBTournamentTeam.GetWrapperFor(obj);
  }

  public class MBTournamentTeamList : List<MBTournamentTeam>
  {
    public static implicit operator List<TournamentTeam>(MBTournamentTeamList wrapperList) => wrapperList.Unwrap<MBTournamentTeam, TournamentTeam>();
    public static implicit operator MBTournamentTeamList(List<TournamentTeam> objectList) => (MBTournamentTeamList)objectList.Wrap<MBTournamentTeam, TournamentTeam>();
  }
}
