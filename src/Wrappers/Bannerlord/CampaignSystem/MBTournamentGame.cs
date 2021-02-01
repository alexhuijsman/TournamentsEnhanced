using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTournamentGame : MBWrapperBase<MBTournamentGame, TournamentGame>
  {
    public virtual MBTown Town => UnwrappedObject.Town;

    public static implicit operator TournamentGame(MBTournamentGame wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTournamentGame(TournamentGame obj) => GetWrapper(obj);
  }
}
