using System.Collections.Generic;

using HarmonyLib;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.CampaignSystem;

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

      MBTournamentTeamList teams = __instance.Teams.ToList();
      var playerTeam = GetPlayerTeamFrom(teams);
      var nonPlayerTeams = teams.ToList();
      nonPlayerTeams.Remove(playerTeam);

      MBTournamentParticipant wrappedParticipant = participant;

      ____participants.Add(participant);

      if (playerTeam.IsParticipantRequired() &&
        (participant.IsPlayer || participant.IsPlayerCompanion() || participant.IsMarriedToPlayer() || participant.IsPlayerTroop()))
      {
        playerTeam.AddParticipant(participant);

        return false;
      }

      if ((firstTime && participant.TryPlaceInNewOrSameTeam((List<TournamentTeam>)teams)) || participant.TryPlaceInAnyTeam((List<TournamentTeam>)teams))
      {
        return false;
      }

      return false;
    }

    private static MBTournamentTeam GetPlayerTeamFrom(MBTournamentTeamList teams)
    {
      var tournamentRecord = ModState.TournamentRecords.ForCurrentTown();

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
        ModState.TournamentRecords.AddOrUpdate(tournamentRecord);
      }

      return playerTeam;
    }

    private static MBTournamentTeam GetTeamByColor(MBTournamentTeamList teams, uint playerTeamColor)
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

    private static MBTournamentTeam GetEmptyTeam(MBTournamentTeamList teams)
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
