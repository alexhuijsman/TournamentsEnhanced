using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class MBTown
  {
    public static List<Town> AllTownsWithoutTournaments => Town.AllTowns.ToList().FindAll((town) => !town.HasTournament);
    public static List<Town> AllTownsWithTournaments => Town.AllTowns.ToList().FindAll((town) => town.HasTournament);
  }
}
