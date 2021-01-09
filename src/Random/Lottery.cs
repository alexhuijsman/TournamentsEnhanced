using System;

using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Random
{
  public static class Lottery
  {
    public static void DeterministicallyRefreshWinners()
    {
      ModState.LotteryWinners = MBMBRandom.DeterministicRandom.Next();
    }

    public static bool IsWinner<T>(T value)
    where T : IConvertible
    {
      return ((ModState.LotteryWinners >> Convert.ToInt32(value)) & 1) == 1;
    }

    public static void DailyTick()
    {
      DeterministicallyRefreshWinners();
    }
  }
}