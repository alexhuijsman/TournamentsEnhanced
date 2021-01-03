using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBWeaponDesign : CachedWrapperBase<MBWeaponDesign, WeaponDesign>
  {
    public static implicit operator WeaponDesign(MBWeaponDesign wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBWeaponDesign(WeaponDesign obj) => MBWeaponDesign.GetWrapperFor(obj);
  }

  public class MBWeaponDesignList : List<MBWeaponDesign>
  {
    public static implicit operator List<WeaponDesign>(MBWeaponDesignList wrapperList) => wrapperList.Unwrap<MBWeaponDesign, WeaponDesign>();
    public static implicit operator MBWeaponDesignList(List<WeaponDesign> objectList) => (MBWeaponDesignList)objectList.Wrap<MBWeaponDesign, WeaponDesign>();
  }
}
