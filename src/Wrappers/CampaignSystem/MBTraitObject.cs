using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTraitObject : MBWrapperBase<MBTraitObject, TraitObject>
  {
    public static implicit operator  TraitObject(MBTraitObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTraitObject(TraitObject obj) => MBTraitObject.GetWrapperFor(obj);
  }

  public class MBTraitObjectList : MBListBase<MBTraitObject,MBTraitObjectList>
  {
    public static implicit operator List<TraitObject>(MBTraitObjectList wrapperList) => wrapperList.Unwrap<MBTraitObject, TraitObject>();
    public static implicit operator MBTraitObjectList(List<TraitObject> objectList) => (MBTraitObjectList)objectList.Wrap<MBTraitObject, TraitObject>();
  }
}
