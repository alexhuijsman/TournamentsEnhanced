using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace TournamentsEnhanced
{
    static class MBSettlementExtensions
    {
        public static bool IsEligibleForProsperityTournament(this Settlement settlement)
        {
            return settlement.IsTown &&
                !settlement.Town.HasTournament &&
                settlement.Prosperity >= 5000.00f &&
                settlement.OwnerClan.Leader.Gold >= 10000;
        }

        public static bool IsLedBy(this Settlement settlement, Hero leader)
        {
            return settlement.OwnerClan.Leader.Equals(leader) || settlement.MapFaction.Leader.Equals(leader);
        }

        public static Hero ClanLeader(this Settlement settlement)
        {
            return settlement.OwnerClan.Leader;
        }

        public static Hero FactionLeader(this Settlement settlement)
        {
            return settlement.MapFaction.Leader;
        }
    }
}
