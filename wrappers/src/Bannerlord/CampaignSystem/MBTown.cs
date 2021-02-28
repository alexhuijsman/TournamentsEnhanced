using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;

using static TaleWorlds.CampaignSystem.SettlementComponent;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTown : CachedWrapperBase<MBTown, Town>, IMBTown
  {
    public static List<MBTown> AllFiefs { get; }
    public static List<MBTown> AllCastles { get; }
    public static List<MBTown> AllTownsWithoutTournaments => AllTowns;
    public static List<MBTown> AllTownsWithTournaments => (List<MBTown>)AllTowns.ToList().FindAll((town) => town.HasTournament);
    public static List<MBTown> AllTowns => Town.AllTowns?.CastList<MBTown>();

    public virtual int DaysAtUnrest => UnwrappedObject.DaysAtUnrest;

    public virtual List<MBBuilding> Buildings => UnwrappedObject.Buildings.CastList<MBBuilding>();

    public virtual int BoostBuildingProcess => UnwrappedObject.BoostBuildingProcess;

    public virtual bool InRebelliousState => UnwrappedObject.InRebelliousState;

    public virtual IMBFaction MapFaction => UnwrappedObject.MapFaction.ToIMBFaction();

    public virtual float MilitiaChange => UnwrappedObject.MilitiaChange;

    public virtual float Construction => UnwrappedObject.Construction;

    public virtual MBClan OwnerClan { get => UnwrappedObject.OwnerClan; set => UnwrappedObject.OwnerClan = value; }
    public virtual float Security { get => UnwrappedObject.Security; set => UnwrappedObject.Security = value; }
    public virtual float Loyalty { get => UnwrappedObject.Loyalty; set => UnwrappedObject.Loyalty = value; }

    public virtual List<MBWorkshop> Workshops => UnwrappedObject.Workshops.CastList<MBWorkshop>();

    public virtual MBBuilding CurrentBuilding => UnwrappedObject.CurrentBuilding;

    public virtual bool IsUnderSiege => UnwrappedObject.IsUnderSiege;

    public virtual MBBuilding CurrentDefaultBuilding => UnwrappedObject.CurrentDefaultBuilding;

    public virtual int TradeTaxAccumulated { get => UnwrappedObject.TradeTaxAccumulated; set => UnwrappedObject.TradeTaxAccumulated = value; }
    public virtual MBHero Governor { get => UnwrappedObject.Governor; set => UnwrappedObject.Governor = value; }
    public virtual MBClan LastCapturedBy { get => UnwrappedObject.LastCapturedBy; set => UnwrappedObject.LastCapturedBy = value; }

    public virtual List<MBVillage> Villages => UnwrappedObject.Villages.CastList<MBVillage>();

    public virtual MBTownMarketData MarketData => UnwrappedObject.MarketData;

    public virtual float SecurityChange => UnwrappedObject.SecurityChange;

    public virtual float LoyaltyChange => UnwrappedObject.LoyaltyChange;

    public virtual bool HasTournament => UnwrappedObject.HasTournament;

    public virtual float FoodChange => UnwrappedObject.FoodChange;

    public virtual int GarrisonChange => UnwrappedObject.GarrisonChange;

    public virtual float ProsperityChange => UnwrappedObject.ProsperityChange;

    public virtual MBCultureObject Culture => UnwrappedObject.Culture;

    public virtual bool AfterSneakFight { get => UnwrappedObject.AfterSneakFight; set => UnwrappedObject.AfterSneakFight = value; }
    public virtual int Gold => UnwrappedObject.Gold;

    public virtual MBSettlement Settlement => UnwrappedObject.Settlement;

    public virtual float FoodStocks { get => UnwrappedObject.FoodStocks; set => UnwrappedObject.FoodStocks = value; }

    public virtual void ChangeGold(int amount) => UnwrappedObject.ChangeGold(amount);

    public virtual int FoodStocksUpperLimit() => UnwrappedObject.FoodStocksUpperLimit();

    public virtual float GetItemCategoryPriceIndex(MBItemCategory itemCategory, bool isSellingToTown = false) => UnwrappedObject.GetItemCategoryPriceIndex(itemCategory, isSellingToTown);

    public virtual int GetItemPrice(MBEquipmentElement itemRosterElement, MBMobileParty tradingParty = null, bool isSelling = false) => UnwrappedObject.GetItemPrice(itemRosterElement, tradingParty, isSelling);

    public virtual int GetItemPrice(MBItemObject item, MBMobileParty tradingParty = null, bool isSelling = false) => UnwrappedObject.GetItemPrice(item, tradingParty, isSelling);

    public virtual ProsperityLevel GetProsperityLevel() => UnwrappedObject.GetProsperityLevel();

    public virtual int GetWallLevel() => UnwrappedObject.GetWallLevel();

    public static implicit operator Town(MBTown wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTown(Town obj) => GetWrapper(obj);
  }
}
