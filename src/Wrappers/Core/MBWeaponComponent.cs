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
    public static implicit operator List<WeaponComponent>(MBWeaponComponentList wrapperList) => wrapperList.Unwrap<MBWeaponComponent, WeaponComponent>();
    public static implicit operator MBWeaponComponentList(List<WeaponComponent> objectList) => (MBWeaponComponentList)objectList.Wrap<MBWeaponComponent, WeaponComponent>();
  }
}
