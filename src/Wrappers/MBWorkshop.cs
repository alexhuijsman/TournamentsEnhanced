using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBWorkshop : CachedWrapperBase<MBWorkshop, Workshop>
  {
    public static implicit operator Workshop(MBWorkshop wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWorkshop(Workshop obj) => MBWorkshop.GetWrapperFor(obj);
  }
}
