using TaleWorlds.CampaignSystem;
using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced
{
    public static class MBHeroExtensions
    {
        public static ItemTiers GetTournamentRewardTier(this Hero hero)
        {
            ItemTiers itemTier;
            if (Hero.MainHero.Clan.Renown <= 300)
            {
                itemTier = ItemTiers.Tier4;
            }
            else if (Hero.MainHero.Clan.Renown <= 600)
            {
                itemTier = ItemTiers.Tier5;
            }
            else
            {
                itemTier = ItemTiers.Tier6;
            }

            return itemTier;
        }
    }
}
