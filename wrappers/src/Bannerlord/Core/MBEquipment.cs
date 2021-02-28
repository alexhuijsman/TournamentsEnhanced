using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBEquipment : MBWrapperBase<MBEquipment, Equipment>
  {
    public static implicit operator Equipment(MBEquipment wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBEquipment(Equipment obj) => GetWrapper(obj);
  }
}
