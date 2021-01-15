using TaleWorlds.Library;

namespace TournamentsEnhanced.Wrappers.Library
{
  public static class MBMathF
  {
    public const float DegToRad = MathF.DegToRad;
    public const float RadToDeg = MathF.RadToDeg;
    public const float TwoPI = MathF.TwoPI;
    public const float PI = MathF.PI;
    public const float HalfPI = MathF.HalfPI;
    public const float Epsilon = MathF.Epsilon;

    public static float Abs(float f) => MathF.Abs(f);
    public static float AngleClamp(float angle) => MathF.AngleClamp(angle);
    public static float AngleLerp(float angleFrom, float angleTo, float amount, float minimumDifference = 1E-05F) => MathF.AngleLerp(angleFrom, angleTo, amount, minimumDifference);
    public static int Ceiling(float f) => MathF.Ceiling(f);
    public static object Clamp(float ratio, object minPriceFactor, object maxPriceFactor) => MathF.Clamp(ratio, minPriceFactor, maxPriceFactor);
    public static float Clamp(float value, float minValue, float maxValue) => MathF.Clamp(value, minValue, maxValue);
    public static float Cos(float x) => MathF.Cos(x);
    public static int Floor(float f) => MathF.Floor(f);
    public static int GreatestCommonDivisor(int a, int b) => MathF.GreatestCommonDivisor(a, b);
    public static bool IsValidValue(float f) => MathF.IsValidValue(f);
    public static float Lerp(float valueFrom, float valueTo, float amount, float minimumDifference = 1E-05F) => MathF.Lerp(valueFrom, valueTo, amount, minimumDifference);
    public static float Max(float a, float b, float c) => MathF.Max(a, b, c);
    public static float PingPong(float min, float max, float time) => MathF.PingPong(min, max, time);
    public static float Pow(float x, float y) => MathF.Pow(x, y);
    public static int Round(float f) => MathF.Round(f);
    public static float Sin(float x) => MathF.Sin(x);
    public static float Sqrt(float x) => MathF.Sqrt(x);
    public static float Tan(float x) => MathF.Tan(x);
  }
}
