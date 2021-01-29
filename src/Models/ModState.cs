using System;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Models
{
  public class ModState
  {
    private SerializableModState _state;
    public static ModState Instance { get; } = new ModState();
    public SerializableModState SerializableObject { get => _state; set => _state = value; }
    protected MBMBRandom MBMBRandom { get; set; } = MBMBRandom.Instance;
    public DaysSinceTournamentTracker DaysSince => _state.daysSince;
    public TournamentRecordDictionary TournamentRecords => _state.tournamentRecords;
    public virtual int LotteryResults { get => _state.lotteryResults; set => _state.lotteryResults = value; }

    protected ModState()
    {
      _state.tournamentRecords = new TournamentRecordDictionary();
      _state.daysSince = new DaysSinceTournamentTracker(Constants.DaysSinceTracker.TournamentTypes);
      _state.lotteryResults = Constants.LotteryResults.NoWinners;
    }


    public virtual void Reset()
    {
      TournamentRecords.Clear();
      DaysSince.Reset();
      LotteryResults = Constants.LotteryResults.NoWinners;
    }

    public void DailyTick()
    {
      DaysSince.IncrementDay();
      RefreshLotteryResults();
    }

    private void RefreshLotteryResults()
    {
      LotteryResults = MBMBRandom.DeterministicRandom.Next();
    }

    public bool IsLotteryWinner<T>(T value)
    where T : IConvertible
    {
      var intValue = Convert.ToInt32(value);

      if (intValue > Constants.ModState.MaxIsWinnerArgValue)
      {
        throw new ArgumentOutOfRangeException($"value, Parameter value: {intValue}");
      }

      return ((LotteryResults >> intValue) & 1) != 0;
    }
  }
}