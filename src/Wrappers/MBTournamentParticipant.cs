using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTournamentParticipant : CachedWrapperBase<MBTournamentParticipant, TournamentParticipant>
  {
    public static implicit operator TournamentParticipant(MBTournamentParticipant wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentParticipant(TournamentParticipant obj) => MBTournamentParticipant.GetWrapperFor(obj);
  }
}
