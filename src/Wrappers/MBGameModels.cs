using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Wrappers
{
  public class MBGameModels : CachedWrapperBase<MBGameModels, GameModels>
  {
    public SettlementAccessModel SettlementAccessModel => UnwrappedObject.SettlementAccessModel;
  }
}
