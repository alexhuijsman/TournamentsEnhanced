using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace TournamentsEnhanced
{
    static class MBTownExtensions
    {
        private const int RELATIONSHIP_MODIFIER = 3;

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

        public static void ApplyTournamentCreationEffects(this Settlement settlement)
        {
            settlement.Prosperity += Settings.Instance.ProsperityIncrease;
            settlement.Town.Loyalty += Settings.Instance.LoyaltyIncrease;
            settlement.Town.Security += Settings.Instance.SecurityIncrease;
            settlement.Town.FoodStocks -= Settings.Instance.FoodStocksDecrease;
            settlement.OwnerClan.Leader.ChangeHeroGold(-Settings.Instance.TournamentCost);

            if (settlement.MapFaction.Leader.IsHumanPlayerCharacter && Settings.Instance.SettlementStatNotification)
            {
                NotificationUtils.DisplayMessage(settlement.Town.Name + "'s prosperity, loyalty and security have increased and food stocks have decreased");
            }
        }

        public static void ApplyHostedTournamentRelationsGain(this Settlement settlement)
        {
            var notables = settlement.Notables;
            foreach (var notable in notables)
            {
                if (!notable.Name.Equals(settlement.OwnerClan.Leader.Name))
                {
                    notable.SetPersonalRelation(settlement.OwnerClan.Leader, notable.GetRelation(settlement.OwnerClan.Leader) + RELATIONSHIP_MODIFIER);
                }
            }

            if (settlement.OwnerClan.Leader.IsHumanPlayerCharacter)
            {
                NotificationUtils.DisplayBannerMessage("Your relationship with local notables at " + settlement.Town.Name + " has improved");
            }
        }
    }
}
