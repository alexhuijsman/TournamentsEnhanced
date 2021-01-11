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
}
