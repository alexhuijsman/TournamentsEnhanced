using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced
{
    public static class ItemExtensions
    {
        public static bool IsWorthyTournamentPrizeForMainHero(this ItemObject item)
        {
            return item.IsTierable() && item.Tier == HeroUtils.GetMainHeroTournamentRewardTier();
        }

        public static bool IsTierable(this ItemObject item)
        {
            return item.IsOfAnyMatchingType(ItemConstants.TierableItemTypes);
        }

        private static bool IsOfAnyMatchingType(this ItemObject item, params ItemTypeEnum[] matchingItemTypes)
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
