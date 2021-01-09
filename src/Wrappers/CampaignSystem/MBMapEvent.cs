using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBMapEvent : MBWrapperBase<MBMapEvent, MapEvent>
  {
    public static implicit operator MapEvent(MBMapEvent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMapEvent(MapEvent obj) => MBMapEvent.GetWrapperFor(obj);
  }

  public class MBMapEventList : MBListBase<MBMapEvent, MBMapEventList>
  {
    public MBMapEventList(params MBMapEvent[] wrappers) : this((IEnumerable<MBMapEvent>)wrappers) { }
    public MBMapEventList(IEnumerable<MBMapEvent> wrappers) => AddRange(wrappers);
    public MBMapEventList(MBMapEvent wrapper) => Add(wrapper);
    public MBMapEventList() { }

    public static implicit operator List<MapEvent>(MBMapEventList wrapperList) => wrapperList.Unwrap<MBMapEvent, MapEvent>();
    public static implicit operator MBMapEventList(List<MapEvent> objectList) => (MBMapEventList)objectList.Wrap<MBMapEvent, MapEvent>();
    public static implicit operator MBMapEvent[](MBMapEventList wrapperList) => wrapperList.ToArray();
  }
}
