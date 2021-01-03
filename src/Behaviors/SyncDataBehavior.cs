using System.Text.Json;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Behaviors.Abstract;
using TournamentsEnhanced.Models.ModState;

namespace TournamentsEnhanced.Behaviors
{
  public class SyncDataBehavior : MBCampaignBehaviorBase
  {
    public override void RegisterEvents() { }

    public override void SyncData(IDataStore dataStore)
    {
      JsonSerializer.Serialize(ModState.)
      JsonConvert.SerializeObject(saveData);
      string dataToSync = json;

      var wasSyncSuccessful = dataStore.SyncData(ModuleConstants.ProductName, ref json);
      if (dataStore.IsLoading)
      {

      }
    }
  }
}