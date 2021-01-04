using System.Collections.Generic;

using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public struct FindHostSettlementOptions : IFindOptionsBase<MBSettlement, MBSettlementList>
  {
    public Payor Payor;

    public MBSettlementList Candidates { get; set; }
    public IComparer<MBSettlement>[] Comparers { get; set; }
  }
}
