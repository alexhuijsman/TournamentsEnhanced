using System.Collections.Generic;

using TaleWorlds.Core;

using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced.Wrappers
{
  public class MBItemObject : CachedWrapper<MBItemObject, ItemObject>
  {
    public static IReadOnlyList<MBItemObject> All => WrapAll();
    public static List<MBItemObject> AllShuffled => All.ToList().Shuffle();

    public MBItemObject() : base() { }
    public MBItemObject(ItemObject obj) : base(obj) { }

    private static List<MBItemObject> WrapAll()
    {
      var items = ItemObject.All;
      var wrappedItems = new List<MBItemObject>(items.Count);

      foreach (var item in items)
      {
        wrappedItems.Add(new MBItemObject(item));
      }

      return wrappedItems;
    }

    public static List<MBItemObject> GetAvailableTournamentPrizes()
    {
      var allItems = AllShuffled;
      var prizeItems = allItems.FindAll((MBItemObject item) => item.IsWorthyTournamentPrizeForMainHero());

      if (prizeItems.Count == 0)
      {
        prizeItems.Add(allItems.GetRandomElement());
      }

      return prizeItems;
    }

    public bool IsWorthyTournamentPrizeForMainHero()
    {
      return IsTierable() && UnwrappedObject.Tier == HeroUtils.GetMainHeroTournamentRewardTier();
    }

    public bool IsTierable()
    {
      return IsOfAnyMatchingType(ItemConstants.TierableItemTypes);
    }

    private bool IsOfAnyMatchingType(params ItemTypeEnum[] matchingItemTypes)
    {
      var actualItemType = UnwrappedObject.ItemType;
      var foundMatch = false;

      foreach (var matchingItemType in matchingItemTypes)
      {
        if (actualItemType == matchingItemType)
        {
          foundMatch = true;
          break;
        }
      }

      return foundMatch;
    }


    public static implicit operator ItemObject(MBItemObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemObject(ItemObject obj) => MBItemObject.GetWrapperFor(obj);

  }
}
