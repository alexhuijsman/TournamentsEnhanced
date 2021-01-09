using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBVillage : MBWrapperBase<MBVillage, Village>
  {
    public static implicit operator Village(MBVillage wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBVillage(Village obj) => MBVillage.GetWrapper(obj);
  }

  public class MBVillageList : MBListBase<MBVillage, MBVillageList>
  {
    public MBVillageList(params MBVillage[] wrappers) : this((IEnumerable<MBVillage>)wrappers) { }
    public MBVillageList(IEnumerable<MBVillage> wrappers) => AddRange(wrappers);
    public MBVillageList(MBVillage wrapper) => Add(wrapper);
    public MBVillageList() { }

    public static implicit operator List<Village>(MBVillageList wrapperList) => wrapperList.Unwrap<MBVillage, Village>();
    public static implicit operator MBVillageList(List<Village> objectList) => (MBVillageList)objectList.Wrap<MBVillage, Village>();
    public static implicit operator MBVillage[](MBVillageList wrapperList) => wrapperList.ToArray();
  }
}
