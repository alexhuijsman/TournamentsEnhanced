using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBTradeItemComponent : MBObjectBaseWrapper<MBTradeItemComponent, TradeItemComponent>
  {
    public static implicit operator TradeItemComponent(MBTradeItemComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTradeItemComponent(TradeItemComponent obj) => MBTradeItemComponent.GetWrapper(obj);
  }

  public class MBTradeItemComponentList : MBListBase<MBTradeItemComponent, MBTradeItemComponentList>
  {
    public MBTradeItemComponentList(params MBTradeItemComponent[] wrappers) : this((IEnumerable<MBTradeItemComponent>)wrappers) { }
    public MBTradeItemComponentList(IEnumerable<MBTradeItemComponent> wrappers) => AddRange(wrappers);
    public MBTradeItemComponentList(MBTradeItemComponent wrapper) => Add(wrapper);
    public MBTradeItemComponentList() { }

    public static implicit operator List<TradeItemComponent>(MBTradeItemComponentList wrapperList) => wrapperList.Unwrap<MBTradeItemComponent, TradeItemComponent>();
    public static implicit operator MBTradeItemComponentList(List<TradeItemComponent> objectList) => (MBTradeItemComponentList)objectList.Wrap<MBTradeItemComponent, TradeItemComponent>();
    public static implicit operator MBTradeItemComponent[](MBTradeItemComponentList wrapperList) => wrapperList.ToArray();
  }
}
