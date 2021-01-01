using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBWorkshop : CachedWrapperBase<MBWorkshop, Workshop>
  {
    public static implicit operator Workshop(MBWorkshop wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWorkshop(Workshop obj) => MBWorkshop.GetWrapperFor(obj);
  }

  public class MBWorkshopList : List<MBWorkshop>
  {
    public static implicit operator List<Workshop>(MBWorkshopList wrapperList) => wrapperList.Unwrap<MBWorkshop, Workshop>();
    public static implicit operator MBWorkshopList(List<Workshop> objectList) => (MBWorkshopList)objectList.Wrap<MBWorkshop, Workshop>();
  }
}
