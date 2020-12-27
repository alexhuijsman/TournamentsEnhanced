using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
    public static class MBTournamentFacade
    {
        public static TournamentCreationResult TryMakePeaceTournamentForFaction(IFaction faction)
        {
            var findHostTownResult = MBSettlementFacade.FindHostTownForFaction(faction, FindTownOptions.UseExistingTournamentAsLastResort);

            if (findHostTownResult.Failed)
            {
                return TournamentCreationResult.Failure();
            }

            return PreparePeaceTournamentForTown(findHostTownResult.Town);
        }

        private static TournamentCreationResult PreparePeaceTournamentForTown(Town town)
        {
            var townHasExistingTournament = town.HasTournament;

            if (!townHasExistingTournament)
            {
                InstantiateTournamentForTown(town);
            }
            else
            {
                TournamentTracker.AddByTownAndRecord(town, new TournamentRecord() { type = TournamentType.Peace });

                town.ApplyTournamentCreationEffects();
            }

            return TournamentCreationResult.Success(town, townHasExistingTournament);
        }

        private static void InstantiateTournamentForTown(Town town)
        {
            var tournament = new FightTournamentGame(town);
            Campaign.Current.TournamentManager.AddTournament(tournament);
        }
    }
}
