using TaleWorlds.Library;

namespace TournamentsEnhanced.Wrappers.Library
{
  public struct MBColor : IMBColor
  {
    public Color UnwrappedStruct { get; set; }

    public static MBColor ConvertStringToColor(string color) => Color.ConvertStringToColor(color);
    public static MBColor FromUint(uint color) => Color.FromUint(color);
    public static MBColor Lerp(MBColor start, MBColor end, float ratio) => Color.Lerp(start, end, ratio);
    public static string UIntToColorString(uint color) => Color.UIntToColorString(color);

    public float Length()
    {
      return UnwrappedStruct.Length();
    }

    public uint ToUnsignedInteger()
    {
      return UnwrappedStruct.ToUnsignedInteger();
    }

    public MBVec3 ToVec3()
    {
      return UnwrappedStruct.ToVec3();
    }

    public static implicit operator Color(MBColor wrapper) => wrapper.UnwrappedStruct;
    public static implicit operator MBColor(Color unwrapped) => new MBColor() { UnwrappedStruct = unwrapped };
  }
}
