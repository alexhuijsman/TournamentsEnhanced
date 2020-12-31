using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace TournamentsEnhanced.Wrappers
{
  public class MBSettlement : CachedWrapper<MBSettlement, Settlement>
  {
    public static IReadOnlyList<MBSettlement> All => _allSettlements ?? (_allSettlements = WrapSettlements());
    public static List<MBSettlement> AllShuffled => All.ToList().Shuffle();
    public static MBSettlement CurrentSettlement => MBSettlement.GetWrapperFor(Settlement.CurrentSettlement);
    public static MBTown CurrentTown => CurrentSettlement.Town;

    private MBTown _town;
    public MBTown Town => _town ?? (_town = MBTown.GetWrapperFor(UnwrappedObject.Town));
    public bool IsEligibleForProsperityTournament => UnwrappedObject.IsTown &&
                                                     !UnwrappedObject.Town.HasTournament &&
                                                      UnwrappedObject.Prosperity >= 5000.00f &&
                                                      UnwrappedObject.OwnerClan.Leader.Gold >= 10000;

    private static List<MBSettlement> _allSettlements;

    public Hero ClanLeader => UnwrappedObject.OwnerClan.Leader;
    public Hero FactionLeader => UnwrappedObject.MapFaction.Leader;

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
  }
}
