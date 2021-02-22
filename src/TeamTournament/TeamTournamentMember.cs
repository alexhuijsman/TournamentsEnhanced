using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace TournamentsEnhanced.TeamTournament
{
  public class TeamTournamentMember
  {
    public int Score { get; private set; }
    public CharacterObject Character { get; private set; }
    public UniqueTroopDescriptor Descriptor { get; private set; }
    public TeamTournamentTeam Team { get => _team; private set { _team = value; } }
    public Equipment MatchEquipment { get; set; }
    public bool IsPlayer => Character != null && Character.IsPlayerCharacter;

    public TeamTournamentMember(CharacterObject character)
    {
      this.Character = character;
      this.Descriptor = new UniqueTroopDescriptor(Game.Current.NextUniqueTroopSeed);
    }

    public int AddScore(int score)
    {
      this.Score += score;
      return this.Score;
    }

    public void SetTeam(TeamTournamentTeam team) => this.Team = team;

    private TeamTournamentTeam _team;

    public void ResetScore() => this.Score = 0;

    public bool IsCharWithDescriptor(int uniqueTroopSeed) => this.Descriptor.CompareTo(uniqueTroopSeed) == 0;
  }
}