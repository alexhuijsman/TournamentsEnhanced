using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced
{
    public static class ItemExtensions
    {
        public static bool IsTierable(this ItemObject item)
        {
            return item.IsOfAnyMatchingType(
                ItemTypeEnum.Arrows, ItemTypeEnum.BodyArmor, ItemTypeEnum.Bow,
                ItemTypeEnum.Cape, ItemTypeEnum.ChestArmor, ItemTypeEnum.Crossbow,
                ItemTypeEnum.HandArmor, ItemTypeEnum.HeadArmor, ItemTypeEnum.HorseHarness,
                ItemTypeEnum.LegArmor, ItemTypeEnum.OneHandedWeapon, ItemTypeEnum.Polearm,
                ItemTypeEnum.Shield, ItemTypeEnum.Horse, ItemTypeEnum.Thrown, ItemTypeEnum.TwoHandedWeapon);
        }

        public static bool IsOfAnyMatchingType(this ItemObject item, params ItemTypeEnum[] matchingItemTypes)
        {
            var itemType = item.ItemType;
            foreach (var matchingItemType in matchingItemTypes)
            {
                if (itemType == matchingItemType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
