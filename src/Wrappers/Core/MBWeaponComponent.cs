using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBWeaponComponent : MBObjectBaseWrapper<MBWeaponComponent, WeaponComponent>
  {
    public static implicit operator WeaponComponent(MBWeaponComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWeaponComponent(WeaponComponent obj) => MBWeaponComponent.GetWrapper(obj);
  }
}
