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
    public static MBTown CurrentTown => CurrentSettlement.Town;
    public MBTown Town => UnwrappedObject.Town;
    public bool IsTown => UnwrappedObject.IsTown;
    public MBTextObject Name => UnwrappedObject.Name;

    private static MBSettlementList _allSettlements;

    public Hero ClanLeader => UnwrappedObject.OwnerClan.Leader;
    public Hero FactionLeader => UnwrappedObject.MapFaction.Leader;

    public float Prosperity
    {
      get => UnwrappedObject.Prosperity;
      set => UnwrappedObject.Prosperity = value;
    }

    public bool IsLedBy(Hero leader)
    {
      return ClanLeader.Equals(leader) || FactionLeader.Equals(leader);
    }

    public static implicit operator Settlement(MBSettlement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSettlement(Settlement obj) => MBSettlement.GetWrapperFor(obj);
  }

  public class MBSettlementList : MBListBase<MBSettlement, MBSettlementList>
  {
    public static implicit operator List<Settlement>(MBSettlementList wrapperList) => wrapperList.Unwrap<MBSettlement, Settlement>();
    public static implicit operator MBSettlementList(List<Settlement> objectList) => (MBSettlementList)objectList.Wrap<MBSettlement, Settlement>();
  }
}
