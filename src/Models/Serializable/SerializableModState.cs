using System.Collections.Generic;

namespace TournamentsEnhanced.Models.Serializable
{
  public struct SerializableModState
  {
    public DaysSinceTournamentTracker daysSince;
    public TournamentRecordDictionary tournamentRecords;
    public int lotteryResults;
  }
}