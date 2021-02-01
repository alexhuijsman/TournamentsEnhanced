using TaleWorlds.Core;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.Core
{
  public struct MBEquipmentElement : IMBEquipmentElement
  {
    public EquipmentElement UnwrappedStruct { get; set; }

    public bool IsEmpty => UnwrappedStruct.IsEmpty;

    public MBItemModifier ItemModifier => UnwrappedStruct.ItemModifier;

    public MBItemObject Item => UnwrappedStruct.Item;

    public int ItemValue => UnwrappedStruct.ItemValue;

    public float Weight => UnwrappedStruct.Weight;

    public void Clear()
    {
      UnwrappedStruct.Clear();
    }

    public bool Equals(MBItemRosterElement other)
    {
      return UnwrappedStruct.Equals(other);
    }

    public int GetBaseValue()
    {
      return UnwrappedStruct.GetBaseValue();
    }

    public float GetEquipmentElementWeight()
    {
      return UnwrappedStruct.GetEquipmentElementWeight();
    }

    public int GetModifiedArmArmor()
    {
      return UnwrappedStruct.GetModifiedArmArmor();
    }

    public int GetModifiedBodyArmor()
    {
      return UnwrappedStruct.GetModifiedBodyArmor();
    }

    public int GetModifiedHandlingForUsage(int usageIndex)
    {
      return UnwrappedStruct.GetModifiedHandlingForUsage(usageIndex);
    }

    public int GetModifiedHeadArmor()
    {
      return UnwrappedStruct.GetModifiedHeadArmor();
    }

    public MBTextObject GetModifiedItemName()
    {
      return UnwrappedStruct.GetModifiedItemName();
    }

    public int GetModifiedLegArmor()
    {
      return UnwrappedStruct.GetModifiedLegArmor();
    }

    public short GetModifiedMaximumHitPointsForUsage(int usageIndex)
    {
      return UnwrappedStruct.GetModifiedMaximumHitPointsForUsage(usageIndex);
    }

    public int GetModifiedMissileDamageForUsage(int usageIndex)
    {
      return UnwrappedStruct.GetModifiedMissileDamageForUsage(usageIndex);
    }

    public int GetModifiedMissileSpeedForUsage(int usageIndex)
    {
      return UnwrappedStruct.GetModifiedMissileSpeedForUsage(usageIndex);
    }

    public int GetModifiedMountBodyArmor()
    {
      return UnwrappedStruct.GetModifiedMountBodyArmor();
    }

    public int GetModifiedMountCharge(in MBEquipmentElement harness)
    {
      return UnwrappedStruct.GetModifiedMountCharge(harness);
    }

    public int GetModifiedMountHitPoints()
    {
      return UnwrappedStruct.GetModifiedMountHitPoints();
    }

    public int GetModifiedMountManeuver(in MBEquipmentElement harness)
    {
      return UnwrappedStruct.GetModifiedMountManeuver(harness);
    }

    public int GetModifiedMountSpeed(in MBEquipmentElement harness)
    {
      return UnwrappedStruct.GetModifiedMountSpeed(harness);
    }

    public short GetModifiedStackCountForUsage(int usageIndex)
    {
      return UnwrappedStruct.GetModifiedStackCountForUsage(usageIndex);
    }

    public int GetModifiedSwingDamageForUsage(int usageIndex)
    {
      return UnwrappedStruct.GetModifiedSwingDamageForUsage(usageIndex);
    }

    public int GetModifiedSwingSpeedForUsage(int usageIndex)
    {
      return UnwrappedStruct.GetModifiedSwingSpeedForUsage(usageIndex);
    }

    public int GetModifiedThrustDamageForUsage(int usageIndex)
    {
      return UnwrappedStruct.GetModifiedThrustDamageForUsage(usageIndex);
    }

    public int GetModifiedThrustSpeedForUsage(int usageIndex)
    {
      return UnwrappedStruct.GetModifiedThrustSpeedForUsage(usageIndex);
    }

    public bool IsEqualTo(MBEquipmentElement other)
    {
      return UnwrappedStruct.IsEqualTo(other);
    }

    public void SetModifier(MBItemModifier itemModifier)
    {
      UnwrappedStruct.SetModifier(itemModifier);
    }

    public static implicit operator EquipmentElement(MBEquipmentElement wrapper) => wrapper.UnwrappedStruct;
    public static implicit operator MBEquipmentElement(EquipmentElement unwrapped) => new MBEquipmentElement() { UnwrappedStruct = unwrapped };
  }
}
