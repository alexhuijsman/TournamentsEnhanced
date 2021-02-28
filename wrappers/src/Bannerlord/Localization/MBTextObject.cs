using System.Collections.Generic;

using TaleWorlds.Localization;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Localization
{
  public class MBTextObject : MBWrapperBase<MBTextObject, TextObject>
  {
    public MBTextObject(string message)
    {
      UnwrappedObject = new TextObject(message);
    }
    public MBTextObject() { }
    public static implicit operator TextObject(MBTextObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTextObject(TextObject obj) => GetWrapper(obj);
  }
}
