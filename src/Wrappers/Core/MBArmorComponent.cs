using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBArmorComponent : MBObjectBaseWrapper<MBArmorComponent, ArmorComponent>
  {
    public static implicit operator ArmorComponent(MBArmorComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBArmorComponent(ArmorComponent obj) => MBArmorComponent.GetWrapper(obj);
  }
}
