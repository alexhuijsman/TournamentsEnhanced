using TaleWorlds.Library;

namespace TournamentsEnhanced.Wrappers.Library
{
  public struct MBVec2 : IMBVec2
  {
    public float this[int i] => UnwrappedStruct[i];

    public Vec2 UnwrappedStruct { get; set; }

    public float LengthSquared => UnwrappedStruct.LengthSquared;

    public float Length => UnwrappedStruct.Length;

    public float Y => UnwrappedStruct.Y;

    public float X => UnwrappedStruct.X;

    public float RotationInRadians => UnwrappedStruct.RotationInRadians;

    public bool IsValid => UnwrappedStruct.IsValid;

    public float AngleBetween(MBVec2 vector2)
    {
      return UnwrappedStruct.AngleBetween(vector2);
    }

    public float Distance(MBVec2 v)
    {
      return UnwrappedStruct.Distance(v);
    }

    public float DistanceSquared(MBVec2 v)
    {
      return UnwrappedStruct.DistanceSquared(v);
    }

    public float DotProduct(MBVec2 v)
    {
      return UnwrappedStruct.DotProduct(v);
    }

    public bool IsNonZero()
    {
      return UnwrappedStruct.IsNonZero();
    }

    public bool IsUnit()
    {
      return UnwrappedStruct.IsUnit();
    }

    public MBVec2 LeftVec()
    {
      return UnwrappedStruct.LeftVec();
    }

    public bool NearlyEquals(MBVec2 v, float epsilon = 1E-05F)
    {
      return UnwrappedStruct.NearlyEquals(v, epsilon);
    }

    public float Normalize()
    {
      return UnwrappedStruct.Normalize();
    }

    public MBVec2 Normalized()
    {
      return UnwrappedStruct.Normalized();
    }

    public MBVec2 RightVec()
    {
      return UnwrappedStruct.RightVec();
    }

    public void RotateCCW(float angleInRadians)
    {
      UnwrappedStruct.RotateCCW(angleInRadians);
    }

    public MBVec3 ToVec3(float z = 0)
    {
      return UnwrappedStruct.ToVec3(z);
    }

    public MBVec2 TransformToLocalUnitF(MBVec2 a)
    {
      return UnwrappedStruct.TransformToLocalUnitF(a);
    }

    public MBVec2 TransformToLocalUnitFLeftHanded(MBVec2 a)
    {
      return UnwrappedStruct.TransformToLocalUnitFLeftHanded(a);
    }

    public MBVec2 TransformToParentUnitF(MBVec2 a)
    {
      return UnwrappedStruct.TransformToParentUnitF(a);
    }

    public MBVec2 TransformToParentUnitFLeftHanded(MBVec2 a)
    {
      return UnwrappedStruct.TransformToParentUnitFLeftHanded(a);
    }

    public static implicit operator Vec2(MBVec2 wrapper) => wrapper.UnwrappedStruct;
    public static implicit operator MBVec2(Vec2 unwrapped) => new MBVec2() { UnwrappedStruct = unwrapped };
  }
}
