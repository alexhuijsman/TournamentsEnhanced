using TaleWorlds.Library;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Library
{
  public interface IMBVec2 : IStructWrapperBase<Vec2>
  {
    float this[int i] { get; }

    float LengthSquared { get; }
    float Length { get; }
    float Y { get; }
    float X { get; }
    float RotationInRadians { get; }
    bool IsValid { get; }

    float AngleBetween(MBVec2 vector2);
    float Distance(MBVec2 v);
    float DistanceSquared(MBVec2 v);
    float DotProduct(MBVec2 v);
    bool IsNonZero();
    bool IsUnit();
    MBVec2 LeftVec();
    bool NearlyEquals(MBVec2 v, float epsilon = 1E-05F);
    float Normalize();
    MBVec2 Normalized();
    MBVec2 RightVec();
    void RotateCCW(float angleInRadians);
    MBVec3 ToVec3(float z = 0);
    MBVec2 TransformToLocalUnitF(MBVec2 a);
    MBVec2 TransformToLocalUnitFLeftHanded(MBVec2 a);
    MBVec2 TransformToParentUnitF(MBVec2 a);
    MBVec2 TransformToParentUnitFLeftHanded(MBVec2 a);
  }
}