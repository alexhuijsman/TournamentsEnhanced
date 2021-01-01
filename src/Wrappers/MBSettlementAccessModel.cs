using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBSettlementAccessModel : CachedWrapperBase<MBSettlementAccessModel, SettlementAccessModel>
  {
    public static implicit operator SettlementAccessModel(MBSettlementAccessModel wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSettlementAccessModel(SettlementAccessModel obj) => MBSettlementAccessModel.GetWrapperFor(obj);
  }

  public class MBSettlementAccessModelList : List<MBSettlementAccessModel>
  {
    public static implicit operator List<SettlementAccessModel>(MBSettlementAccessModelList wrapperList) => wrapperList.Unwrap<MBSettlementAccessModel, SettlementAccessModel>();
    public static implicit operator MBSettlementAccessModelList(List<SettlementAccessModel> objectList) => (MBSettlementAccessModelList)objectList.Wrap<MBSettlementAccessModel, SettlementAccessModel>();
  }
}
