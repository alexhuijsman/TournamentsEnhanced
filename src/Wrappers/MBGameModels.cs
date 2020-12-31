using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Wrappers
{
  public class MBGameModels : CachedWrapper<MBGameModels, GameModels>
  {
    public SettlementAccessModel SettlementAccessModel => UnwrappedObject.SettlementAccessModel;
  }
}
