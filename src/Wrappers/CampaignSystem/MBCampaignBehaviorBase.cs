using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCampaignBehaviorBase : MBWrapperBase<MBCampaignBehaviorBase, CampaignBehaviorBase>
  {
    public virtual void SyncData(IDataStore dataStore) => UnwrappedObject.SyncData(dataStore);

    public virtual void RegisterEvents() { }
    public static implicit operator CampaignBehaviorBase(MBCampaignBehaviorBase wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCampaignBehaviorBase(CampaignBehaviorBase obj) => MBCampaignBehaviorBase.GetWrapper(obj);
  }

  public class MBCampaignBehaviorBaseList : MBListBase<MBCampaignBehaviorBase, MBCampaignBehaviorBaseList>
  {
    public MBCampaignBehaviorBaseList(params MBCampaignBehaviorBase[] wrappers) : this((IEnumerable<MBCampaignBehaviorBase>)wrappers) { }
    public MBCampaignBehaviorBaseList(IEnumerable<MBCampaignBehaviorBase> wrappers) => AddRange(wrappers);
    public MBCampaignBehaviorBaseList(MBCampaignBehaviorBase wrapper) => Add(wrapper);
    public MBCampaignBehaviorBaseList() { }

    public static implicit operator List<CampaignBehaviorBase>(MBCampaignBehaviorBaseList wrapperList) => wrapperList.Unwrap<MBCampaignBehaviorBase, CampaignBehaviorBase>();
    public static implicit operator MBCampaignBehaviorBaseList(List<CampaignBehaviorBase> objectList) => (MBCampaignBehaviorBaseList)objectList.Wrap<MBCampaignBehaviorBase, CampaignBehaviorBase>();
    public static implicit operator MBCampaignBehaviorBase[](MBCampaignBehaviorBaseList wrapperList) => wrapperList.ToArray();
  }
}
