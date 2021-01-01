using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBBasicCultureObject : CachedWrapperBase<MBBasicCultureObject, BasicCultureObject>
  {
    public static implicit operator BasicCultureObject(MBBasicCultureObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBasicCultureObject(BasicCultureObject obj) => MBBasicCultureObject.GetWrapperFor(obj);
  }

  public class MBBasicCultureObjectList : List<MBBasicCultureObject>
  {
    public static implicit operator List<BasicCultureObject>(MBBasicCultureObjectList wrapperList) => wrapperList.Unwrap<MBBasicCultureObject, BasicCultureObject>();
    public static implicit operator MBBasicCultureObjectList(List<BasicCultureObject> objectList) => (MBBasicCultureObjectList)objectList.Wrap<MBBasicCultureObject, BasicCultureObject>();
  }
}
