using System.Text.Json;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Models;

namespace TournamentsEnhanced
{
  public class SyncDataBehavior : CampaignBehaviorBase
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