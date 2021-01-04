using System.Collections.Generic;

using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced
{
  public struct FindHostSettlementOptions
  {
    public Payor Payor;
    public MBSettlementList Settlements;
    public IComparer<MBSettlement>[] Comparers;
  }
}
