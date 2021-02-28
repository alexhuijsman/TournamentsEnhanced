using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBMapEvent : MBWrapperBase<MBMapEvent, MapEvent>
  {
    public static implicit operator MapEvent(MBMapEvent wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBMapEvent(MapEvent obj) => GetWrapper(obj);
  }
}
