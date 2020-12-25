using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(TournamentMatch), "AddParticipant")]
  class AddParticipantsPatch
  {

    static bool Prefix(ref List<TournamentParticipant> ____participants, TournamentParticipant participant, bool firstTime, TournamentMatch __instance)
    {
      ____participants.Add(participant);
      if (firstTime && TournamentKB.GetTournamentKB(Hero.MainHero.CurrentSettlement) != null)
      {
        bool teamselected = false;
        foreach (TournamentTeam tournamentTeam3 in __instance.Teams)
        {
          if (tournamentTeam3.Participants.IsEmpty() && !teamselected)
          {
            TournamentKB.GetTournamentKB(Hero.MainHero.CurrentSettlement).playerTeam = tournamentTeam3;
            teamselected = true;
          }
          if (TournamentsEnhancedSettings.Instance.BringCompanions &&
            TournamentKB.GetTournamentKB(Hero.MainHero.CurrentSettlement).playerTeam != null &&
            TournamentKB.GetTournamentKB(Hero.MainHero.CurrentSettlement).playerTeam.TeamColor == tournamentTeam3.TeamColor &&
            tournamentTeam3.IsParticipantRequired() &&
            participant.Character.IsHero &&
            (participant.Character.IsPlayerCharacter || participant.Character.HeroObject.IsPlayerCompanion ||
              (participant.Character.HeroObject.Spouse != null && participant.Character.HeroObject.Spouse.Name.Equals(Hero.MainHero.Name))))
          {
            tournamentTeam3.AddParticipant(participant);
            return false;
          }
          else if (TournamentsEnhancedSettings.Instance.BringCompanions &&
            TournamentKB.GetTournamentKB(Hero.MainHero.CurrentSettlement).playerTeam != null &&
            TournamentKB.GetTournamentKB(Hero.MainHero.CurrentSettlement).playerTeam.TeamColor == tournamentTeam3.TeamColor &&
            !tournamentTeam3.IsParticipantRequired() &&
            participant.Character.IsHero &&
            participant.Character.IsPlayerCharacter)
          {
            Utilities.LogAnnouncer("player could not join full team");
          }
        }
      }
      foreach (TournamentTeam tournamentTeam in __instance.Teams)
      {
        if (firstTime && TournamentKB.GetTournamentKB(Hero.MainHero.CurrentSettlement) != null && tournamentTeam.TeamColor == TournamentKB.GetTournamentKB(Hero.MainHero.CurrentSettlement).playerTeam.TeamColor)
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
      return false;
    }

  }
}
