using System;
using System.Collections.Generic;

using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TournamentsEnhanced.Wrappers.Core
{
  public static class MBMBRandom
  {
    public static Random Random => MBRandom.Random;
    public static Random DeterministicRandom => MBRandom.DeterministicRandom;
    public static float RandomFloat => MBRandom.RandomFloat;
    public static float RandomFloatNormal => MBRandom.RandomFloatNormal;
    public static Vec3 InsideUnitSphere => MBRandom.InsideUnitSphere;

    public static T ChooseWeighted<T>(IEnumerable<T> candidates, Func<T, float> weightFunction) => MBRandom.ChooseWeighted<T>(candidates, weightFunction);
    public static T ChooseWeighted<T>(IEnumerable<T> candidates, Func<T, float> weightFunction, out int chosenIndex) => MBRandom.ChooseWeighted<T>(candidates, weightFunction, out chosenIndex);
    public static int DeterministicRandomInt(int maxValue) => MBRandom.DeterministicRandomInt(maxValue);
    public static float RandomFloatRanged(float maxVal) => MBRandom.RandomFloatRanged(maxVal);
    public static float RandomFloatRanged(float minVal, float maxVal) => MBRandom.RandomFloatRanged(minVal, maxVal);
    public static int RandomInt() => MBRandom.RandomInt();
    public static int RandomInt(int maxValue) => MBRandom.RandomInt(maxValue);
    public static int RandomInt(int minValue, int maxValue) => MBRandom.RandomInt(minValue, maxValue);
    public static int RoundRandomized(float f) => MBRandom.RoundRandomized(f);
  }
}
