using System;

using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Random
{
  public class Lottery
  {
    public static Lottery Instance { get; } = new Lottery();
    public MBMBRandom MBMBRandom { protected get; set; } = MBMBRandom.Instance;
    public ModState ModState { protected get; set; } = ModState.Instance;

    protected Lottery() { }

    public void DeterministicallyRefreshWinners()
    {
      ModState.LotteryWinners = MBMBRandom.DeterministicRandom.Next();
    }

    public bool IsWinner<T>(T value)
    where T : IConvertible
    {
      return ((ModState.LotteryWinners >> Convert.ToInt32(value)) & 1) == 1;
    }

    public void DailyTick()
    {
      DeterministicallyRefreshWinners();
    }
  }
}