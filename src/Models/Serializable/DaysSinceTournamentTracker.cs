namespace TournamentsEnhanced.Models.Serializable
{
  public class DaysSinceTournamentTracker : DaysSinceTracker<TournamentType>
  {
    public DaysSinceTournamentTracker(params TournamentType[] types) : base(types) { }
  }
}