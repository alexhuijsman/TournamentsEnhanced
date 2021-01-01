using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBSettlement : CachedWrapperBase<MBSettlement, Settlement>
  {
    public static IReadOnlyList<MBSettlement> All => _allSettlements ?? (_allSettlements = WrapSettlements());
    public static List<MBSettlement> AllShuffled => All.ToList().Shuffle();
    public static MBSettlement CurrentSettlement => Settlement.CurrentSettlement;
    public static MBTown CurrentTown => CurrentSettlement.Town;

    public MBTown Town => UnwrappedObject.Town;
    public bool IsEligibleForProsperityTournament => UnwrappedObject.IsTown &&
                                                     !UnwrappedObject.Town.HasTournament &&
                                                      UnwrappedObject.Prosperity >= 5000.00f &&
                                                      UnwrappedObject.OwnerClan.Leader.Gold >= 10000;

    private static List<MBSettlement> _allSettlements;

    public Hero ClanLeader => UnwrappedObject.OwnerClan.Leader;
    public Hero FactionLeader => UnwrappedObject.MapFaction.Leader;

    public MBSettlement() : base() { }
    public MBSettlement(Settlement obj) : base(obj) { }
    private static List<MBSettlement> WrapSettlements()
    {
      var settlements = Settlement.All;
      var wrappedSettlements = new List<MBSettlement>(settlements.Count);

      foreach (var settlement in settlements)
      {
        wrappedSettlements.Add(new MBSettlement(settlement));
      }

      return wrappedSettlements;
    }

    public bool IsLedBy(Hero leader)
    {
      return ClanLeader.Equals(leader) || FactionLeader.Equals(leader);
    }

    public static implicit operator Settlement(MBSettlement wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSettlement(Settlement obj) => MBSettlement.GetWrapperFor(obj);
  }

  public class MBSettlementList : List<MBSettlement>
  {
    public static implicit operator List<Settlement>(MBSettlementList wrapperList) => wrapperList.Unwrap<MBSettlement, Settlement>();
    public static implicit operator MBSettlementList(List<Settlement> objectList) => (MBSettlementList)objectList.Wrap<MBSettlement, Settlement>();
  }
}
