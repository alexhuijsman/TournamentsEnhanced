using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBMobileParty : CachedWrapperBase<MBMobileParty, MobileParty>
  {
    public static implicit operator MobileParty(MBMobileParty wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMobileParty(MobileParty obj) => MBMobileParty.GetWrapperFor(obj);
  }

  public class MBMobilePartyList : List<MBMobileParty>
  {
    public static implicit operator List<MobileParty>(MBMobilePartyList wrapperList) => wrapperList.Unwrap<MBMobileParty, MobileParty>();
    public static implicit operator MBMobilePartyList(List<MobileParty> objectList) => (MBMobilePartyList)objectList.Wrap<MBMobileParty, MobileParty>();
  }
}
