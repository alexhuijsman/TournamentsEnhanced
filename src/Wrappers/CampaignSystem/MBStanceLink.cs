using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBStanceLink : MBWrapperBase<MBStanceLink, StanceLink>
  {
    public static implicit operator StanceLink(MBStanceLink wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBStanceLink(StanceLink obj) => MBStanceLink.GetWrapper(obj);
  }
}
