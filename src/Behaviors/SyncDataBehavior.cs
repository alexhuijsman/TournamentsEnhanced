using System.Text.Json;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Behaviors.Abstract;
using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Models.Serializable;

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

      var wasSuccessful = dataStore.SyncData(ModuleConstants.ProductName, ref json);

      if (dataStore.IsLoading && wasSuccessful)
      {
        ModState.SerializableObject = JsonSerializer.Deserialize<SerializableModState>(json);
      }
    }
  }
}