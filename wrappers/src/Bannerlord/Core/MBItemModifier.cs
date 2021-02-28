using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBItemModifier : MBWrapperBase<MBItemModifier, ItemModifier>
  {
    public static implicit operator ItemModifier(MBItemModifier wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemModifier(ItemModifier obj) => GetWrapper(obj);
  }
}
