using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public class SaveCampaignModBehavior : CampaignBehaviorBase
  {
    private SaveModel SaveModel
    {
      get => BuildSaveModel();
      set => LoadFromSaveModel(value);
    }

    private SaveModel BuildSaveModel()
    {
      var saveModel = new SaveModel()
      {
        tournamentRecords = TournamentRecords
      };
    }

    public static implicit operator SaveModel(TournamentRecords r) => r.;

    private void LoadFromSaveModel(SaveModel value)
    {
      throw new System.NotImplementedException();
    }

    public override void RegisterEvents() { }

    public override void SyncData(IDataStore dataStore)
    {
      string dataToSync = json;

      var wasSyncSuccessful = dataStore.SyncData(ModuleConstants.ProductName, ref json);
      if (dataStore.IsLoading)
      {

      }
    }
  }
}