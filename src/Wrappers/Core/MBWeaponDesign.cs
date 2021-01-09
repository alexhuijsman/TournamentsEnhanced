using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBWeaponDesign : MBWrapperBase<MBWeaponDesign, WeaponDesign>
  {
    public static implicit operator WeaponDesign(MBWeaponDesign wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWeaponDesign(WeaponDesign obj) => MBWeaponDesign.GetWrapper(obj);
  }

  public class MBWeaponDesignList : MBListBase<MBWeaponDesign, MBWeaponDesignList>
  {
    public MBWeaponDesignList(params MBWeaponDesign[] wrappers) : this((IEnumerable<MBWeaponDesign>)wrappers) { }
    public MBWeaponDesignList(IEnumerable<MBWeaponDesign> wrappers) => AddRange(wrappers);
    public MBWeaponDesignList(MBWeaponDesign wrapper) => Add(wrapper);
    public MBWeaponDesignList() { }

    public static implicit operator List<WeaponDesign>(MBWeaponDesignList wrapperList) => wrapperList.Unwrap<MBWeaponDesign, WeaponDesign>();
    public static implicit operator MBWeaponDesignList(List<WeaponDesign> objectList) => (MBWeaponDesignList)objectList.Wrap<MBWeaponDesign, WeaponDesign>();
    public static implicit operator MBWeaponDesign[](MBWeaponDesignList wrapperList) => wrapperList.ToArray();
  }
}
