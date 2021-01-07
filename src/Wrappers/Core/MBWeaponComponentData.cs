using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBWeaponComponentData : MBWrapperBase<MBWeaponComponentData, WeaponComponentData>
  {
    public static implicit operator WeaponComponentData(MBWeaponComponentData wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWeaponComponentData(WeaponComponentData obj) => MBWeaponComponentData.GetWrapperFor(obj);
  }

  public class MBWeaponComponentDataList : MBListBase<MBWeaponComponentData, MBWeaponComponentDataList>
  {
    public MBWeaponComponentDataList(params MBWeaponComponentData[] wrappers) : this((IEnumerable<MBWeaponComponentData>)wrappers) { }
    public MBWeaponComponentDataList(IEnumerable<MBWeaponComponentData> wrappers) => AddRange(wrappers);
    public MBWeaponComponentDataList(MBWeaponComponentData wrapper) => Add(wrapper);
    public MBWeaponComponentDataList() { }

    public static implicit operator List<WeaponComponentData>(MBWeaponComponentDataList wrapperList) => wrapperList.Unwrap<MBWeaponComponentData, WeaponComponentData>();
    public static implicit operator MBWeaponComponentDataList(List<WeaponComponentData> objectList) => (MBWeaponComponentDataList)objectList.Wrap<MBWeaponComponentData, WeaponComponentData>();
  }
}
