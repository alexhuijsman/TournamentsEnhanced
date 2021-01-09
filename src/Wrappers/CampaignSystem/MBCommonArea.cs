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

  public class MBCommonAreaList : MBListBase<MBCommonArea, MBCommonAreaList>
  {
    public MBCommonAreaList(params MBCommonArea[] wrappers) : this((IEnumerable<MBCommonArea>)wrappers) { }
    public MBCommonAreaList(IEnumerable<MBCommonArea> wrappers) => AddRange(wrappers);
    public MBCommonAreaList(MBCommonArea wrapper) => Add(wrapper);
    public MBCommonAreaList() { }

    public static implicit operator List<CommonArea>(MBCommonAreaList wrapperList) => wrapperList.Unwrap<MBCommonArea, CommonArea>();
    public static implicit operator MBCommonAreaList(List<CommonArea> objectList) => (MBCommonAreaList)objectList.Wrap<MBCommonArea, CommonArea>();
    public static implicit operator MBCommonArea[](MBCommonAreaList wrapperList) => wrapperList.ToArray();
  }
}
