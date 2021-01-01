using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBStanceLink : CachedWrapperBase<MBStanceLink, StanceLink>
  {
    public static implicit operator StanceLink(MBStanceLink wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBStanceLink(StanceLink obj) => MBStanceLink.GetWrapperFor(obj);
  }

  public class MBStanceLinkList : List<MBStanceLink>
  {
    public static implicit operator List<StanceLink>(MBStanceLinkList wrapperList) => wrapperList.Unwrap<MBStanceLink, StanceLink>();
    public static implicit operator MBStanceLinkList(List<StanceLink> objectList) => (MBStanceLinkList)objectList.Wrap<MBStanceLink, StanceLink>();
  }
}
