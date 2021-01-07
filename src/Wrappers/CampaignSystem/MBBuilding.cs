using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBBuilding : MBWrapperBase<MBBuilding, Building>
  {
    public static implicit operator Building(MBBuilding wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBBuilding(Building obj) => MBBuilding.GetWrapperFor(obj);
  }

  public class MBBuildingList : MBListBase<MBBuilding, MBBuildingList>
  {
    public MBBuildingList(params MBBuilding[] wrappers) : this((IEnumerable<MBBuilding>)wrappers) { }
    public MBBuildingList(IEnumerable<MBBuilding> wrappers) => AddRange(wrappers);
    public MBBuildingList(MBBuilding wrapper) => Add(wrapper);
    public MBBuildingList() { }

    public static implicit operator List<Building>(MBBuildingList wrapperList) => wrapperList.Unwrap<MBBuilding, Building>();
    public static implicit operator MBBuildingList(List<Building> objectList) => (MBBuildingList)objectList.Wrap<MBBuilding, Building>();
    public static implicit operator MBBuilding[](MBBuildingList wrapperList) => wrapperList.ToArray();
  }
}
