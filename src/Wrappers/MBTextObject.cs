using TaleWorlds.Localization;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBTextObject : CachedWrapperBase<MBTextObject, TextObject>
  {
    public static implicit operator TextObject(MBTextObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTextObject(TextObject obj) => MBTextObject.GetWrapperFor(obj);
  }
}
