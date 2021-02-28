using System;
using System.Collections.Generic;
using System.Linq;
using static TournamentsEnhanced.Constants.DaysSinceTracker;

namespace TournamentsEnhanced.Models.Serializable
{
  public class DaysSinceTracker<T> : Dictionary<T, int>
  where T : Enum
  {
    public DaysSinceTracker(params T[] types)
    {
      InitializeDictionary(types);
    }

    private void InitializeDictionary(T[] types)
    {
      foreach (var type in types)
      {
        Add(type, Default.DictionaryValue);
      }
    }

    public virtual void IncrementDay()
    {
      foreach (var key in Keys.ToArray())
      {
        if (this[key] == Default.DictionaryValue)
        {
          continue;
        }

        this[key]++;
      }
    }

    public virtual void Reset()
    {
      foreach (var key in Keys.ToList())
      {
        this[key] = Default.DictionaryValue;
      }
    }
  }
}