using TournamentsEnhanced.Models.Serializable;

namespace TournamentsEnhanced.Models.ModState
{
  public class ModState
  {
    public static ModState Instance { get; } = new ModState();
    public DaysSinceTracker<TournamentType> DaysSince => _state.daysSince;
    public TournamentRecordDictionary TournamentRecords => _state.tournamentRecords;

    public virtual int LotteryWinners
    {
      get => _state.dailyLotteryWinners;
      set => _state.dailyLotteryWinners = value;
    }

    public ModState() => Initialize();

    private void Initialize()
    {
      _state.tournamentRecords = new TournamentRecordDictionary();
      _state.daysSince = new DaysSinceTournamentTracker(Constants.DaysSince.TournamentTypes);
    }

    public virtual void Reset()
    {
      TournamentRecords.Clear();
      DaysSince.Reset();
    }

    public void DailyTick()
    {
      DaysSince.DailyTick();
    }

    private SerializableModState _state;

    public SerializableModState SerializableObject { get => _state; set => _state = value; }

  }
}