using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Patches
{
  [HarmonyPatch(typeof(TournamentCampaignBehavior), "ConsiderStartOrEndTournament")]
  public class DisableTournamentSpawnPatch
  {
    protected static MBCampaign MBCampaign { get; set; } = MBCampaign.Instance;

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
