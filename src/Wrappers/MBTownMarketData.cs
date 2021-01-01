using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTownMarketData : CachedWrapperBase<MBTownMarketData, TownMarketData>
  {
    public static implicit operator TownMarketData(MBTownMarketData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTownMarketData(TownMarketData obj) => MBTownMarketData.GetWrapperFor(obj);
  }
}
