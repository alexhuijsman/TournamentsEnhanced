using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBPartyBase : MBWrapperBase<MBPartyBase, PartyBase>
  {
    public static implicit operator PartyBase(MBPartyBase wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBPartyBase(PartyBase obj) => MBPartyBase.GetWrapperFor(obj);
  }

  public class MBPartyBaseList : MBListBase<MBPartyBase, MBPartyBaseList>
  {
    public MBPartyBaseList(params MBPartyBase[] wrappers) : this((IEnumerable<MBPartyBase>)wrappers) { }
    public MBPartyBaseList(IEnumerable<MBPartyBase> wrappers) => AddRange(wrappers);
    public MBPartyBaseList(MBPartyBase wrapper) => Add(wrapper);
    public MBPartyBaseList() { }

    public static implicit operator List<PartyBase>(MBPartyBaseList wrapperList) => wrapperList.Unwrap<MBPartyBase, PartyBase>();
    public static implicit operator MBPartyBaseList(List<PartyBase> objectList) => (MBPartyBaseList)objectList.Wrap<MBPartyBase, PartyBase>();
  }
}
