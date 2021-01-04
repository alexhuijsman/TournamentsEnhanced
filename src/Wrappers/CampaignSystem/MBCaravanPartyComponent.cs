using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCaravanPartyComponent : MBWrapperBase<MBCaravanPartyComponent, CaravanPartyComponent>
  {
    public static implicit operator  CaravanPartyComponent(MBCaravanPartyComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCaravanPartyComponent(CaravanPartyComponent obj) => MBCaravanPartyComponent.GetWrapperFor(obj);
  }

  public class MBCaravanPartyComponentList : MBListBase<MBCaravanPartyComponent,MBCaravanPartyComponentList>
  {
    public static implicit operator List<CaravanPartyComponent>(MBCaravanPartyComponentList wrapperList) => wrapperList.Unwrap<MBCaravanPartyComponent, CaravanPartyComponent>();
    public static implicit operator MBCaravanPartyComponentList(List<CaravanPartyComponent> objectList) => (MBCaravanPartyComponentList)objectList.Wrap<MBCaravanPartyComponent, CaravanPartyComponent>();
  }
}
