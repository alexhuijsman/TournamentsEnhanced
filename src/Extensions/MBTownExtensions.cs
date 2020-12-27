using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace TournamentsEnhanced
{
    static class MBTownExtensions
    {

        public static void ApplyTournamentCreationEffects(this Town town)
        {
            town.Settlement.Prosperity += Settings.Instance.ProsperityIncrease;
            town.Loyalty += Settings.Instance.LoyaltyIncrease;
            town.Security += Settings.Instance.SecurityIncrease;
            town.FoodStocks -= Settings.Instance.FoodStocksDecrease;
            town.OwnerClan.Leader.ChangeHeroGold(-Settings.Instance.TournamentCost);

            if (town.MapFaction.Leader.IsHumanPlayerCharacter && Settings.Instance.SettlementStatNotification)
            {
                NotificationUtils.DisplayMessage(town.Name + "'s prosperity, loyalty and security have increased and food stocks have decreased");
            }
        }

        public static void ApplyHostedTournamentRelationsGain(this Town town)
        {
            var notables = town.Settlement.Notables;
            foreach (var notable in notables)
            {
                if (!notable.Name.Equals(town.OwnerClan.Leader.Name))
                {
                    notable.SetPersonalRelation(town.OwnerClan.Leader, notable.GetRelation(town.OwnerClan.Leader) + TournamentConstants.HostedTournamentEffects.NobleRelationshipModifier);
                }
            }

            if (town.OwnerClan.Leader.IsHumanPlayerCharacter)
            {
                NotificationUtils.DisplayBannerMessage("Your relationship with local notables at " + town.Name + " has improved");
            }
        }
    }
}
