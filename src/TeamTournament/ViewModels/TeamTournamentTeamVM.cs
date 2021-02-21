using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;
using TournamentsEnhanced.TeamTournament;
using TournamentsEnhanced.TeamTournament.ViewModels;

namespace SandBox.ViewModelCollection.Tournament.ViewModels
{
  public class TeamTournamentTeamVM : ViewModel
  {
    public IEnumerable<TeamTournamentMemberVM> Members { get => _members; }
    public TeamTournamentTeam Team { get; private set; }

    public TeamTournamentTeamVM()
    {
      this.Participant1 = new TeamTournamentMemberVM();
      this.Participant2 = new TeamTournamentMemberVM();
      this.Participant3 = new TeamTournamentMemberVM();
      this.Participant4 = new TeamTournamentMemberVM();
      this.Participant5 = new TeamTournamentMemberVM();
      this.Participant6 = new TeamTournamentMemberVM();
      this.Participant7 = new TeamTournamentMemberVM();
      this.Participant8 = new TeamTournamentMemberVM();
      _members = new List<TeamTournamentMemberVM>
      {
        this.Participant1,
        this.Participant2,
        this.Participant3,
        this.Participant4,
        this.Participant5,
        this.Participant6,
        this.Participant7,
        this.Participant8
      };
    }

    public IEnumerable<TeamTournamentMemberVM> GetMembers() => this.Members.Where(x => x.IsValid);

    public TeamTournamentMemberVM GetTeamLeader() => Members.FirstOrDefault(x => x.Member == Team.GetTeamLeader());

    public override void RefreshValues()
    {
      base.RefreshValues();
      this._members.ForEach(x => x.RefreshValues());
    }

    public void Initialize()
    {
      this.IsValid = this.Team != null;

      for (var i = 0; Team != null && i < this.Count; i++)
        this._members[i].Refresh(Team.Members.ElementAtOrDefault(i), Color.FromUint(this.Team.TeamColor));
    }

    public void Initialize(TeamTournamentTeam team)
    {
      this.Team = team;
      this.Count = team.Members.Count();
      this.Initialize();
    }

    public void Refresh()
    {
      this.IsValid = (this.Team != null);
      OnPropertyChanged("Count");

      int num = 0;
      foreach (var member in this.Members.Where(x => x.IsValid))
      {
        OnPropertyChanged("Participant" + num++);
        member.Refresh();
      }
    }

    #region view properties
    [DataSourceProperty]
    public bool IsValid
    {
      get => _isValid;
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
    public int Score
    {
      get => this._score;
      set
      {
        if (value != this._score)
        {
          this._score = value;
          OnPropertyChangedWithValue(value, "Score");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMemberVM Participant1
    {
      get => this._participant1;
      set
      {
        if (value != this._participant1)
        {
          this._participant1 = value;
          OnPropertyChangedWithValue(value, "Participant1");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMemberVM Participant2
    {
      get => this._participant2;
      set
      {
        if (value != this._participant2)
        {
          this._participant2 = value;
          OnPropertyChangedWithValue(value, "Participant2");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMemberVM Participant3
    {
      get => this._participant3;
      set
      {
        if (value != this._participant3)
        {
          this._participant3 = value;
          OnPropertyChangedWithValue(value, "Participant3");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMemberVM Participant4
    {
      get
      {
        return this._participant4;
      }
      set
      {
        if (value != this._participant4)
        {
          this._participant4 = value;
          OnPropertyChangedWithValue(value, "Participant4");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMemberVM Participant5
    {
      get
      {
        return this._participant5;
      }
      set
      {
        if (value != this._participant5)
        {
          this._participant5 = value;
          OnPropertyChangedWithValue(value, "Participant5");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMemberVM Participant6
    {
      get => this._participant6;
      set
      {
        if (value != this._participant6)
        {
          this._participant6 = value;
          OnPropertyChangedWithValue(value, "Participant6");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMemberVM Participant7
    {
      get
      {
        return this._participant7;
      }
      set
      {
        if (value != this._participant7)
        {
          this._participant7 = value;
          OnPropertyChangedWithValue(value, "Participant7");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMemberVM Participant8
    {
      get
      {
        return this._participant8;
      }
      set
      {
        if (value != this._participant8)
        {
          this._participant8 = value;
          OnPropertyChangedWithValue(value, "Participant8");
        }
      }
    }

    [DataSourceProperty]
    public int Count
    {
      get => this._count;
      set
      {
        if (value != this._count)
        {
          this._count = value;
          OnPropertyChangedWithValue(value, "Count");
        }
      }
    }
    #endregion

    private int _count = -1;
    private TeamTournamentMemberVM _participant1;
    private TeamTournamentMemberVM _participant2;
    private TeamTournamentMemberVM _participant3;
    private TeamTournamentMemberVM _participant4;
    private TeamTournamentMemberVM _participant5;
    private TeamTournamentMemberVM _participant6;
    private TeamTournamentMemberVM _participant7;
    private TeamTournamentMemberVM _participant8;
    private int _score;
    private bool _isValid;
    private List<TeamTournamentMemberVM> _members;
  }
}
