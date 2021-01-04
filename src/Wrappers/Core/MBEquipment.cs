using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBEquipment : MBWrapperBase<MBEquipment, Equipment>
  {
    public static implicit operator  Equipment(MBEquipment wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBEquipment(Equipment obj) => MBEquipment.GetWrapperFor(obj);
  }

  public class MBEquipmentList : MBListBase<MBEquipment,MBEquipmentList>
  {
    public static implicit operator List<Equipment>(MBEquipmentList wrapperList) => wrapperList.Unwrap<MBEquipment, Equipment>();
    public static implicit operator MBEquipmentList(List<Equipment> objectList) => (MBEquipmentList)objectList.Wrap<MBEquipment, Equipment>();
  }
}
