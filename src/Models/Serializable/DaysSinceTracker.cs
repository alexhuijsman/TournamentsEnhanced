using System;
using System.Collections.Generic;

namespace TournamentsEnhanced.Models.Serializable
{
  public class DaysSinceTracker<T> : Dictionary<T, int>
  where T : IConvertible
  {
    public DaysSinceTracker(params T[] types)
    {
      InitializeDictionary(types);
    }

    private void InitializeDictionary(T[] types)
    {
      foreach (var type in types)
      {
        Add(type, Constants.DaysSinceTracker.Default.KeyValue);
      }
    }

    public virtual void Reset()
    {
      foreach (var key in Keys.ToList())
      {
        this[key] = Constants.DaysSinceTracker.Default.KeyValue;
      }
    }

    public virtual void IncrementDay()
    {
      foreach (var key in Keys)
      {
        this[key]++;
      }
    }
  }

  public class DaysSinceTournamentTracker : DaysSinceTracker<TournamentType>
  {
    public DaysSinceTournamentTracker(params TournamentType[] types) : base(types) { }
  }
}