using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCultureObject : MBObjectBaseWrapper<MBCultureObject, CultureObject>
  {
    public static implicit operator CultureObject(MBCultureObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCultureObject(CultureObject obj) => MBCultureObject.GetWrapperFor(obj);
  }

  public class MBCultureObjectList : List<MBCultureObject>
  {
    public static implicit operator List<CultureObject>(MBCultureObjectList wrapperList) => wrapperList.Unwrap<MBCultureObject, CultureObject>();
    public static implicit operator MBCultureObjectList(List<CultureObject> objectList) => (MBCultureObjectList)objectList.Wrap<MBCultureObject, CultureObject>();
  }
}
