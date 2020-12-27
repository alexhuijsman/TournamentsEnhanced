using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
    public class TournamentTracker
    {
        private static Dictionary<Town, TournamentRecord> Tournaments = new Dictionary<Town, TournamentRecord>();

        public static void AddByTownAndRecord(Town town, TournamentRecord record)
        {
            Tournaments.Add(town, record);
        }

        public static bool HasTournamentInTown(Town town)
        {
            return Tournaments.ContainsKey(town);
        }

        public static TournamentRecord GetByTown(Town town)
        {
            return Tournaments[town];
        }

        public static void RemoveByTown(Town town)
        {
            Tournaments.Remove(town);
        }
    }
}
