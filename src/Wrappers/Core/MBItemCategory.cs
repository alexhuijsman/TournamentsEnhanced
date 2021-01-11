using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBItemCategory : MBObjectBaseWrapper<MBItemCategory, ItemCategory>
  {
    public static implicit operator ItemCategory(MBItemCategory wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemCategory(ItemCategory obj) => MBItemCategory.GetWrapper(obj);
  }
}
