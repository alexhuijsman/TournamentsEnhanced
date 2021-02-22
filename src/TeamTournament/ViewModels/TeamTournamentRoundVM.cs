using System.Collections.Generic;
using System.Linq;
using SandBox.ViewModelCollection.Tournament.ViewModels;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using static TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames.TournamentMatch;

namespace TournamentsEnhanced.TeamTournament.ViewModels
{
  public class TeamTournamentRoundVM : ViewModel
  {
    public TeamTournamentRound Round { get; private set; }
    public bool IsFinished => MatchVMs.All(m => m.Match.State == MatchState.Finished);
    public IEnumerable<TeamTournamentMatchVM> MatchVMs { get => _matchVMs; }

    public TeamTournamentRoundVM()
    {
      this.Match1 = new TeamTournamentMatchVM();
      this.Match2 = new TeamTournamentMatchVM();
      this.Match3 = new TeamTournamentMatchVM();
      this.Match4 = new TeamTournamentMatchVM();
      this.Match5 = new TeamTournamentMatchVM();
      this.Match6 = new TeamTournamentMatchVM();
      this.Match7 = new TeamTournamentMatchVM();
      this.Match8 = new TeamTournamentMatchVM();
      this._matchVMs = new List<TeamTournamentMatchVM>
      {
        this.Match1,
        this.Match2,
        this.Match3,
        this.Match4,
        this.Match5,
        this.Match6,
        this.Match7,
        this.Match8
      };
    }

    public override void RefreshValues()
    {
      base.RefreshValues();
      this._matchVMs.ForEach(x => x.RefreshValues());
    }

    public void Initialize() => this._matchVMs.ForEach(x => x.Initialize());

    public void Initialize(TeamTournamentRound round, TextObject name)
    {
      this.Initialize(round);
      this.Name = name.ToString();
    }

    public void Initialize(TeamTournamentRound round)
    {
      this.IsValid = round != null;

      if (round != null)
      {
        this.Round = round;
        this.Count = round.MatchCount; // count of machtes
        var index = 0;
        foreach (var match in round.Matches)
          _matchVMs[index++].Initialize(match);
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
    public string Name
    {
      get
      {
        return this._name;
      }
      set
      {
        if (value != this._name)
        {
          this._name = value;
          OnPropertyChangedWithValue(value, "Name");
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
    public TeamTournamentMatchVM Match1
    {
      get
      {
        return this._match1;
      }
      set
      {
        if (value != this._match1)
        {
          this._match1 = value;
          OnPropertyChangedWithValue(value, "Match1");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMatchVM Match2
    {
      get
      {
        return this._match2;
      }
      set
      {
        if (value != this._match2)
        {
          this._match2 = value;
          OnPropertyChangedWithValue(value, "Match2");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMatchVM Match3
    {
      get
      {
        return this._match3;
      }
      set
      {
        if (value != this._match3)
        {
          this._match3 = value;
          OnPropertyChangedWithValue(value, "Match3");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMatchVM Match4
    {
      get
      {
        return this._match4;
      }
      set
      {
        if (value != this._match4)
        {
          this._match4 = value;
          OnPropertyChangedWithValue(value, "Match4");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMatchVM Match5
    {
      get
      {
        return this._match5;
      }
      set
      {
        if (value != this._match5)
        {
          this._match5 = value;
          OnPropertyChangedWithValue(value, "Match5");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMatchVM Match6
    {
      get
      {
        return this._match6;
      }
      set
      {
        if (value != this._match6)
        {
          this._match6 = value;
          OnPropertyChangedWithValue(value, "Match6");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMatchVM Match7
    {
      get
      {
        return this._match7;
      }
      set
      {
        if (value != this._match7)
        {
          this._match7 = value;
          OnPropertyChangedWithValue(value, "Match7");
        }
      }
    }

    [DataSourceProperty]
    public TeamTournamentMatchVM Match8
    {
      get
      {
        return this._match8;
      }
      set
      {
        if (value != this._match8)
        {
          this._match8 = value;
          OnPropertyChangedWithValue(value, "Match8");
        }
      }
    }
    #endregion view properties

    private TeamTournamentMatchVM _match1;
    private TeamTournamentMatchVM _match2;
    private TeamTournamentMatchVM _match3;
    private TeamTournamentMatchVM _match4;
    private TeamTournamentMatchVM _match5;
    private TeamTournamentMatchVM _match6;
    private TeamTournamentMatchVM _match7;
    private TeamTournamentMatchVM _match8;
    private int _count = -1;
    private string _name;
    private bool _isValid;
    private List<TeamTournamentMatchVM> _matchVMs;
  }
}
