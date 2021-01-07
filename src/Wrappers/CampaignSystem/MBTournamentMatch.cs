using System.Collections.Generic;
using System.Linq;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTournamentMatch : MBWrapperBase<MBTournamentMatch, TournamentMatch>
  {
    public static implicit operator TournamentMatch(MBTournamentMatch wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentMatch(TournamentMatch obj) => MBTournamentMatch.GetWrapperFor(obj);

    public bool IsPlayerParticipating() => UnwrappedObject.IsPlayerParticipating();
    public MBSettlement HostSettlement => UnwrappedObject.Winners.First().Character.HeroObject.CurrentSettlement;
  }

  public class MBTournamentMatchList : MBListBase<MBTournamentMatch, MBTournamentMatchList>
  {
    public MBTournamentMatchList(params MBTournamentMatch[] wrappers) : this((IEnumerable<MBTournamentMatch>)wrappers) { }
    public MBTournamentMatchList(IEnumerable<MBTournamentMatch> wrappers) => AddRange(wrappers);
    public MBTournamentMatchList(MBTournamentMatch wrapper) => Add(wrapper);
    public MBTournamentMatchList() { }

    public static implicit operator List<TournamentMatch>(MBTournamentMatchList wrapperList) => wrapperList.Unwrap<MBTournamentMatch, TournamentMatch>();
    public static implicit operator MBTournamentMatchList(List<TournamentMatch> objectList) => (MBTournamentMatchList)objectList.Wrap<MBTournamentMatch, TournamentMatch>();
  }
}
