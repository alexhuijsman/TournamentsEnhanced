using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTown : CachedWrapperBase<MBTown, Town>
  {
    public static List<Town> AllTownsWithoutTournaments => Town.AllTowns.ToList().FindAll((town) => !town.HasTournament);
    public static List<Town> AllTownsWithTournaments => Town.AllTowns.ToList().FindAll((town) => town.HasTournament);
    public static List<Town> AllTowns => Town.AllTowns.ToList();

    public static implicit operator Town(MBTown wrapper) => wrapper.Unwrap();
    public static implicit operator MBTown(Town obj) => MBTown.GetWrapperFor(obj);
  }
}
