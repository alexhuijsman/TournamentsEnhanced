using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class TournamentRecords
  {
    private static Dictionary<Town, TournamentRecord> Tournaments = new Dictionary<Town, TournamentRecord>();

    public static void AddOrReplaceForTown(TournamentRecord record, Town town)
    {
      Tournaments[town] = record;
    }

    public static bool ExistsForTown(Town town)
    {
      return Tournaments.ContainsKey(town);
    }

    public static TournamentRecord GetRecordForCurrentTown()
    {
      return GetRecordByTown(MainHero.CurrentTown);
    }

    public static void UpdateRecordForCurrentTown(TournamentRecord record)
    {
      Tournaments[MainHero.CurrentTown] = record;
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
