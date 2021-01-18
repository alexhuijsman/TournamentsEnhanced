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
    public System.Random Random => MBRandom.Random != null ? MBRandom.Random : new System.Random();
    public virtual System.Random DeterministicRandom => MBRandom.DeterministicRandom;
    public float RandomFloat => MBRandom.RandomFloat;
    public float RandomFloatNormal => MBRandom.RandomFloatNormal;
    public MBVec3 InsideUnitSphere => MBRandom.InsideUnitSphere;

    public T ChooseWeighted<T>(IEnumerable<T> candidates, Func<T, float> weightFunction) => MBRandom.ChooseWeighted<T>(candidates, weightFunction);
    public T ChooseWeighted<T>(IEnumerable<T> candidates, Func<T, float> weightFunction, out int chosenIndex) => MBRandom.ChooseWeighted<T>(candidates, weightFunction, out chosenIndex);
    public int DeterministicRandomInt(int maxValue) => MBRandom.DeterministicRandomInt(maxValue);
    public float RandomFloatRanged(float maxVal) => MBRandom.RandomFloatRanged(maxVal);
    public float RandomFloatRanged(float minVal, float maxVal) => MBRandom.RandomFloatRanged(minVal, maxVal);
    public int RandomInt() => MBRandom.RandomInt();
    public int RandomInt(int maxValue) => MBRandom.RandomInt(maxValue);
    public int RandomInt(int minValue, int maxValue) => MBRandom.RandomInt(minValue, maxValue);
    public int RoundRandomized(float f) => MBRandom.RoundRandomized(f);
  }
}
