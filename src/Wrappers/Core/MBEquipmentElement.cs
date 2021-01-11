using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBEquipmentElement : MBWrapperBase<MBEquipmentElement, EquipmentElement>
  {
    public static implicit operator EquipmentElement(MBEquipmentElement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBEquipmentElement(EquipmentElement obj) => MBEquipmentElement.GetWrapper(obj);
  }
}
