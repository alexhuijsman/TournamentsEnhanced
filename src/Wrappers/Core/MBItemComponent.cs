using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBItemComponent : MBObjectBaseWrapper<MBItemComponent, ItemComponent>
  {
    public static implicit operator ItemComponent(MBItemComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemComponent(ItemComponent obj) => MBItemComponent.GetWrapperFor(obj);
  }

  public class MBItemComponentList : MBListBase<MBItemComponent, MBItemComponentList>
  {
    public static implicit operator List<ItemComponent>(MBItemComponentList wrapperList) => wrapperList.Unwrap<MBItemComponent, ItemComponent>();
    public static implicit operator MBItemComponentList(List<ItemComponent> objectList) => (MBItemComponentList)objectList.Wrap<MBItemComponent, ItemComponent>();
  }
}
