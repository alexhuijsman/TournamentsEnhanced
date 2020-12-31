using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Wrappers
{
  public class MBMobileParty : CachedWrapper<MBMobileParty, MobileParty>
  {
    public static implicit operator MobileParty(MBMobileParty wrapper) => wrapper.Unwrap();
    public static implicit operator MBMobileParty(MobileParty obj) => MBMobileParty.GetWrapperFor(obj);
  }
}
