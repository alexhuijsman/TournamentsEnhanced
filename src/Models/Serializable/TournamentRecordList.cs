using System.Collections.Generic;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced.Models.Serializable
{
  public class TournamentRecordDictionary : Dictionary<string, TournamentRecord>
  {
    public TournamentRecord ForCurrentTown()
    {
      return this[MBHero.MainHero.CurrentTown.Settlement.StringId];
    }

    public TournamentRecord this[MBTown town] => this[town.Settlement.StringId];

    public void AddOrUpdate(TournamentRecord record)
    {
      if (ContainsKey(record.hostSettlementId))
      {
        this[record.hostSettlementId] = record;
      }
      else
      {
        Add(record.hostSettlementId, record);
      }
    }
    public void Remove(MBTown town) => Remove(town.Settlement.StringId);
  }
}
