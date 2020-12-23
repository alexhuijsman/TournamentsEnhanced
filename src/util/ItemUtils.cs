using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced
{
    public class ItemUtils
    {
        public static IList<ItemObject> AllItems => new List<ItemObject>(AllItemsReadOnly);
        public static IList<ItemObject> AllItemsShuffled => AllItems.Shuffle();
        public static IReadOnlyList<ItemObject> AllItemsReadOnly => ItemObject.All;

        public static List<ItemObject> GetRandomlySelectedPrizeList()
        {
            var allItems = ItemUtils.AllItemsShuffled;
            var selectedItems = new List<ItemObject>();

            foreach (var item in allItems)
            {
                if (!item.IsWorthyTournamentPrizeForMainHero())
                {
                    continue;
                }

                selectedItems.Add(item);
            }

            if (selectedItems.Count == 0)
            {
                selectedItems.Add(AllItemsReadOnly.GetRandomElement());
            }

            return selectedItems;
        }
    }
}
