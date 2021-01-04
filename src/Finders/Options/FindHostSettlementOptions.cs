using System.Collections.Generic;

using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced
{
  public struct FindHostSettlementOptions
  {
    public IComparer<MBSettlement>[] Comparers;
    public MBSettlementList Settlements;
    public Payor Payor;
  }
}
