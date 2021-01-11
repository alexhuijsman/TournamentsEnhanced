using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCaravanPartyComponent : MBWrapperBase<MBCaravanPartyComponent, CaravanPartyComponent>
  {
    public static implicit operator CaravanPartyComponent(MBCaravanPartyComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCaravanPartyComponent(CaravanPartyComponent obj) => MBCaravanPartyComponent.GetWrapper(obj);
  }
}
