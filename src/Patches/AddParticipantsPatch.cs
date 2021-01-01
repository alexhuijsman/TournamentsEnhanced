using System.Collections.Generic;

using HarmonyLib;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers;

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

      var teams = __instance.Teams.Wrap<MBTournamentTeam, TournamentTeam>();
      var playerTeam = GetPlayerTeamFrom(teams);
      var nonPlayerTeams = new List<MBTournamentTeam>(teams).Remove(playerTeam);
      var wrappedParticipant = participant.Wrap();

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

    private static MBTournamentTeam GetPlayerTeamFrom(IEnumerable<MBTournamentTeam> teams)
    {
      var tournamentRecord = TournamentRecords.GetForCurrentTown();

      MBTournamentTeam playerTeam;
      if (tournamentRecord.HasPlayerTeam)
      {
        playerTeam = GetTeamByColor(teams, tournamentRecord.playerTeamColor);
      }
      else
      {
        playerTeam = GetEmptyTeam(teams);
        tournamentRecord.playerTeamColor = playerTeam.TeamColor;
        tournamentRecord.HasPlayerTeam = true;
        TournamentRecords.AddOrUpdateForCurrentTown(tournamentRecord);
      }

      return playerTeam;
    }

    private static MBTournamentTeam GetTeamByColor(IEnumerable<TournamentTeam> teams, uint playerTeamColor)
    {
      MBTournamentTeam matchingTeam = null;
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

    private static MBTournamentTeam GetEmptyTeam(IEnumerable<MBTournamentTeam> teams)
    {
      MBTournamentTeam emptyTeam = null;
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
