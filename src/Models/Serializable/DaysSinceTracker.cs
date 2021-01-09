using System;
using System.Collections.Generic;

namespace TournamentsEnhanced.Models.Serializable
{
  public class DaysSinceTracker<T> : Dictionary<T, int>
  where T : Enum
  {
    private static readonly int DefaultValue = int.MaxValue;

    public DaysSinceTracker(params T[] types)
    {
      InitializeDictionary(types);
    }

    private void InitializeDictionary(T[] types)
    {
      foreach (var type in types)
      {
        Add(type, DefaultValue);
      }
    }

    public void Reset()
    {
      foreach (var key in Keys)
      {
        this[key] = DefaultValue;
      }
    }

    public void DailyTick()
    {
      IncrementAll();
    }

    private void IncrementAll()
    {
      foreach (var key in Keys)
      {
        this[key]++;
      }
    }
  }
}