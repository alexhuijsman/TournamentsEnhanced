using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.Core;

using static TaleWorlds.CampaignSystem.SettlementComponent;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public interface IMBTown
  {
    int DaysAtUnrest { get; }
    List<MBBuilding> Buildings { get; }
    int BoostBuildingProcess { get; }
    bool InRebelliousState { get; }
    IMBFaction MapFaction { get; }
    float MilitiaChange { get; }
    float Construction { get; }
    MBClan OwnerClan { get; set; }
    float Security { get; set; }
    float Loyalty { get; set; }
    List<MBWorkshop> Workshops { get; }
    MBBuilding CurrentBuilding { get; }
    bool IsUnderSiege { get; }
    MBBuilding CurrentDefaultBuilding { get; }
    int TradeTaxAccumulated { get; set; }
    MBHero Governor { get; set; }
    MBClan LastCapturedBy { get; set; }
    List<MBVillage> Villages { get; }
    MBMobileParty MilitiaParty { get; }
    MBTownMarketData MarketData { get; }
    float SecurityChange { get; }
    float LoyaltyChange { get; }
    bool HasTournament { get; }
    float FoodChange { get; }
    int GarrisonChange { get; }
    float ProsperityChange { get; }
    MBCultureObject Culture { get; }
    bool AfterSneakFight { get; set; }

    int FoodStocksUpperLimit();
    float GetItemCategoryPriceIndex(MBItemCategory itemCategory, bool isSellingToTown = false);
    int GetItemPrice(MBEquipmentElement itemRosterElement, MBMobileParty tradingParty = null, bool isSelling = false);
    int GetItemPrice(MBItemObject item, MBMobileParty tradingParty = null, bool isSelling = false);
    ProsperityLevel GetProsperityLevel();
    int GetWallLevel();

  }
}