using System.Collections.Generic;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTournamentParticipant : MBWrapperBase<MBTournamentParticipant, TournamentParticipant>
  {
    public static implicit operator TournamentParticipant(MBTournamentParticipant wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentParticipant(TournamentParticipant obj) => GetWrapper(obj);
  }
}
