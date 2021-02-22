using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.ViewModelCollection.Tournament.ViewModels;
using TaleWorlds.Library;

namespace TournamentsEnhanced.TeamTournament.ViewModels
{
  public class TeamTournamentMatchVM : ViewModel
  {
    public TeamTournamentMatch Match { get; private set; }
    public IEnumerable<TeamTournamentTeamVM> Teams { get => _teams; }

    public TeamTournamentMatchVM()
    {
      this.Team1 = new TeamTournamentTeamVM();
      this.Team2 = new TeamTournamentTeamVM();
      this.Team3 = new TeamTournamentTeamVM();
      this.Team4 = new TeamTournamentTeamVM();
      this._teams = new List<TeamTournamentTeamVM>
      {
        this.Team1,
        this.Team2,
        this.Team3,
        this.Team4
      };
    }

    public IEnumerable<TeamTournamentMemberVM> GetMatchMemberVMs() => Teams.SelectMany(x => x.Members);

    public override void RefreshValues()
    {
      base.RefreshValues();
      this._teams.ForEach(x => x.RefreshValues());
    }

    public void Initialize()
    {
      foreach (var tournamentTeamVM in this.Teams)
      {
        if (tournamentTeamVM.IsValid)
          tournamentTeamVM.Initialize();
      }
    }

    public void Initialize(TeamTournamentMatch match)
    {
      int index = 0;
      this.Match = match;
      this.IsValid = (this.Match != null);
      this.Count = match.Teams.Count();

      foreach (var team in match.Teams)
        this._teams[index++].Initialize(team);

      this.State = 0;
    }

    public void Refresh(bool forceRefresh)
    {
      if (forceRefresh)
      {
        OnPropertyChanged("Count");
      }
      for (int i = 0; i < this.Count; i++)
      {
        var tournamentTeamVM = this._teams[i];
        if (forceRefresh)
        {
          OnPropertyChanged("Team" + i + 1);
        }
        tournamentTeamVM.Refresh();
        for (int j = 0; j < tournamentTeamVM.Count; j++)
        {
          var teamMemberVM = tournamentTeamVM.Members.ElementAt(j);
          teamMemberVM.Score = teamMemberVM.Member.Score.ToString();
          teamMemberVM.IsQualifiedForNextRound = this.Match.Winners != null && this.Match.Winners.Any(x => x.Members.Contains(teamMemberVM.Member));
        }
      }
    }

    public void RefreshActiveMatch()
    {
      for (int i = 0; i < this.Count; i++)
      {
        var tournamentTeamVM = this._teams[i];
        for (int j = 0; j < tournamentTeamVM.Count; j++)
        {
          var tournamentParticipantVM = tournamentTeamVM.Members.ElementAt(j);
          tournamentParticipantVM.Score = tournamentParticipantVM.Member.Score.ToString();
        }
      }
    }

    public void Refresh(TeamTournamentMatchVM target)
    {
      OnPropertyChanged("Count");
      int num = 0;
      foreach (var tournamentTeamVM in from t in this.Teams
                                       where t.IsValid
                                       select t)
      {
        OnPropertyChanged("Team" + num + 1);
        tournamentTeamVM.Refresh();
        num++;
      }
    }

    #region view properties

    [DataSourceProperty]
    public bool IsValid
    {
      get
      {
        return this._isValid;
      }
      set
      {
        if (value != this._isValid)
        {
          this._isValid = value;
          OnPropertyChangedWithValue(value, "IsValid");
        }
      }
    }

    [DataSourceProperty]
    public int State
    {
      get
      {
        return this._state;
      }
      set
      {
        if (value != this._state)
        {
          this._state = value;
          OnPropertyChangedWithValue(value, "State");
        }
      }
    }

    [DataSourceProperty]
    public int Count
    {
      get
      {
        return this._count;
      }
      set
      {
        if (value != this._count)
        {
          this._count = value;
          OnPropertyChangedWithValue(value, "Count");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentTeamVM Team1
    {
      get
      {
        return this._team1;
      }
      set
      {
        if (value != this._team1)
        {
          this._team1 = value;
          OnPropertyChangedWithValue(value, "Team1");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentTeamVM Team2
    {
      get
      {
        return this._team2;
      }
      set
      {
        if (value != this._team2)
        {
          this._team2 = value;
          OnPropertyChangedWithValue(value, "Team2");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentTeamVM Team3
    {
      get
      {
        return this._team3;
      }
      set
      {
        if (value != this._team3)
        {
          this._team3 = value;
          OnPropertyChangedWithValue(value, "Team3");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentTeamVM Team4
    {
      get
      {
        return this._team4;
      }
      set
      {
        if (value != this._team4)
        {
          this._team4 = value;
          OnPropertyChangedWithValue(value, "Team4");
        }
      }
    }
    #endregion

    private TeamTournamentTeamVM _team1;
    private TeamTournamentTeamVM _team2;
    private TeamTournamentTeamVM _team3;
    private TeamTournamentTeamVM _team4;
    private int _count = -1;
    private int _state = -1;
    private bool _isValid;
    private List<TeamTournamentTeamVM> _teams;
  }
}
