using System.Text.Json;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Behaviors
{
  public class SyncDataBehavior : MBCampaignBehaviorBase
  {
    public override void RegisterEvents() { }

    public override void SyncData(IDataStore dataStore)
    {
      string json = null;

      if (dataStore.IsSaving)
      {
        json = JsonSerializer.Serialize(ModState.SerializableObject);
      }

      var wasSuccessful = dataStore.SyncData(Constants.Module.ProductName, ref json);

      if (dataStore.IsLoading && wasSuccessful)
      {
        ModState.SerializableObject = JsonSerializer.Deserialize<SerializableModState>(json);
      }
    }
  }
}