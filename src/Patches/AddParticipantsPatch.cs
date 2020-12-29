using System;
using System.Collections.Generic;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(TournamentMatch), "AddParticipant")]
  class AddParticipantsPatch
  {

    static bool Prefix(ref List<TournamentParticipant> ____participants, TournamentParticipant participant, bool firstTime, TournamentMatch __instance)
    {
      if (!__instance.IsPlayerParticipating())
      {
        return true;
      }

      var teams = __instance.Teams;
      var playerTeam = GetPlayerTeam(teams);
      var nonPlayerTeams = teams.AllExcept(playerTeam);

      ____participants.Add(participant);

      if (playerTeam.IsParticipantRequired() &&
        (participant.IsPlayer || participant.IsPlayerCompanion() || participant.IsMarriedToPlayer() || participant.IsPlayerTroop()))
      {
        playerTeam.AddParticipant(participant);
        return false;
      }

      if ((firstTime && participant.TryPlaceInNewOrSameTeam(teams)) || participant.TryPlaceInAnyTeam(teams))
      {
        return false;
      }

      return false;
    }

    private static TournamentTeam GetPlayerTeam(IEnumerable<TournamentTeam> teams)
    {
      var tournamentRecord = TournamentRecords.GetRecordForCurrentTown();

      TournamentTeam playerTeam;
      if (tournamentRecord.HasPlayerTeam)
      {
        playerTeam = GetTeamByColor(teams, tournamentRecord.playerTeamColor);
      }
      else
      {
        playerTeam = GetEmptyTeam(teams);
        tournamentRecord.playerTeamColor = playerTeam.TeamColor;
        tournamentRecord.HasPlayerTeam = true;
        TournamentRecords.UpdateRecordForCurrentTown(tournamentRecord);
      }

      return playerTeam;
    }

    private static TournamentTeam GetTeamByColor(IEnumerable<TournamentTeam> teams, uint playerTeamColor)
    {
      TournamentTeam matchingTeam = null;
      foreach (var team in teams)
      {
        if (team.TeamColor == playerTeamColor)
        {
          matchingTeam = team;
          break;
        }
      }

      return matchingTeam;
    }

    private static TournamentTeam GetEmptyTeam(IEnumerable<TournamentTeam> teams)
    {
      TournamentTeam emptyTeam = null;
      foreach (var team in teams)
        if (team.Participants.IsEmpty())
        {
          emptyTeam = team;
          break;
        }

      return emptyTeam;
    }
  }
}
