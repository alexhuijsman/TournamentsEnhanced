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
    public static implicit operator MBTextObject(TextObject obj) => MBTextObject.GetWrapper(obj);
  }

  public class MBTextObjectList : MBListBase<MBTextObject, MBTextObjectList>
  {
    public MBTextObjectList(params MBTextObject[] wrappers) : this((IEnumerable<MBTextObject>)wrappers) { }
    public MBTextObjectList(IEnumerable<MBTextObject> wrappers) => AddRange(wrappers);
    public MBTextObjectList(MBTextObject wrapper) => Add(wrapper);
    public MBTextObjectList() { }

    public static implicit operator List<TextObject>(MBTextObjectList wrapperList) => wrapperList.Unwrap<MBTextObject, TextObject>();
    public static implicit operator MBTextObjectList(List<TextObject> objectList) => (MBTextObjectList)objectList.Wrap<MBTextObject, TextObject>();
    public static implicit operator MBTextObject[](MBTextObjectList wrapperList) => wrapperList.ToArray();
  }
}
