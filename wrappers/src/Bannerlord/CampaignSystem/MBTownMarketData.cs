using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTownMarketData : MBWrapperBase<MBTownMarketData, TownMarketData>
  {
    public static implicit operator TownMarketData(MBTownMarketData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTownMarketData(TownMarketData obj) => GetWrapper(obj);
  }
}
