using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
    static class SettlementExtensions
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
            return settlement.OwnerClan.Leader.Equals(leader);
        }

        public static Hero Leader(this Settlement settlement)
        {
            return settlement.OwnerClan.Leader;
        }

    }
}
