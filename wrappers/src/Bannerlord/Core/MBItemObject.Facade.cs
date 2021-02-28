using System.Collections.Generic;
using TaleWorlds.Core;
using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced.Wrappers.Core
{
  public partial class MBItemObject : MBObjectBaseWrapper<MBItemObject, ItemObject>, IMBItemObject
  {
    protected MBHero MBHero { get; set; } = MBHero.Instance;

    protected static Settings Settings { get; set; } = Settings.Instance;

    //TODO Move logic to ItemFinder
    public static List<MBItemObject> GetAvailableTournamentPrizes()
    {
      var availablePrizes = (List<MBItemObject>)All.FindAll((MBItemObject item) => item.IsTournamentPrize());
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

    public virtual bool IsWorthyTournamentPrizeForMainHero()
    {
      return IsTournamentPrize() && UnwrappedObject.Tier == MBHero.GetMainHeroTournamentRewardTier();
    }

    public virtual bool IsTournamentPrize()
    {
      return !IsCraftedByPlayer && !IsCraftedWeapon && IsTierable();
    }

    public virtual bool IsTierable()
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
