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
}
