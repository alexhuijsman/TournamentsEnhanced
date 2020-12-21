using System.Collections.Generic;
using TaleWorlds.Core;
using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced
{
    public class ItemUtils
    {
        public static ItemObject RandomObject()
        {
            List<ItemObject> prizeItems = new List<ItemObject>();
            ItemTiers heroItemTier = HeroUtils.GetMainHeroTournamentRewardTier();

            foreach (var item in ItemObject.All)
            {
                if (!item.IsTierable() || item.Tier != heroItemTier)
                {
                    continue;
                }

                prizeItems.Add(item);
            }

            return prizeItems.IsEmpty() ? ItemObject.All.GetRandomElement() : prizeItems.GetRandomElement();
        }
    }
}
