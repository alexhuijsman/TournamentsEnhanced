using System.Reflection;

namespace TournamentsEnhanced
{
  public static partial class Constants
  {
    public static class Settings
    {
      public static class Default
      {
        public const bool ProsperityNotification = true;
        public const bool PeaceNotification = true;
        public const bool SettlementStatNotification = true;
        public const float ProsperityIncrease = 100.00f;
        public const float LoyaltyIncrease = 10.00f;
        public const float SecurityIncrease = 10.00f;
        public const float FoodStocksDecrease = 20.00f;
        public const int NobleRelationModifier = 3;
        public const int TournamentInitialSpawnCount = 10;
        public const int TournamentSkillXP = 1500;
        public const float TournamentHitXP = 1f;
        public const float ArenaHitXP = 1f;
        public const bool VeryHardTournaments = true;
        public const int UpperBoundHeroesAdded = 10;
        public const int RenownReward = 10;
        public const bool BringCompanions = true;

        public const int MinDaysBetweenHostedTournaments = 7;
        public const int MinProsperityRequirement = 5000;
        public const int NoblesRelationIncrease = 3;
        public const float MaxRenownForInvitationTournaments = 800.00f;
        public const int NumberOfPrizesToChooseFrom = 5;
        public const int TournamentCost = 2000;
      }
    }
  }
}
