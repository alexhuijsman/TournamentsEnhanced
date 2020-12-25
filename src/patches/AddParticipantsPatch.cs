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
      ____participants.Add(participant);

      var town = Hero.MainHero.CurrentSettlement?.Town;

      if (town == null || !TournamentTracker.HasTournamentInTown(town))
      {
        return false;
      }

      var tournamentRecord = TournamentTracker.GetByTown(town);
      TournamentTeam playerTeam = tournamentRecord.playerTeam;

      if (playerTeam == null)
      {
        playerTeam = GetEmptyTeamFromTeams(__instance.Teams);
        tournamentRecord.playerTeam = playerTeam;
      }

      if (firstTime && Settings.Instance.BringCompanions && playerTeam.IsParticipantRequired())
      {

      }

      {
        AssignCompanionsToTeam(playerTeam);

        foreach (var team in __instance.Teams)
        {
          if (Settings.Instance.BringCompanions &&
            playerTeam != null &&
            playerTeam.TeamColor == team.TeamColor &&
            team.IsParticipantRequired() &&
            participant.Character.IsHero &&
            (participant.Character.IsPlayerCharacter || participant.Character.HeroObject.IsPlayerCompanion ||
              (participant.Character.HeroObject.Spouse != null && participant.Character.HeroObject.Spouse.Name.Equals(Hero.MainHero.Name))))
          {
            team.AddParticipant(participant);
            return false;
          }
          else if (Settings.Instance.BringCompanions &&
            tournamentRecord.playerTeam != null &&
            tournamentRecord.playerTeam.TeamColor == team.TeamColor &&
            !team.IsParticipantRequired() &&
            participant.Character.IsHero &&
            participant.Character.IsPlayerCharacter)
          {
            NotificationUtils.DisplayMessage("player could not join full team");
          }
        }

      }

      foreach (TournamentTeam tournamentTeam in __instance.Teams)
      {
        if (firstTime && tournamentRecord != null && tournamentTeam.TeamColor == tournamentRecord.playerTeam.TeamColor)
        {
          continue;
        }
        if (tournamentTeam.IsParticipantRequired() && ((participant.Team != null && participant.Team.TeamColor == tournamentTeam.TeamColor) || firstTime))
        {
          tournamentTeam.AddParticipant(participant);
          return false;
        }
      }
      foreach (TournamentTeam tournamentTeam2 in __instance.Teams)
      {
        if (tournamentTeam2.IsParticipantRequired())
        {
          tournamentTeam2.AddParticipant(participant);
          break;
        }
      }

      TournamentTracker.UpdateRecord(tournamentRecord);
      return false;
    }

    private static void AssignParticipantToTeam(TournamentParticipant participant, TournamentTeam team)
    {
      {
        if (Settings.Instance.BringCompanions &&
          tournamentRecord.playerTeam != null &&
          tournamentRecord.playerTeam.TeamColor == team.TeamColor &&
          team.IsParticipantRequired() &&
          participant.Character.IsHero &&
          (participant.Character.IsPlayerCharacter || participant.Character.HeroObject.IsPlayerCompanion ||
            (participant.Character.HeroObject.Spouse != null && participant.Character.HeroObject.Spouse.Name.Equals(Hero.MainHero.Name))))
        {
          team.AddParticipant(participant);
          return false;
        }
        else if (Settings.Instance.BringCompanions &&
          tournamentRecord.playerTeam != null &&
          tournamentRecord.playerTeam.TeamColor == team.TeamColor &&
          !team.IsParticipantRequired() &&
          participant.Character.IsHero &&
          participant.Character.IsPlayerCharacter)
        {
          NotificationUtils.DisplayMessage("player could not join full team");
        }
      }
    }

    private static TournamentTeam GetEmptyTeamFromTeams(IEnumerable<TournamentTeam> teams)
    {
      foreach (var team in teams)
        if (team.Participants.IsEmpty())
        {
          return team;
        }

      return null;
    }
  }
}
