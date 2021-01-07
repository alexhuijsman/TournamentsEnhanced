using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBWorkshop : MBWrapperBase<MBWorkshop, Workshop>
  {
    public static implicit operator Workshop(MBWorkshop wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBWorkshop(Workshop obj) => MBWorkshop.GetWrapperFor(obj);
  }

  public class MBWorkshopList : MBListBase<MBWorkshop, MBWorkshopList>
  {
    public MBWorkshopList(params MBWorkshop[] wrappers) : this((IEnumerable<MBWorkshop>)wrappers) { }
    public MBWorkshopList(IEnumerable<MBWorkshop> wrappers) => AddRange(wrappers);
    public MBWorkshopList(MBWorkshop wrapper) => Add(wrapper);
    public MBWorkshopList() { }

    public static implicit operator List<Workshop>(MBWorkshopList wrapperList) => wrapperList.Unwrap<MBWorkshop, Workshop>();
    public static implicit operator MBWorkshopList(List<Workshop> objectList) => (MBWorkshopList)objectList.Wrap<MBWorkshop, Workshop>();
    public static implicit operator MBWorkshop[](MBWorkshopList wrapperList) => wrapperList.ToArray();
  }
}
