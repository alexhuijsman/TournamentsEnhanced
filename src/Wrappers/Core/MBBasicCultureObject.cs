using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBBasicCultureObject : MBObjectBaseWrapper<MBBasicCultureObject, BasicCultureObject>
  {
    public static implicit operator BasicCultureObject(MBBasicCultureObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBasicCultureObject(BasicCultureObject obj) => MBBasicCultureObject.GetWrapperFor(obj);
  }

  public class MBBasicCultureObjectList : MBListBase<MBBasicCultureObject, MBBasicCultureObjectList>
  {
    public MBBasicCultureObjectList(params MBBasicCultureObject[] wrappers) : this((IEnumerable<MBBasicCultureObject>)wrappers) { }
    public MBBasicCultureObjectList(IEnumerable<MBBasicCultureObject> wrappers) => AddRange(wrappers);
    public MBBasicCultureObjectList(MBBasicCultureObject wrapper) => Add(wrapper);
    public MBBasicCultureObjectList() { }

    public static implicit operator List<BasicCultureObject>(MBBasicCultureObjectList wrapperList) => wrapperList.Unwrap<MBBasicCultureObject, BasicCultureObject>();
    public static implicit operator MBBasicCultureObjectList(List<BasicCultureObject> objectList) => (MBBasicCultureObjectList)objectList.Wrap<MBBasicCultureObject, BasicCultureObject>();
  }
}
