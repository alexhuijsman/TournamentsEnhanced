using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBBasicCharacterObject : MBWrapperBase<MBBasicCharacterObject, BasicCharacterObject>
  {
    public static implicit operator BasicCharacterObject(MBBasicCharacterObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBasicCharacterObject(BasicCharacterObject obj) => GetWrapper(obj);
  }
}
