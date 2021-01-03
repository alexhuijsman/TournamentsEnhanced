using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBArmorComponent : CachedWrapperBase<MBArmorComponent, ArmorComponent>
  {
    public static implicit operator ArmorComponent(MBArmorComponent wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBArmorComponent(ArmorComponent obj) => MBArmorComponent.GetWrapperFor(obj);
  }

  public class MBArmorComponentList : List<MBArmorComponent>
  {
    public static implicit operator List<ArmorComponent>(MBArmorComponentList wrapperList) => wrapperList.Unwrap<MBArmorComponent, ArmorComponent>();
    public static implicit operator MBArmorComponentList(List<ArmorComponent> objectList) => (MBArmorComponentList)objectList.Wrap<MBArmorComponent, ArmorComponent>();
  }
}
