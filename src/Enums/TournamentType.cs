namespace TournamentsEnhanced
{
  public enum TournamentType
  {
    None,
    Initial,
    Wedding,
    Birth,
    Prosperity,
    Peace,
    Invitation,
    Hosted,
    Lord
  }

  public static class TournamentTypeExtensions
  {
    public static bool HasPriorityOver(this TournamentType type, TournamentType other)
    {
      return false;
    }
  }
}
