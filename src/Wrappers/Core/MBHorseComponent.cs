using System.Collections.Generic;

using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.Core
{
  public class MBHorseComponent : MBObjectBaseWrapper<MBHorseComponent, HorseComponent>
  {
    public static implicit operator HorseComponent(MBHorseComponent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBHorseComponent(HorseComponent obj) => MBHorseComponent.GetWrapperFor(obj);
  }

  public class MBHorseComponentList : MBListBase<MBHorseComponent, MBHorseComponentList>
  {
    public MBHorseComponentList(params MBHorseComponent[] wrappers) : this((IEnumerable<MBHorseComponent>)wrappers) { }
    public MBHorseComponentList(IEnumerable<MBHorseComponent> wrappers) => AddRange(wrappers);
    public MBHorseComponentList(MBHorseComponent wrapper) => Add(wrapper);
    public MBHorseComponentList() { }

    public static implicit operator List<HorseComponent>(MBHorseComponentList wrapperList) => wrapperList.Unwrap<MBHorseComponent, HorseComponent>();
    public static implicit operator MBHorseComponentList(List<HorseComponent> objectList) => (MBHorseComponentList)objectList.Wrap<MBHorseComponent, HorseComponent>();
  }
}
