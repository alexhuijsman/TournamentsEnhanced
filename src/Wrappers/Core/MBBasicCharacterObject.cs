using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBBasicCharacterObject : MBWrapperBase<MBBasicCharacterObject, BasicCharacterObject>
  {
    public static implicit operator BasicCharacterObject(MBBasicCharacterObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBasicCharacterObject(BasicCharacterObject obj) => MBBasicCharacterObject.GetWrapper(obj);
  }

  public class MBBasicCharacterObjectList : MBListBase<MBBasicCharacterObject, MBBasicCharacterObjectList>
  {
    public MBBasicCharacterObjectList(params MBBasicCharacterObject[] wrappers) : this((IEnumerable<MBBasicCharacterObject>)wrappers) { }
    public MBBasicCharacterObjectList(IEnumerable<MBBasicCharacterObject> wrappers) => AddRange(wrappers);
    public MBBasicCharacterObjectList(MBBasicCharacterObject wrapper) => Add(wrapper);
    public MBBasicCharacterObjectList() { }

    public static implicit operator List<BasicCharacterObject>(MBBasicCharacterObjectList wrapperList) => wrapperList.Unwrap<MBBasicCharacterObject, BasicCharacterObject>();
    public static implicit operator MBBasicCharacterObjectList(List<BasicCharacterObject> objectList) => (MBBasicCharacterObjectList)objectList.Wrap<MBBasicCharacterObject, BasicCharacterObject>();
    public static implicit operator MBBasicCharacterObject[](MBBasicCharacterObjectList wrapperList) => wrapperList.ToArray();
  }
}
