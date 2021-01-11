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
    List<MBTournamentParticipant> Participants { get; }
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

    public List<MBTournamentParticipant> Participants => UnwrappedObject.Participants.CastList<MBTournamentParticipant>();

    public int Score => UnwrappedObject.Score;

    public void AddParticipant(MBTournamentParticipant participant) => UnwrappedObject.AddParticipant(participant);

    public bool IsParticipantRequired() => UnwrappedObject.IsParticipantRequired();
    public static implicit operator TournamentTeam(MBTournamentTeam wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentTeam(TournamentTeam obj) => MBTournamentTeam.GetWrapper(obj);
  }
}
