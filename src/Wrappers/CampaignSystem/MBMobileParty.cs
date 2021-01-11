using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBMobileParty : MBObjectBaseWrapper<MBMobileParty, MobileParty>
  {
    public static implicit operator MobileParty(MBMobileParty wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMobileParty(MobileParty obj) => MBMobileParty.GetWrapper(obj);
  }
}
