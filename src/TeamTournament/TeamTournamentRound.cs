using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;

namespace TournamentsEnhanced.TeamTournament
{
  public class TeamTournamentRound
  {
    public IEnumerable<TeamTournamentMatch> Matches { get => _matches; }
    public int MatchCount { get => _matches.Count; }
    public int CurrentMatchIndex { get; private set; }
    public TeamTournamentMatch CurrentMatch => (this.CurrentMatchIndex >= this._matches.Count) ? null : this._matches[this.CurrentMatchIndex];
    public IEnumerable<TeamTournamentTeam> Teams => Matches != null ? Matches.SelectMany(x => x.Teams) : null;

    public TeamTournamentRound(int teamsInRound, int numberOfMatches, int numerOfWinnerTeams)
    {
      this.CurrentMatchIndex = 0;
      _matches = new List<TeamTournamentMatch>(numberOfMatches);
      for (var i = 0; i < numberOfMatches; i++)
        this._matches.Add(new TeamTournamentMatch(teamsInRound / numberOfMatches, numerOfWinnerTeams));
    }

    public int AddTeam(TeamTournamentTeam team)
    {
      int matchNum;
      int tryNum = 0;
      do
      {
        if (tryNum++ == 64)
        {
          matchNum = this._matches.FindIndex(x => !x.IsFullMatch);
          break;
        }
        matchNum = MBRandom.Random.Next(this._matches.Count);
      } while (this._matches[matchNum].IsFullMatch);

      this._matches[matchNum].AddTeam(team);
      return matchNum;
    }

    public void EndMatch()
    {
      this.CurrentMatch?.End();
      this.CurrentMatchIndex++;
    }

    private List<TeamTournamentMatch> _matches;
  }
}

