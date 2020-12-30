using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class TournamentRecords
  {
    private static Dictionary<Town, TournamentRecord> Tournaments = new Dictionary<Town, TournamentRecord>();

    public static void AddOrUpdateForCurrentTown(TournamentRecord record)
    {
      AddOrUpdateForTown(record, MainHero.CurrentTown);
    }

    public static void AddOrUpdateForTown(TournamentRecord record, Town town)
    {
      Tournaments[town] = record;
    }

    public static TournamentRecord GetForCurrentTown()
    {
      return GetRecordByTown(MainHero.CurrentTown);
    }

    private static TournamentRecord GetRecordByTown(Town town)
    {
      return Tournaments[town];
    }

    public static void RemoveByTown(Town town)
    {
      Tournaments.Remove(town);
    }
  }
}
