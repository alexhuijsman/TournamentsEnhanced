using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBItemComponent : MBObjectBaseWrapper<MBItemComponent, ItemComponent>
  {
    public static implicit operator ItemComponent(MBItemComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemComponent(ItemComponent obj) => MBItemComponent.GetWrapper(obj);
  }
}
