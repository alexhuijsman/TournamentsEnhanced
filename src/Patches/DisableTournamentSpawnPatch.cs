﻿using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(TournamentCampaignBehavior), "ConsiderStartOrEndTournament")]
  class DisableTournamentSpawnPatch
  {
    static bool Prefix(Town town)
    {
      ITournamentManager tournamentManager = Campaign.Current.TournamentManager;
      TournamentGame tournamentGame = tournamentManager.GetTournamentGame(town);
      if (tournamentGame != null && MBRandom.RandomFloat < Campaign.Current.Models.TournamentModel.GetTournamentEndChance(tournamentGame))
      {
        tournamentManager.ResolveTournament(tournamentGame, tournamentGame.Town);
      }

      return false;
    }
  }
}
