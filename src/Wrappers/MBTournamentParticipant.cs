using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTournamentParticipant : CachedWrapperBase<MBTournamentParticipant, TournamentParticipant>
  {
    public static implicit operator TournamentParticipant(MBTournamentParticipant wrapper) => wrapper.Unwrap();
    public static implicit operator MBTournamentParticipant(TournamentParticipant obj) => MBTournamentParticipant.GetWrapperFor(obj);
  }
}
