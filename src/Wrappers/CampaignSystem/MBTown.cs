using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Core;

using static TaleWorlds.CampaignSystem.SettlementComponent;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTown : CachedWrapperBase<MBTown, Town>, IMBTown
  {
    public static MBTownList AllFiefs { get; }
    public static MBTownList AllCastles { get; }
    public static MBTownList AllTownsWithoutTournaments => AllTowns;
    public static MBTownList AllTownsWithTournaments => (MBTownList)AllTowns.ToList().FindAll((town) => town.HasTournament);
    public static MBTownList AllTowns => Town.AllTowns.ToList();

    public int DaysAtUnrest => UnwrappedObject.DaysAtUnrest;

    public MBBuildingList Buildings => UnwrappedObject.Buildings.ToList();

    public int BoostBuildingProcess => UnwrappedObject.BoostBuildingProcess;

    public bool InRebelliousState => UnwrappedObject.InRebelliousState;

    public IMBFaction MapFaction => UnwrappedObject.MapFaction.ToIMBFaction();

    public float MilitiaChange => UnwrappedObject.MilitiaChange;

    public float Construction => UnwrappedObject.Construction;

    public MBClan OwnerClan { get => UnwrappedObject.OwnerClan; set => UnwrappedObject.OwnerClan = value; }
    public float Security { get => UnwrappedObject.Security; set => UnwrappedObject.Security = value; }
    public float Loyalty { get => UnwrappedObject.Loyalty; set => UnwrappedObject.Loyalty = value; }

    public MBWorkshopList Workshops => UnwrappedObject.Workshops.ToList();

    public MBBuilding CurrentBuilding => UnwrappedObject.CurrentBuilding;

    public bool IsUnderSiege => UnwrappedObject.IsUnderSiege;

    public MBBuilding CurrentDefaultBuilding => UnwrappedObject.CurrentDefaultBuilding;

    public int TradeTaxAccumulated { get => UnwrappedObject.TradeTaxAccumulated; set => UnwrappedObject.TradeTaxAccumulated = value; }
    public MBHero Governor { get => UnwrappedObject.Governor; set => UnwrappedObject.Governor = value; }
    public MBClan LastCapturedBy { get => UnwrappedObject.LastCapturedBy; set => UnwrappedObject.LastCapturedBy = value; }

    public MBVillageList Villages => UnwrappedObject.Villages.ToList();

    public MBMobileParty MilitiaParty => UnwrappedObject.MilitiaParty;

    public MBTownMarketData MarketData => UnwrappedObject.MarketData;

    public float SecurityChange => UnwrappedObject.SecurityChange;

    public float LoyaltyChange => UnwrappedObject.LoyaltyChange;

    public bool HasTournament => UnwrappedObject.HasTournament;

    public float FoodChange => UnwrappedObject.FoodChange;

    public int GarrisonChange => UnwrappedObject.GarrisonChange;

    public float ProsperityChange => UnwrappedObject.ProsperityChange;

    public MBCultureObject Culture => UnwrappedObject.Culture;

    public bool AfterSneakFight { get => UnwrappedObject.AfterSneakFight; set => UnwrappedObject.AfterSneakFight = value; }
    public int Gold => UnwrappedObject.Gold;

    public MBSettlement Settlement => UnwrappedObject.Settlement;

    public float FoodStocks { get => UnwrappedObject.FoodStocks; set => UnwrappedObject.FoodStocks = value; }

    public void ChangeGold(int amount) => UnwrappedObject.ChangeGold(amount);

    public int FoodStocksUpperLimit() => UnwrappedObject.FoodStocksUpperLimit();

    public float GetItemCategoryPriceIndex(MBItemCategory itemCategory, bool isSellingToTown = false) => UnwrappedObject.GetItemCategoryPriceIndex(itemCategory, isSellingToTown);

    public int GetItemPrice(MBEquipmentElement itemRosterElement, MBMobileParty tradingParty = null, bool isSelling = false) => UnwrappedObject.GetItemPrice(itemRosterElement, tradingParty, isSelling);

    public int GetItemPrice(MBItemObject item, MBMobileParty tradingParty = null, bool isSelling = false) => UnwrappedObject.GetItemPrice(item, tradingParty, isSelling);

    public ProsperityLevel GetProsperityLevel() => UnwrappedObject.GetProsperityLevel();

    public int GetWallLevel() => UnwrappedObject.GetWallLevel();

    public static implicit operator Town(MBTown wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTown(Town obj) => MBTown.GetWrapper(obj);
  }

  public class MBTownList : MBListBase<MBTown, MBTownList>
  {
    public MBTownList(params MBTown[] wrappers) : this((IEnumerable<MBTown>)wrappers) { }
    public MBTownList(IEnumerable<MBTown> wrappers) => AddRange(wrappers);
    public MBTownList(MBTown wrapper) => Add(wrapper);
    public MBTownList() { }

    public static implicit operator List<Town>(MBTownList wrapperList) => wrapperList.Unwrap<MBTown, Town>();
    public static implicit operator MBTownList(List<Town> objectList) => (MBTownList)objectList.Wrap<MBTown, Town>();
    public static implicit operator MBTown[](MBTownList wrapperList) => wrapperList.ToArray();
  }
}
