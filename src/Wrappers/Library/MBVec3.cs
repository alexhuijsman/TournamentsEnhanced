using TaleWorlds.Library;

namespace TournamentsEnhanced.Wrappers.Library
{
  public struct MBVec3 : IMBVec3
  {
    public Vec3 UnwrappedStruct { get; set; }

    public static MBVec3 Side => Vec3.Side;
    public static MBVec3 Forward => Vec3.Forward;
    public static MBVec3 Up => Vec3.Up;
    public static MBVec3 One => Vec3.One;
    public static MBVec3 Zero => Vec3.Zero;
    public static MBVec3 Invalid => Vec3.Invalid;

    public float X => UnwrappedStruct.X;

    public uint ToARGB => UnwrappedStruct.ToARGB;

    public MBVec2 AsVec2 => UnwrappedStruct.AsVec2;

    public bool IsNonZero => UnwrappedStruct.IsNonZero;

    public bool IsUnit => UnwrappedStruct.IsUnit;

    public bool IsValidXYZW => UnwrappedStruct.IsValidXYZW;

    public bool IsValid => UnwrappedStruct.IsValid;

    public float LengthSquared => UnwrappedStruct.LengthSquared;

    public float Length => UnwrappedStruct.Length;

    public float Z => UnwrappedStruct.Z;

    public float Y => UnwrappedStruct.Y;

    public float RotationZ => UnwrappedStruct.RotationZ;

    public float RotationX => UnwrappedStruct.RotationX;

    public float this[int i] => UnwrappedStruct[i];

    public static MBVec3 Abs(MBVec3 vec) => Vec3.Abs(vec);
    public static float AngleBetweenTwoVectors(MBVec3 v1, MBVec3 v2) => Vec3.AngleBetweenTwoVectors(v1, v2);
    public static MBVec3 CrossProduct(MBVec3 va, MBVec3 vb) => Vec3.CrossProduct(va, vb);
    public static float DotProduct(MBVec3 v1, MBVec3 v2) => Vec3.DotProduct(v1, v2);
    public static MBVec3 Lerp(MBVec3 v1, MBVec3 v2, float alpha) => Vec3.Lerp(v1, v2, alpha);
    public static MBVec3 Parse(string input) => Vec3.Parse(input);
    public static MBVec3 Slerp(MBVec3 start, MBVec3 end, float percent) => Vec3.Slerp(start, end, percent);
    public static MBVec3 Vec3Max(MBVec3 v1, MBVec3 v2) => Vec3.Vec3Max(v1, v2);
    public static MBVec3 Vec3Min(MBVec3 v1, MBVec3 v2) => Vec3.Vec3Min(v1, v2);

    public MBVec3 ClampedCopy(float min, float max) => UnwrappedStruct.ClampedCopy(min, max);

    public MBVec3 ClampedCopy(float min, float max, out bool valueClamped) => UnwrappedStruct.ClampedCopy(min, max, out valueClamped);

    public float Distance(MBVec3 v) => UnwrappedStruct.Distance(v);

    public float DistanceSquared(MBVec3 v) => UnwrappedStruct.DistanceSquared(v);

    public bool NearlyEquals(MBVec3 v, float epsilon = 1E-05F) => UnwrappedStruct.NearlyEquals(v, epsilon);

    public float Normalize() => UnwrappedStruct.Normalize();

    public MBVec3 NormalizedCopy() => UnwrappedStruct.NormalizedCopy();

    public void NormalizeWithoutChangingZ()
    {
      UnwrappedStruct.NormalizeWithoutChangingZ();
    }

    public MBVec3 ProjectOnUnitVector(MBVec3 ov) => UnwrappedStruct.ProjectOnUnitVector(ov);

    public MBVec3 Reflect(MBVec3 normal) => UnwrappedStruct.Reflect(normal);

    public MBVec3 RotateAboutAnArbitraryVector(MBVec3 vec, float a) => UnwrappedStruct.RotateAboutAnArbitraryVector(vec, a);

    public void RotateAboutX(float a)
    {
      UnwrappedStruct.RotateAboutX(a);
    }

    public void RotateAboutY(float a)
    {
      UnwrappedStruct.RotateAboutY(a);
    }

    public void RotateAboutZ(float a)
    {
      UnwrappedStruct.RotateAboutZ(a);
    }

    public static implicit operator Vec3(MBVec3 wrapper) => wrapper.UnwrappedStruct;
    public static implicit operator MBVec3(Vec3 unwrapped) => new MBVec3() { UnwrappedStruct = unwrapped };
  }
}
