using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Localization;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBSettlement : MBObjectBaseWrapper<MBSettlement, Settlement>
  {
    public static MBSettlementList All => Settlement.All.ToList();
    public static MBSettlement CurrentSettlement => Settlement.CurrentSettlement;

    public static MBSettlement Find(string stringId) => Settlement.Find(stringId);

    public MBTown Town => UnwrappedObject.Town;
    public bool IsTown => UnwrappedObject.IsTown;
    public MBTextObject Name => UnwrappedObject.Name;
    public MBHeroList Notables => UnwrappedObject.Notables.ToList();
    public IMBFaction MapFaction => UnwrappedObject.MapFaction.ToIMBFaction();
    public MBClan OwnerClan => UnwrappedObject.OwnerClan;
    public float Prosperity { get => UnwrappedObject.Prosperity; set => UnwrappedObject.Prosperity = value; }

    public static MBSettlementList FindSettlementsAroundPosition(TaleWorlds.Library.Vec2 position, float radius, Func<Settlement, bool> condition = null) => (MBSettlementList)Settlement.FindSettlementsAroundPosition(position, radius, condition);

    public static implicit operator Settlement(MBSettlement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSettlement(Settlement obj) => MBSettlement.GetWrapper(obj);
  }

  public class MBSettlementList : MBListBase<MBSettlement, MBSettlementList>
  {
    public MBSettlementList(params MBSettlement[] wrappers) : this((IEnumerable<MBSettlement>)wrappers) { }
    public MBSettlementList(IEnumerable<MBSettlement> wrappers) => AddRange(wrappers);
    public MBSettlementList(MBSettlement wrapper) => Add(wrapper);
    public MBSettlementList() { }

    public static implicit operator List<Settlement>(MBSettlementList wrapperList) => wrapperList.Unwrap<MBSettlement, Settlement>();
    public static implicit operator MBSettlementList(List<Settlement> objectList) => (MBSettlementList)objectList.Wrap<MBSettlement, Settlement>();
    public static implicit operator MBSettlement[](MBSettlementList wrapperList) => wrapperList.ToArray();
  }
}
