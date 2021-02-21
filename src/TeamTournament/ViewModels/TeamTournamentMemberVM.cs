using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;

namespace TournamentsEnhanced.TeamTournament.ViewModels
{
  public class TeamTournamentMemberVM : ViewModel
  {
    public TeamTournamentMember Member { get; private set; }

    public TeamTournamentMemberVM()
    {
      this._visual = new ImageIdentifierVM(ImageIdentifierType.Null);
      this._character = new CharacterViewModel(CharacterViewModel.StanceTypes.CelebrateVictory);
    }

    public TeamTournamentMemberVM(TeamTournamentMember member) : this()
    {
      Refresh(member, Color.FromUint(member.Team.TeamColor));
    }

    public override void RefreshValues()
    {
      base.RefreshValues();
      if (this.IsInitialized)
        this.Refresh(this.Member, this.TeamColor);
    }

    public void Refresh(TeamTournamentMember member, Color teamColor)
    {
      this.Member = member;
      this.TeamColor = teamColor;
      this.State = member == null ? 0 : (member.IsPlayer ? 2 : 1);
      this.IsInitialized = true;
      if (member != null)
      {
        this.Name = member.Character.Name.ToString();
        this.Character = new CharacterViewModel(CharacterViewModel.StanceTypes.CelebrateVictory);
        this.Character.FillFrom(member.Character, -1);
        this.Visual = new ImageIdentifierVM(CharacterCode.CreateFrom(member.Character));
        this.IsValid = true;
        this.IsMainHero = member.IsPlayer;
      }
    }

    public void Refresh()
    {
      OnPropertyChanged("Name");
      OnPropertyChanged("Visual");
      OnPropertyChanged("Score");
      OnPropertyChanged("State");
      OnPropertyChanged("TeamColor");
      OnPropertyChanged("IsDead");
      this.IsMainHero = (this.Member != null && this.Member.IsPlayer);
    }

    #region view properties
    [DataSourceProperty]
    public bool IsInitialized
    {
      get => this._isInitialized;
      set
      {
        if (value != this._isInitialized)
        {
          this._isInitialized = value;
          OnPropertyChangedWithValue(value, "IsInitialized");
        }
      }
    }

    [DataSourceProperty]
    public bool IsValid
    {
      get => this._isValid;
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
    public bool IsDead
    {
      get => this._isDead;
      set
      {
        if (value != this._isDead)
        {
          this._isDead = value;
          OnPropertyChangedWithValue(value, "IsDead");
        }
      }
    }

    [DataSourceProperty]
    public bool IsMainHero
    {
      get => this._isMainHero;
      set
      {
        if (value != this._isMainHero)
        {
          this._isMainHero = value;
          OnPropertyChangedWithValue(value, "IsMainHero");
        }
      }
    }

    [DataSourceProperty]
    public Color TeamColor
    {
      get => this._teamColor;
      set
      {
        if (value != this._teamColor)
        {
          this._teamColor = value;
          OnPropertyChangedWithValue(value, "TeamColor");
        }
      }
    }

    [DataSourceProperty]
    public ImageIdentifierVM Visual
    {
      get => this._visual;
      set
      {
        if (value != this._visual)
        {
          this._visual = value;
          OnPropertyChangedWithValue(value, "Visual");
        }
      }
    }

    [DataSourceProperty]
    public int State
    {
      get => this._state;
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
    public bool IsQualifiedForNextRound
    {
      get => this._isQualifiedForNextRound;
      set
      {
        if (value != this._isQualifiedForNextRound)
        {
          this._isQualifiedForNextRound = value;
          OnPropertyChangedWithValue(value, "IsQualifiedForNextRound");
        }
      }
    }

    [DataSourceProperty]
    public string Score
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
    public string Name
    {
      get => this._name;
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
    public CharacterViewModel Character
    {
      get => this._character;
      set
      {
        if (value != this._character)
        {
          this._character = value;
          OnPropertyChangedWithValue(value, "Character");
        }
      }
    }
    #endregion 

    private bool _isInitialized;
    private bool _isValid;
    private string _name = "";
    private string _score = "-";
    private bool _isQualifiedForNextRound;
    private int _state = -1;
    private ImageIdentifierVM _visual;
    private Color _teamColor;
    private bool _isDead;
    private bool _isMainHero;
    private CharacterViewModel _character;

#pragma warning disable IDE0051 // Remove unused private members
    /// <summary>
    /// DO NOT REMOVE CALLED DYNAMICALLY
    /// </summary>
    private void ExecuteOpenEncyclopedia()
    {
      if (this.Member != null && this.Member.Character != null)
      {
        Campaign.Current.EncyclopediaManager.GoToLink(this.Member.Character.EncyclopediaLink);
      }
    }
#pragma warning restore IDE0051 // Remove unused private members
  }
}
