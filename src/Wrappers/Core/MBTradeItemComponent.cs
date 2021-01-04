using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBTradeItemComponent : MBObjectBaseWrapper<MBTradeItemComponent, TradeItemComponent>
  {
    public static implicit operator TradeItemComponent(MBTradeItemComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTradeItemComponent(TradeItemComponent obj) => MBTradeItemComponent.GetWrapperFor(obj);
  }

  public class MBTradeItemComponentList : List<MBTradeItemComponent>
  {
    public static implicit operator List<TradeItemComponent>(MBTradeItemComponentList wrapperList) => wrapperList.Unwrap<MBTradeItemComponent, TradeItemComponent>();
    public static implicit operator MBTradeItemComponentList(List<TradeItemComponent> objectList) => (MBTradeItemComponentList)objectList.Wrap<MBTradeItemComponent, TradeItemComponent>();
  }
}
