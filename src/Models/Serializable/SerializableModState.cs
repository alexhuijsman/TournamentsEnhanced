using System.Collections.Generic;

namespace TournamentsEnhanced.Models.Serializable
{
  public struct SerializableModState
  {
    public DaysSinceTracker<TournamentType> daysSince;
    public TournamentRecordDictionary tournamentRecords;
    public int lotteryResults;
  }
}