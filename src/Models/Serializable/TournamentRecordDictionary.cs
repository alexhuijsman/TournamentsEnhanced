using System;
using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Models.Serializable
{
  public class TournamentRecordDictionary : Dictionary<string, TournamentRecord>
  {
    public TournamentRecord ForCurrentTown()
    {
      return this[MBHero.MainHero.CurrentTown.Settlement.StringId];
    }

    public bool ContainsSettlement(MBSettlement settlement) => ContainsKey(settlement.StringId);

    public TournamentRecord this[MBSettlement settlement] => this[settlement.StringId];

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

    internal void Remove(object hostSettlement)
    {
      throw new NotImplementedException();
    }

    public void Remove(MBSettlement settlement) => Remove(settlement.StringId);
  }
}
