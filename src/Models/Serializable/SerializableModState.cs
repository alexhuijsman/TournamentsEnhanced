using System.Collections.Generic;

namespace TournamentsEnhanced.Models.Serializable
{
  public struct SerializableModState
  {
    public TournamentRecordDictionary tournamentRecords;
    public int weeksSinceHostedTournament;
  }
}