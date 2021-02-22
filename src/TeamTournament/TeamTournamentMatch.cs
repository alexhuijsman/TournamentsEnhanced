using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using static TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames.TournamentMatch;

namespace TournamentsEnhanced.TeamTournament
{
  public class TeamTournamentMatch
  {
    public IEnumerable<TeamTournamentTeam> Teams { get => _teams; }
    public MatchState State { get; private set; }
    public bool IsReady => this.State == MatchState.Ready;
    public bool IsFinished => this.State == MatchState.Finished;

    public static int[] TeamColors = new int[] // TODO: maybe make a setting out of this
    {
      119,
      118,
      120,
      121
    };

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
        team.TeamColor = BannerManager.GetColor(TeamColors[_teams.Count]);
        team.TeamBanner = Banner.CreateOneColoredEmptyBanner(_teams.Count);
      }
      _teams.Add(team);
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

