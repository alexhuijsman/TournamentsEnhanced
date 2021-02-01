using TaleWorlds.Library;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Library
{
  public interface IMBVec3 : IStructWrapperBase<Vec3>
  {
    float this[int i] { get; }

    float X { get; }
    uint ToARGB { get; }
    MBVec2 AsVec2 { get; }
    bool IsNonZero { get; }
    bool IsUnit { get; }
    bool IsValidXYZW { get; }
    bool IsValid { get; }
    float LengthSquared { get; }
    float Length { get; }
    float Z { get; }
    float Y { get; }
    float RotationZ { get; }
    float RotationX { get; }

    MBVec3 ClampedCopy(float min, float max);
    MBVec3 ClampedCopy(float min, float max, out bool valueClamped);
    float Distance(MBVec3 v);
    float DistanceSquared(MBVec3 v);
    bool NearlyEquals(MBVec3 v, float epsilon = 1E-05F);
    float Normalize();
    MBVec3 NormalizedCopy();
    void NormalizeWithoutChangingZ();
    MBVec3 ProjectOnUnitVector(MBVec3 ov);
    MBVec3 Reflect(MBVec3 normal);
    MBVec3 RotateAboutAnArbitraryVector(MBVec3 vec, float a);
    void RotateAboutX(float a);
    void RotateAboutY(float a);
    void RotateAboutZ(float a);
  }
}