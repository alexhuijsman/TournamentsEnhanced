using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using static TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames.TournamentMatch;

namespace TournamentsEnhanced.TeamTournament
{
  public class TeamTournamentMatch
  {
    private enum TeamIndex
    {
      First = 0,
      Second = 1,
      Third = 2,
      Fourth = 3,
    }

    public IEnumerable<TeamTournamentTeam> Teams { get => _teams; }
    public MatchState State { get; private set; }
    public bool IsReady => this.State == MatchState.Ready;
    public bool IsFinished => this.State == MatchState.Finished;

    public IEnumerable<TeamTournamentTeam> Winners => this.GetWinners();

    public TeamTournamentMatch(int teamCount, int winnerTeamsPerMatch)
    {
      this._winnerTeamsPerMatch = winnerTeamsPerMatch;
      _teams = new List<TeamTournamentTeam>(teamCount);
      this.State = MatchState.Ready;
    }

    public void AddTeam(TeamTournamentTeam team)
    {
      if (!team.IsPlayerTeam)
      {
        team.TeamColor = BannerManager.GetColor(GetColorIndex((TeamIndex)_teams.Count));
        team.TeamBanner = Banner.CreateOneColoredEmptyBanner(_teams.Count);
      }

      _teams.Add(team);
    }

    private int GetColorIndex(TeamIndex teamIndex)
    {
      int colorIndex;
      switch (teamIndex)
      {
        case TeamIndex.First:
          colorIndex = TournamentsEnhancedSettings.Instance.Team1Color;
          break;
        case TeamIndex.Second:
          colorIndex = TournamentsEnhancedSettings.Instance.Team2Color;
          break;
        case TeamIndex.Third:
          colorIndex = TournamentsEnhancedSettings.Instance.Team3Color;
          break;
        case TeamIndex.Fourth:
          colorIndex = TournamentsEnhancedSettings.Instance.Team4Color;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return colorIndex;
    }

    public void End()
    {
      this.State = MatchState.Finished;
      this._winners = this.GetWinners();
    }

    public void Start()
    {
      if (this.State != MatchState.Started)
      {
        this.State = MatchState.Started;
        _teams.ForEach(t => t.ResetScore());
      }
    }
    public IEnumerable<TeamTournamentMember> MatchMembers => this.Teams.SelectMany(x => x.Members);
    public bool IsPlayerParticipating => this.Teams.Any(x => x.IsPlayerTeam);
    public bool IsPlayerTeamWinner => GetWinners().Any(x => x.IsPlayerTeam);

    public bool IsFullMatch => _teams.Count == _teams.Capacity;

    private List<TeamTournamentTeam> GetWinners()
    {
      if (this.State != MatchState.Finished || _winners == null)
        _winners = this.Teams.OrderByDescending(x => x.Score).Take(this._winnerTeamsPerMatch).ToList();

      return _winners;
    }

    private readonly int _winnerTeamsPerMatch;
    private List<TeamTournamentTeam> _winners;
    private List<TeamTournamentTeam> _teams;
  }
}

