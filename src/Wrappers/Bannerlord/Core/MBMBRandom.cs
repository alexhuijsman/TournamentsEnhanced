using System;
using System.Collections.Generic;

using TaleWorlds.Core;
using TournamentsEnhanced.Wrappers.Library;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBMBRandom
  {
    public static MBMBRandom Instance { get; } = new MBMBRandom();

    protected MBMBRandom() { }
    public virtual System.Random DeterministicRandom => MBRandom.DeterministicRandom;

    public virtual T ChooseWeighted<T>(IEnumerable<T> candidates, Func<T, float> weightFunction) => MBRandom.ChooseWeighted<T>(candidates, weightFunction);
    public virtual T ChooseWeighted<T>(IEnumerable<T> candidates, Func<T, float> weightFunction, out int chosenIndex) => MBRandom.ChooseWeighted<T>(candidates, weightFunction, out chosenIndex);
    public virtual int DeterministicRandomInt(int maxValue) => MBRandom.DeterministicRandomInt(maxValue);
    public virtual float RandomFloatRanged(float maxVal) => MBRandom.RandomFloatRanged(maxVal);
    public virtual float RandomFloatRanged(float minVal, float maxVal) => MBRandom.RandomFloatRanged(minVal, maxVal);
    public virtual int RandomInt() => MBRandom.RandomInt();
    public virtual int RandomInt(int maxValue) => MBRandom.RandomInt(maxValue);
    public virtual int RandomInt(int minValue, int maxValue) => MBRandom.RandomInt(minValue, maxValue);
    public virtual int RoundRandomized(float f) => MBRandom.RoundRandomized(f);
  }
}
