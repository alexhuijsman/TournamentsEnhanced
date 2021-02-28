using System.Collections.Generic;
using System.Linq;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTournamentMatch : MBWrapperBase<MBTournamentMatch, TournamentMatch>
  {
    public static implicit operator TournamentMatch(MBTournamentMatch wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentMatch(TournamentMatch obj) => GetWrapper(obj);

    public virtual bool IsPlayerParticipating() => UnwrappedObject.IsPlayerParticipating();
    public virtual MBSettlement HostSettlement => UnwrappedObject.Winners.First().Character.HeroObject.CurrentSettlement;
  }
}
