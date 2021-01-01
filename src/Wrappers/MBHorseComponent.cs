using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBHorseComponent : CachedWrapperBase<MBHorseComponent, HorseComponent>
  {
    public static implicit operator HorseComponent(MBHorseComponent wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBHorseComponent(HorseComponent obj) => MBHorseComponent.GetWrapperFor(obj);
  }

  public class MBHorseComponentList : List<MBHorseComponent>
  {
    public static implicit operator List<HorseComponent>(MBHorseComponentList wrapperList) => wrapperList.Unwrap<MBHorseComponent, HorseComponent>();
    public static implicit operator MBHorseComponentList(List<HorseComponent> objectList) => (MBHorseComponentList)objectList.Wrap<MBHorseComponent, HorseComponent>();
  }
}
