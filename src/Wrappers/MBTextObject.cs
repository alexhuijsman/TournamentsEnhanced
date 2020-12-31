using TaleWorlds.Localization;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTextObject : CachedWrapper<MBTextObject, TextObject>
  {
    public static implicit operator TextObject(MBTextObject wrapper) => wrapper.Unwrap();
    public static implicit operator MBTextObject(TextObject obj) => MBTextObject.GetWrapperFor(obj);
  }
}
