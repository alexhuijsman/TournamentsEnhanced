using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBItemCategory : CachedWrapperBase<MBItemCategory, ItemCategory>
  {
    public static implicit operator ItemCategory(MBItemCategory wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemCategory(ItemCategory obj) => MBItemCategory.GetWrapperFor(obj);
  }
}
