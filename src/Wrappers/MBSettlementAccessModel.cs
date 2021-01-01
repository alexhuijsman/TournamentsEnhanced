using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBSettlementAccessModel : CachedWrapperBase<MBSettlementAccessModel, SettlementAccessModel>
  {
    public static implicit operator SettlementAccessModel(MBSettlementAccessModel wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSettlementAccessModel(SettlementAccessModel obj) => MBSettlementAccessModel.GetWrapperFor(obj);
  }
}
