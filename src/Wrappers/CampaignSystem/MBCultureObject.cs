using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCultureObject : MBObjectBaseWrapper<MBCultureObject, CultureObject>
  {
    public static implicit operator CultureObject(MBCultureObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCultureObject(CultureObject obj) => MBCultureObject.GetWrapper(obj);
  }

  public class MBCultureObjectList : MBListBase<MBCultureObject, MBCultureObjectList>
  {
    public MBCultureObjectList(params MBCultureObject[] wrappers) : this((IEnumerable<MBCultureObject>)wrappers) { }
    public MBCultureObjectList(IEnumerable<MBCultureObject> wrappers) => AddRange(wrappers);
    public MBCultureObjectList(MBCultureObject wrapper) => Add(wrapper);
    public MBCultureObjectList() { }

    public static implicit operator List<CultureObject>(MBCultureObjectList wrapperList) => wrapperList.Unwrap<MBCultureObject, CultureObject>();
    public static implicit operator MBCultureObjectList(List<CultureObject> objectList) => (MBCultureObjectList)objectList.Wrap<MBCultureObject, CultureObject>();
    public static implicit operator MBCultureObject[](MBCultureObjectList wrapperList) => wrapperList.ToArray();
  }
}
