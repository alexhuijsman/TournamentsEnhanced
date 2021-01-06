using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBMobileParty : MBObjectBaseWrapper<MBMobileParty, MobileParty>
  {
    public static implicit operator MobileParty(MBMobileParty wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMobileParty(MobileParty obj) => MBMobileParty.GetWrapperFor(obj);
  }

  public class MBMobilePartyList : MBListBase<MBMobileParty, MBMobilePartyList>
  {
    public MBMobilePartyList(IEnumerable<MBMobileParty> wrappers) => AddRange(wrappers);
    public MBMobilePartyList(MBMobileParty wrapper) => Add(wrapper);
    public MBMobilePartyList() { }

    public static implicit operator List<MobileParty>(MBMobilePartyList wrapperList) => wrapperList.Unwrap<MBMobileParty, MobileParty>();
    public static implicit operator MBMobilePartyList(List<MobileParty> objectList) => (MBMobilePartyList)objectList.Wrap<MBMobileParty, MobileParty>();
  }
}
