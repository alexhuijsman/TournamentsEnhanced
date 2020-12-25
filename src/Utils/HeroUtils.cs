using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using static TaleWorlds.Core.ItemObject;

namespace TournamentsEnhanced
{
    public class HeroUtils
    {
        public static ItemTiers GetMainHeroTournamentRewardTier()
        {
            return Hero.MainHero.GetTournamentRewardTier();
        }
    }
}
