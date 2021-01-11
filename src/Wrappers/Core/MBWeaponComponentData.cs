using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBWeaponComponentData : MBWrapperBase<MBWeaponComponentData, WeaponComponentData>
  {
    public static implicit operator WeaponComponentData(MBWeaponComponentData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWeaponComponentData(WeaponComponentData obj) => MBWeaponComponentData.GetWrapper(obj);
  }
}
