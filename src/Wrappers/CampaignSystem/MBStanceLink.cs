using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBStanceLink : MBWrapperBase<MBStanceLink, StanceLink>
  {
    public static implicit operator StanceLink(MBStanceLink wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBStanceLink(StanceLink obj) => MBStanceLink.GetWrapperFor(obj);
  }

  public class MBStanceLinkList : MBListBase<MBStanceLink, MBStanceLinkList>
  {
    public MBStanceLinkList(IEnumerable<MBStanceLink> wrappers) => AddRange(wrappers);
    public MBStanceLinkList(MBStanceLink wrapper) => Add(wrapper);
    public MBStanceLinkList() { }

    public static implicit operator List<StanceLink>(MBStanceLinkList wrapperList) => wrapperList.Unwrap<MBStanceLink, StanceLink>();
    public static implicit operator MBStanceLinkList(List<StanceLink> objectList) => (MBStanceLinkList)objectList.Wrap<MBStanceLink, StanceLink>();
  }
}
