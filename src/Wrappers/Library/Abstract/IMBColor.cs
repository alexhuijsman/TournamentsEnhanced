using TaleWorlds.Library;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Library
{
  public interface IMBColor : IStructWrapperBase<Color>
  {
    float Length();
    uint ToUnsignedInteger();
    MBVec3 ToVec3();
  }
}