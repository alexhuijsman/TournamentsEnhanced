using TaleWorlds.Core;

namespace TournamentsEnhanced.Wrappers.Core
{
  public struct MBItemRosterElement : IMBItemRosterElement
  {
    public ItemRosterElement UnwrappedStruct { get; set; }

    public MBEquipmentElement EquipmentElement => UnwrappedStruct.EquipmentElement;

    public int Amount => UnwrappedStruct.Amount;

    public bool IsEmpty => UnwrappedStruct.IsEmpty;

    public void Clear()
    {
      UnwrappedStruct.Clear();
    }

    public float GetRosterElementWeight()
    {
      return UnwrappedStruct.GetRosterElementWeight();
    }

    public static implicit operator ItemRosterElement(MBItemRosterElement wrapper) => wrapper.UnwrappedStruct;
    public static implicit operator MBItemRosterElement(ItemRosterElement unwrapped) => new MBItemRosterElement() { UnwrappedStruct = unwrapped };
  }
}
