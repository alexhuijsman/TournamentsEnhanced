using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Models.Serializable
{
  public class TournamentRecordDictionary : Dictionary<string, TournamentRecord>
  {
    protected MBHero MBHero { get; set; } = MBHero.Instance;

    public virtual TournamentRecord this[MBSettlement settlement]
    {
      get
      {
        var settlementId = settlement.StringId;

        return ContainsKey(settlementId) ? this[settlementId] : default(TournamentRecord);
      }

      set
      {
        if (settlement == MBSettlement.Null)
        {
          return;
        }

        this[settlement.StringId] = value;
      }
    }

    public TournamentRecord ForCurrentSettlement()
    {
      var currentSettlement = MBHero.MainHero?.CurrentSettlement;
      return currentSettlement == null ?
              default(TournamentRecord) :
              this[currentSettlement];
    }

    public virtual bool ContainsSettlement(MBSettlement settlement) => ContainsKey(settlement.StringId);
    public virtual void AddOrUpdate(TournamentRecord record) => this[record.hostSettlementStringId] = record;
    public virtual void Remove(MBSettlement settlement) => Remove(settlement.StringId);
  }
}
