using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTraitObject : MBWrapperBase<MBTraitObject, TraitObject>
  {
    public static implicit operator TraitObject(MBTraitObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTraitObject(TraitObject obj) => MBTraitObject.GetWrapperFor(obj);
  }

  public class MBTraitObjectList : MBListBase<MBTraitObject, MBTraitObjectList>
  {
    public MBTraitObjectList(params MBTraitObject[] wrappers) : this((IEnumerable<MBTraitObject>)wrappers) { }
    public MBTraitObjectList(IEnumerable<MBTraitObject> wrappers) => AddRange(wrappers);
    public MBTraitObjectList(MBTraitObject wrapper) => Add(wrapper);
    public MBTraitObjectList() { }

    public static implicit operator List<TraitObject>(MBTraitObjectList wrapperList) => wrapperList.Unwrap<MBTraitObject, TraitObject>();
    public static implicit operator MBTraitObjectList(List<TraitObject> objectList) => (MBTraitObjectList)objectList.Wrap<MBTraitObject, TraitObject>();
    public static implicit operator MBTraitObject[](MBTraitObjectList wrapperList) => wrapperList.ToArray();
  }
}
