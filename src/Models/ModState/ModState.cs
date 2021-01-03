using TournamentsEnhanced.Models.Serializable;

namespace TournamentsEnhanced.Models.ModState
{
  public class ModState
  {

    public static TournamentRecordDictionary TournamentRecords => _state.tournamentRecords;
    public static int WeeksSinceHostedTournament
    {
      get => _state.weeksSinceHostedTournament;
      set => _state.weeksSinceHostedTournament = value;
    }

    private static SerializableModState _state;

    static ModState() => _state = default(SerializableModState);
  }
}