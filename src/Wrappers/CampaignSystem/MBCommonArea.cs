using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCommonArea : MBWrapperBase<MBCommonArea, CommonArea>
  {
    public static implicit operator CommonArea(MBCommonArea wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCommonArea(CommonArea obj) => MBCommonArea.GetWrapper(obj);
  }
}
