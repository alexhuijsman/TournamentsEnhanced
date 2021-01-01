using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBItemCategory : CachedWrapperBase<MBItemCategory, ItemCategory>
  {
    public static implicit operator ItemCategory(MBItemCategory wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemCategory(ItemCategory obj) => MBItemCategory.GetWrapperFor(obj);
  }

  public class MBItemCategoryList : List<MBItemCategory>
  {
    public static implicit operator List<ItemCategory>(MBItemCategoryList wrapperList) => wrapperList.Unwrap<MBItemCategory, ItemCategory>();
    public static implicit operator MBItemCategoryList(List<ItemCategory> objectList) => (MBItemCategoryList)objectList.Wrap<MBItemCategory, ItemCategory>();
  }
}
