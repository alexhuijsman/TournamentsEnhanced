using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTownMarketData : CachedWrapperBase<MBTownMarketData, TownMarketData>
  {
    public static implicit operator TownMarketData(MBTownMarketData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTownMarketData(TownMarketData obj) => MBTownMarketData.GetWrapperFor(obj);
  }

  public class MBTownMarketDataList : List<MBTownMarketData>
  {
    public static implicit operator List<TownMarketData>(MBTownMarketDataList wrapperList) => wrapperList.Unwrap<MBTownMarketData, TownMarketData>();
    public static implicit operator MBTownMarketDataList(List<TownMarketData> objectList) => (MBTownMarketDataList)objectList.Wrap<MBTownMarketData, TownMarketData>();
  }
}
