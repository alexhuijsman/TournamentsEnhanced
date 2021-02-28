using System.Collections.Generic;
using TaleWorlds.Core;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Core;
using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced
{
  public static class MBItemObjectExtensions
  {
    public static Settings Settings { get; set; } = Settings.Instance;

    //TODO Move logic to ItemFinder
    public static List<MBItemObject> GetAvailableTournamentPrizes(this MBItemObject itemObject)
    {
      var availablePrizes = (List<MBItemObject>)MBItemObject.All.FindAll((MBItemObject item) => item.IsTournamentPrize());
      var selectedPrizes = (List<MBItemObject>)availablePrizes.FindAll((MBItemObject item) => item.IsWorthyTournamentPrizeForMainHero());
      availablePrizes.RemoveAll((prize) => selectedPrizes.Contains(prize));

      MBItemObject selectedPrize;
      while (selectedPrizes.Count < Settings.NumberOfPrizesToChooseFrom && !availablePrizes.IsEmpty())
      {
        selectedPrize = availablePrizes.GetRandomElement();
        selectedPrizes.Add(selectedPrize);
        availablePrizes.Remove(selectedPrize);
      }

      selectedPrizes.Shuffle();

      if (selectedPrizes.Count > Settings.NumberOfPrizesToChooseFrom)
      {
        selectedPrizes.RemoveRange(0, selectedPrizes.Count - Settings.NumberOfPrizesToChooseFrom);
      }

      return selectedPrizes;
    }

    public static bool IsWorthyTournamentPrizeForMainHero(this MBItemObject itemObject)
    {
      return itemObject.IsTournamentPrize() && itemObject.Tier == MBHero.Instance.GetMainHeroTournamentRewardTier();
    }

    public static bool IsTournamentPrize(this MBItemObject itemObject)
    {
      return !itemObject.IsCraftedByPlayer && !itemObject.IsCraftedWeapon && itemObject.IsTierable();
    }

    public static bool IsTierable(this MBItemObject itemObject)
    {
      return itemObject.IsOfAnyMatchingType(Constants.Item.TierableItemTypes);
    }

    public static bool IsOfAnyMatchingType(
      this MBItemObject itemObject,
      params ItemTypeEnum[] matchingItemTypes)
    {
      var actualItemType = itemObject.ItemType;
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
  }
}
