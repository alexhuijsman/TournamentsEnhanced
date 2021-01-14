using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBWeaponDesign : MBWrapperBase<MBWeaponDesign, WeaponDesign>
  {
    public static implicit operator WeaponDesign(MBWeaponDesign wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWeaponDesign(WeaponDesign obj) => GetWrapper(obj);
  }
}
