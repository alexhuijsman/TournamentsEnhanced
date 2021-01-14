using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBPerkObject : MBWrapperBase<MBPerkObject, PerkObject>
  {
    public static implicit operator PerkObject(MBPerkObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBPerkObject(PerkObject obj) => GetWrapper(obj);
  }
}
