using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBPerkObject : MBWrapperBase<MBPerkObject, PerkObject>
  {
    public static implicit operator PerkObject(MBPerkObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBPerkObject(PerkObject obj) => MBPerkObject.GetWrapper(obj);
  }

  public class MBPerkObjectList : MBListBase<MBPerkObject, MBPerkObjectList>
  {
    public MBPerkObjectList(params MBPerkObject[] wrappers) : this((IEnumerable<MBPerkObject>)wrappers) { }
    public MBPerkObjectList(IEnumerable<MBPerkObject> wrappers) => AddRange(wrappers);
    public MBPerkObjectList(MBPerkObject wrapper) => Add(wrapper);
    public MBPerkObjectList() { }

    public static implicit operator List<PerkObject>(MBPerkObjectList wrapperList) => wrapperList.Unwrap<MBPerkObject, PerkObject>();
    public static implicit operator MBPerkObjectList(List<PerkObject> objectList) => (MBPerkObjectList)objectList.Wrap<MBPerkObject, PerkObject>();
    public static implicit operator MBPerkObject[](MBPerkObjectList wrapperList) => wrapperList.ToArray();
  }
}
