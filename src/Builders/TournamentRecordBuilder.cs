using System.Collections.Generic;

using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced.Builders
{
  public static class TournamentRecordBuilder
  {


    public static List<TournamentRecord> All => Records.Values.ToList();
    public static void ReplaceAll(List<TournamentRecord> replacementRecords)
    {
      Records.Clear();

      MBTown hostTown;
      foreach (var record in replacementRecords)
      {
        hostTown = MBTown.Find(record.hostSettlementId);
        Records.Add(hostTown, record);
      }
    }

    private static Dictionary<MBTown, TournamentRecord> Records =
      new Dictionary<MBTown, TournamentRecord>();

    public static void AddOrUpdateForCurrentTown(TournamentRecord record)
    {
      AddOrUpdateForTown(record, MBHero.MainHero.CurrentTown);
    }

    public static void AddOrUpdateForTown(TournamentRecord record, MBTown town)
    {
      Records[town] = record;
    }

    public static TournamentRecord GetForCurrentTown()
    {
      return GetForTown(MBHero.MainHero.CurrentTown);
    }

    public static TournamentType GetTournamentTypeForTown(MBTown town)
    {
      return Records[town].type;
    }

    public static TournamentRecord GetForTown(MBTown town)
    {
      return Records[town];
    }

    public static void RemoveByTown(MBTown town)
    {
      Records.Remove(town);
    }
  }
}
