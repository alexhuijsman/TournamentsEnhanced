using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
    public static class MainHero
    {
        public static Town CurrentTown => Hero.MainHero.CurrentSettlement?.Town;
    }
}
