using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBBasicCultureObject : MBObjectBaseWrapper<MBBasicCultureObject, BasicCultureObject>
  {
    public static implicit operator BasicCultureObject(MBBasicCultureObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBasicCultureObject(BasicCultureObject obj) => GetWrapper(obj);
  }
}
