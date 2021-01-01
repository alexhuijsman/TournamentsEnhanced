using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBWeaponComponent : CachedWrapperBase<MBWeaponComponent, WeaponComponent>
  {
    public static implicit operator WeaponComponent(MBWeaponComponent wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBWeaponComponent(WeaponComponent obj) => MBWeaponComponent.GetWrapperFor(obj);
  }

  public class MBWeaponComponentList : List<MBWeaponComponent>
  {
    public static implicit operator List<WeaponComponent>(MBWeaponComponentList wrapperList) => wrapperList.Unwrap<MBWeaponComponent, WeaponComponent>();
    public static implicit operator MBWeaponComponentList(List<WeaponComponent> objectList) => (MBWeaponComponentList)objectList.Wrap<MBWeaponComponent, WeaponComponent>();
  }
}
