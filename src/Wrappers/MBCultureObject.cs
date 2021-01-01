using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBCultureObject : CachedWrapperBase<MBCultureObject, CultureObject>
  {
    public static implicit operator CultureObject(MBCultureObject wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCultureObject(CultureObject obj) => MBCultureObject.GetWrapperFor(obj);
  }
}
