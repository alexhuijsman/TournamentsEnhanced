using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBWeaponComponent : MBObjectBaseWrapper<MBWeaponComponent, WeaponComponent>
  {
    public static implicit operator WeaponComponent(MBWeaponComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWeaponComponent(WeaponComponent obj) => MBWeaponComponent.GetWrapperFor(obj);
  }

  public class MBWeaponComponentList : MBListBase<MBWeaponComponent, MBWeaponComponentList>
  {
    public MBWeaponComponentList(params MBWeaponComponent[] wrappers) : this((IEnumerable<MBWeaponComponent>)wrappers) { }
    public MBWeaponComponentList(IEnumerable<MBWeaponComponent> wrappers) => AddRange(wrappers);
    public MBWeaponComponentList(MBWeaponComponent wrapper) => Add(wrapper);
    public MBWeaponComponentList() { }

    public static implicit operator List<WeaponComponent>(MBWeaponComponentList wrapperList) => wrapperList.Unwrap<MBWeaponComponent, WeaponComponent>();
    public static implicit operator MBWeaponComponentList(List<WeaponComponent> objectList) => (MBWeaponComponentList)objectList.Wrap<MBWeaponComponent, WeaponComponent>();
    public static implicit operator MBWeaponComponent[](MBWeaponComponentList wrapperList) => wrapperList.ToArray();
  }
}
