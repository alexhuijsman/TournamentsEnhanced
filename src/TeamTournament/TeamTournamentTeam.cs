using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;

namespace TournamentsEnhanced.TeamTournament
{
  public class TeamTournamentTeam
  {
    private List<TeamTournamentMember> _members;
    private int _score;
    private TeamTournamentMember _leader;

    public IEnumerable<TeamTournamentMember> Members { get => _members; }
    public int Score { get => _score; }
    public Banner TeamBanner { get; set; }
    public uint TeamColor { get; set; }
    public bool IsPlayerTeam => this.Members.Any(x => x.IsPlayer);

    public TeamTournamentTeam(IEnumerable<TeamTournamentMember> members, Banner teamBanner = null, uint teamColor = 0, TeamTournamentMember leader = null)
    {
      _members = new List<TeamTournamentMember>(members);
      this.TeamBanner = teamBanner;
      this.TeamColor = teamColor;

      foreach (var el in _members)
        el.SetTeam(this);

      this._leader = leader;
    }

    public void AddScore(int score)
    {
      _score += score;
      Members.ToList().ForEach(x => x.AddScore(score));
    }

    public void ResetScore()
    {
      this._score = 0;
      Members.ToList().ForEach(x => x.ResetScore());
    }

    public TeamTournamentMember GetTeamLeader()
    {
      if (this._leader != null)
        return this._leader;

      if (IsPlayerTeam)
        return this.Members.First(x => x.IsPlayer);
      else
      {
        return
          this.Members.OrderByDescending(x => x.Character.GetPower()).FirstOrDefault(x => x.Character.IsHero)
          ?? this.Members.OrderByDescending(x => x.Character.GetPower()).First();
      }
    }
  }
}
