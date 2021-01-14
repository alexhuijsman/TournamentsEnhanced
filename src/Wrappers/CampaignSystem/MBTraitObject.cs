using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBTraitObject : MBWrapperBase<MBTraitObject, TraitObject>
  {
    public static implicit operator TraitObject(MBTraitObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBTraitObject(TraitObject obj) => GetWrapper(obj);
  }
}
