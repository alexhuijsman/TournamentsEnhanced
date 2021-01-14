using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.Core
{
  public interface IMBEquipmentElement : IStructWrapperBase<EquipmentElement>
  {
    bool IsEmpty { get; }
    MBItemModifier ItemModifier { get; }
    MBItemObject Item { get; }
    int ItemValue { get; }
    float Weight { get; }

    void Clear();
    bool Equals(MBItemRosterElement other);
    int GetBaseValue();
    float GetEquipmentElementWeight();
    int GetModifiedArmArmor();
    int GetModifiedBodyArmor();
    int GetModifiedHandlingForUsage(int usageIndex);
    int GetModifiedHeadArmor();
    MBTextObject GetModifiedItemName();
    int GetModifiedLegArmor();
    short GetModifiedMaximumHitPointsForUsage(int usageIndex);
    int GetModifiedMissileDamageForUsage(int usageIndex);
    int GetModifiedMissileSpeedForUsage(int usageIndex);
    int GetModifiedMountBodyArmor();
    int GetModifiedMountCharge(in MBEquipmentElement harness);
    int GetModifiedMountHitPoints();
    int GetModifiedMountManeuver(in MBEquipmentElement harness);
    int GetModifiedMountSpeed(in MBEquipmentElement harness);
    short GetModifiedStackCountForUsage(int usageIndex);
    int GetModifiedSwingDamageForUsage(int usageIndex);
    int GetModifiedSwingSpeedForUsage(int usageIndex);
    int GetModifiedThrustDamageForUsage(int usageIndex);
    int GetModifiedThrustSpeedForUsage(int usageIndex);
    bool IsEqualTo(MBEquipmentElement other);
    void SetModifier(MBItemModifier itemModifier);
  }
}