using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBBuilding : CachedWrapperBase<MBBuilding, Building>
  {
    public static implicit operator Building(MBBuilding wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBuilding(Building obj) => MBBuilding.GetWrapperFor(obj);
  }
}
