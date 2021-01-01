using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(TournamentCampaignBehavior), "ConsiderStartOrEndTournament")]
  class DisableTournamentSpawnPatch
  {
    static bool Prefix(Town town)
    {
      MBTown wrappedTown = town;
      var tournamentManager = MBCampaign.Current.TournamentManager;
      MBTournamentGame tournamentGame = tournamentManager.GetTournamentGame(wrappedTown);
      if (tournamentGame != null && MBRandom.RandomFloat < MBCampaign.Current.Models.TournamentModel.GetTournamentEndChance(tournamentGame))
      {
        tournamentManager.ResolveTournament(tournamentGame, tournamentGame.Town);
      }

      return false;
    }
  }
}
