using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class TournamentRecords
  {


    public static List<TournamentRecord> All => Records.Values.ToList();
    public static List<TournamentRecord> LoadAll { set => LoadAllFromList(value); }

    private static void LoadAllFromList(List<TournamentRecord> records)
    {
      Records.Clear();
      foreach (var record in records)
      {
        Records.Add(record);
      }
    }

    private static Dictionary<Town, TournamentRecord> Records = new Dictionary<Town, TournamentRecord>();

    public static void AddOrUpdateForCurrentTown(TournamentRecord record)
    {
      AddOrUpdateForTown(record, MainHero.CurrentTown);
    }

    public static void AddOrUpdateForTown(TournamentRecord record, Town town)
    {
      Records[town] = record;
    }

    public static TournamentRecord GetForCurrentTown()
    {
      return GetForTown(MainHero.CurrentTown);
    }

    public static TournamentType GetTournamentTypeForTown(Town town)
    {
      return Records[town].type;
    }

    public static TournamentRecord GetForTown(Town town)
    {
      return Records[town];
    }

    public static void RemoveByTown(Town town)
    {
      Records.Remove(town);
    }
  }
}
