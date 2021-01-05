using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindHostSettlementOptions : FindOptionsBase<MBSettlement, MBSettlementList>
  {
    public Payor Payor { get; private set; }
  }
}
