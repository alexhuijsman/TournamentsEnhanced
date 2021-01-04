using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCommonArea : MBWrapperBase<MBCommonArea, CommonArea>
  {
    public static implicit operator  CommonArea(MBCommonArea wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCommonArea(CommonArea obj) => MBCommonArea.GetWrapperFor(obj);
  }

  public class MBCommonAreaList : MBListBase<MBCommonArea,MBCommonAreaList>
  {
    public static implicit operator List<CommonArea>(MBCommonAreaList wrapperList) => wrapperList.Unwrap<MBCommonArea, CommonArea>();
    public static implicit operator MBCommonAreaList(List<CommonArea> objectList) => (MBCommonAreaList)objectList.Wrap<MBCommonArea, CommonArea>();
  }
}
