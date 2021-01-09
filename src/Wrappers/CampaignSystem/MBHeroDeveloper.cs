using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBHeroDeveloper : MBWrapperBase<MBHeroDeveloper, HeroDeveloper>
  {
    public static implicit operator HeroDeveloper(MBHeroDeveloper wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBHeroDeveloper(HeroDeveloper obj) => MBHeroDeveloper.GetWrapper(obj);
  }

  public class MBHeroDeveloperList : MBListBase<MBHeroDeveloper, MBHeroDeveloperList>
  {
    public MBHeroDeveloperList(params MBHeroDeveloper[] wrappers) : this((IEnumerable<MBHeroDeveloper>)wrappers) { }
    public MBHeroDeveloperList(IEnumerable<MBHeroDeveloper> wrappers) => AddRange(wrappers);
    public MBHeroDeveloperList(MBHeroDeveloper wrapper) => Add(wrapper);
    public MBHeroDeveloperList() { }

    public static implicit operator List<HeroDeveloper>(MBHeroDeveloperList wrapperList) => wrapperList.Unwrap<MBHeroDeveloper, HeroDeveloper>();
    public static implicit operator MBHeroDeveloperList(List<HeroDeveloper> objectList) => (MBHeroDeveloperList)objectList.Wrap<MBHeroDeveloper, HeroDeveloper>();
    public static implicit operator MBHeroDeveloper[](MBHeroDeveloperList wrapperList) => wrapperList.ToArray();
  }
}
