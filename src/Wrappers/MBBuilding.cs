using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBBuilding : CachedWrapperBase<MBBuilding, Building>
  {
    public static implicit operator Building(MBBuilding wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBBuilding(Building obj) => MBBuilding.GetWrapperFor(obj);
  }

  public class MBBuildingList : List<MBBuilding>
  {
    public static implicit operator List<Building>(MBBuildingList wrapperList) => wrapperList.Unwrap<MBBuilding, Building>();
    public static implicit operator MBBuildingList(List<Building> objectList) => (MBBuildingList)objectList.Wrap<MBBuilding, Building>();
  }
}
