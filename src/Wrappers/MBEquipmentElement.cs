using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBEquipmentElement : CachedWrapperBase<MBEquipmentElement, EquipmentElement>
  {
    public static implicit operator EquipmentElement(MBEquipmentElement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBEquipmentElement(EquipmentElement obj) => MBEquipmentElement.GetWrapperFor(obj);
  }
}
