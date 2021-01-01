using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBMobileParty : CachedWrapperBase<MBMobileParty, MobileParty>
  {
    public static implicit operator MobileParty(MBMobileParty wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMobileParty(MobileParty obj) => MBMobileParty.GetWrapperFor(obj);
  }
}
