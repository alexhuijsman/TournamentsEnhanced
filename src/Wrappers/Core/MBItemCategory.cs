using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBItemCategory : MBObjectBaseWrapper<MBItemCategory, ItemCategory>
  {
    public static implicit operator ItemCategory(MBItemCategory wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBItemCategory(ItemCategory obj) => MBItemCategory.GetWrapperFor(obj);
  }

  public class MBItemCategoryList : MBListBase<MBItemCategory, MBItemCategoryList>
  {
    public static implicit operator List<ItemCategory>(MBItemCategoryList wrapperList) => wrapperList.Unwrap<MBItemCategory, ItemCategory>();
    public static implicit operator MBItemCategoryList(List<ItemCategory> objectList) => (MBItemCategoryList)objectList.Wrap<MBItemCategory, ItemCategory>();
  }
}
