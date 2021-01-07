using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBArmorComponent : MBObjectBaseWrapper<MBArmorComponent, ArmorComponent>
  {
    public static implicit operator ArmorComponent(MBArmorComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBArmorComponent(ArmorComponent obj) => MBArmorComponent.GetWrapperFor(obj);
  }

  public class MBArmorComponentList : MBListBase<MBArmorComponent, MBArmorComponentList>
  {
    public MBArmorComponentList(params MBArmorComponent[] wrappers) : this((IEnumerable<MBArmorComponent>)wrappers) { }
    public MBArmorComponentList(IEnumerable<MBArmorComponent> wrappers) => AddRange(wrappers);
    public MBArmorComponentList(MBArmorComponent wrapper) => Add(wrapper);
    public MBArmorComponentList() { }

    public static implicit operator List<ArmorComponent>(MBArmorComponentList wrapperList) => wrapperList.Unwrap<MBArmorComponent, ArmorComponent>();
    public static implicit operator MBArmorComponentList(List<ArmorComponent> objectList) => (MBArmorComponentList)objectList.Wrap<MBArmorComponent, ArmorComponent>();
    public static implicit operator MBArmorComponent[](MBArmorComponentList wrapperList) => wrapperList.ToArray();
  }
}
