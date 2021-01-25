using System.Collections.Generic;
using TaleWorlds.Core;
using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Library;
using TournamentsEnhanced.Wrappers.Localization;
using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced.Wrappers.Core
{
  public partial class MBItemObject : MBObjectBaseWrapper<MBItemObject, ItemObject>, IMBItemObject
  {
    public static List<MBItemObject> GetAvailableTournamentPrizes()
    {
      var availablePrizes = (List<MBItemObject>)All.FindAll((MBItemObject item) => item.IsTournamentPrize());
      var selectedPrizes = (List<MBItemObject>)availablePrizes.FindAll((MBItemObject item) => item.IsWorthyTournamentPrizeForMainHero());
      availablePrizes.RemoveAll((prize) => selectedPrizes.Contains(prize));

      MBItemObject selectedPrize;
      while (selectedPrizes.Count < Settings.Instance.NumberOfPrizesToChooseFrom && !availablePrizes.IsEmpty())
      {
        selectedPrize = availablePrizes.GetRandomElement();
        selectedPrizes.Add(selectedPrize);
        availablePrizes.Remove(selectedPrize);
      }

      selectedPrizes.Shuffle();

      if (selectedPrizes.Count > Settings.Instance.NumberOfPrizesToChooseFrom)
      {
        selectedPrizes.RemoveRange(0, selectedPrizes.Count - Settings.Instance.NumberOfPrizesToChooseFrom);
      }

      return selectedPrizes;
    }

    public bool IsWorthyTournamentPrizeForMainHero()
    {
      return IsTournamentPrize() && UnwrappedObject.Tier == MBHero.GetMainHeroTournamentRewardTier();
    }

    public bool IsTournamentPrize()
    {
      return !IsCraftedByPlayer && !IsCraftedWeapon && IsTierable();
    }

    public bool IsTierable()
    {
      return IsOfAnyMatchingType(Constants.Item.TierableItemTypes);
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
  }
}
