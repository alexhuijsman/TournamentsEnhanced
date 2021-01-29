using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
  public static class MBTournamentParticipantExtensions
  {
    public static Settings Settings { get; set; } = Settings.Instance;

    public static bool IsMarriedToPlayer(this TournamentParticipant participant)
    {
      return participant.Character.HeroObject?.Spouse != null && participant.Character.HeroObject.Spouse == Hero.MainHero;
    }

    public static bool IsPlayerCompanion(this TournamentParticipant participant)
    {
      return participant.Character.IsHero && participant.Character.HeroObject.IsPlayerCompanion;
    }

    public static bool ShouldAddToPlayerTeam(this TournamentParticipant participant)
    {
      return Settings.BringCompanions && (participant.IsPlayerCompanion() || participant.IsMarriedToPlayer());
    }

    public static bool WasPreviouslyOnTeam(this TournamentParticipant participant, TournamentTeam team)
    {
      return participant.Team.TeamColor == team.TeamColor;
    }

    public static bool HasTeam(this TournamentParticipant participant)
    {
      return participant.Team != null;
    }

    public static bool TryPlaceInNewOrSameTeam(this TournamentParticipant participant, IEnumerable<TournamentTeam> teams)
    {
      var participantWasPlaced = false;

      foreach (TournamentTeam team in teams)
      {
        if (team.IsParticipantRequired() && (!participant.HasTeam() || participant.WasPreviouslyOnTeam(team)))
        {
          team.AddParticipant(participant);
          participantWasPlaced = true;
          break;
        }
      }

      return participantWasPlaced;
    }

    public static bool TryPlaceInAnyTeam(this TournamentParticipant participant, IEnumerable<TournamentTeam> teams)
    {
      var participantWasPlaced = false;

      foreach (TournamentTeam team in teams)
      {
        if (!team.IsParticipantRequired())
        {
          team.AddParticipant(participant);
          participantWasPlaced = true;
          break;
        }
      }

      return participantWasPlaced;
    }

    public static bool IsPlayerTroop(this TournamentParticipant participant)
    {
      return !participant.Character.IsHero || participant.Character.HeroObject == Hero.MainHero;
    }
  }
}
