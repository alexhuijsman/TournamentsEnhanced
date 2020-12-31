using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Wrappers
{
  public class MBCultureObject : CachedWrapper<MBCultureObject, CultureObject>
  {
    public static implicit operator CultureObject(MBCultureObject wrapper) => wrapper.Unwrap();
    public static implicit operator MBCultureObject(CultureObject obj) => MBCultureObject.GetWrapperFor(obj);
  }
}
