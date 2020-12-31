using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTown : CachedWrapper<MBTown, Town>
  {
    public static List<Town> AllTownsWithoutTournaments => Town.AllTowns.ToList().FindAll((town) => !town.HasTournament);
    public static List<Town> AllTownsWithTournaments => Town.AllTowns.ToList().FindAll((town) => town.HasTournament);
    public static List<Town> AllTowns => Town.AllTowns.ToList();
  }
}
