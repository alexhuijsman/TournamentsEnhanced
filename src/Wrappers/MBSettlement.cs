using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBSettlement : CachedWrapperBase<MBSettlement, Settlement>
  {
    public static MBSettlementList All => Settlement.All.ToList();
    public static MBSettlement CurrentSettlement => Settlement.CurrentSettlement;
    public static MBTown CurrentTown => CurrentSettlement.Town;

    public MBTown Town => UnwrapedObject.Town;
    public bool IsTown => UnwrapedObject.IsTown;

    private static MBSettlementList _allSettlements;

    public Hero ClanLeader => UnwrapedObject.OwnerClan.Leader;
    public Hero FactionLeader => UnwrapedObject.MapFaction.Leader;

    public float Prosperity
    {
      get => UnwrapedObject.Prosperity;
      set => UnwrapedObject.Prosperity = value;
    }

    public bool IsLedBy(Hero leader)
    {
      return ClanLeader.Equals(leader) || FactionLeader.Equals(leader);
    }

    public static implicit operator Settlement(MBSettlement wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBSettlement(Settlement obj) => MBSettlement.GetWrapperFor(obj);
  }

  public class MBSettlementList : List<MBSettlement>
  {
    public static implicit operator List<Settlement>(MBSettlementList wrapperList) => wrapperList.Unwrap<MBSettlement, Settlement>();
    public static implicit operator MBSettlementList(List<Settlement> objectList) => (MBSettlementList)objectList.Wrap<MBSettlement, Settlement>();
  }
}
