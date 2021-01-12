using System.Collections.Generic;

using HarmonyLib;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Patches
{
  [HarmonyPatch(typeof(TournamentMatch), "AddParticipant")]
  class AddParticipantsPatch
  {
    public static ModState ModState { protected get; set; } = ModState.Instance;

    static bool Prefix(ref List<TournamentParticipant> ____participants, TournamentParticipant participant, bool firstTime, TournamentMatch __instance)
    {
      if (!__instance.IsPlayerParticipating())
      {
        return true;
      }

      var teams = __instance.Teams.CastList<MBTournamentTeam>();
      var playerTeam = GetPlayerTeamFrom(teams);
      var nonPlayerTeams = teams;
      nonPlayerTeams.Remove(playerTeam);

      MBTournamentParticipant wrappedParticipant = participant;

      ____participants.Add(participant);

      if (playerTeam.IsParticipantRequired() &&
        (participant.IsPlayer || participant.IsPlayerCompanion() || participant.IsMarriedToPlayer() || participant.IsPlayerTroop()))
      {
        playerTeam.AddParticipant(participant);

        return false;
      }

      if ((firstTime && participant.TryPlaceInNewOrSameTeam(teams.CastList<TournamentTeam>())) || participant.TryPlaceInAnyTeam(teams.CastList<TournamentTeam>()))
      {
        return false;
      }

      return false;
    }

    private static MBTournamentTeam GetPlayerTeamFrom(IEnumerable<MBTournamentTeam> teams)
    {
      var tournamentRecord = ModState.TournamentRecords.ForCurrentSettlement();

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

    private static MBTournamentTeam GetTeamByColor(IEnumerable<MBTournamentTeam> teams, uint playerTeamColor)
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
