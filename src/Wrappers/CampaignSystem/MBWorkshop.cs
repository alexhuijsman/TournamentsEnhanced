using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBWorkshop : MBWrapperBase<MBWorkshop, Workshop>
  {
    public static implicit operator Workshop(MBWorkshop wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWorkshop(Workshop obj) => GetWrapper(obj);
  }
}
