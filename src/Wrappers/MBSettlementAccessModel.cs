using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

using TournamentsEnhanced.Wrappers.Abstract;

using static TaleWorlds.CampaignSystem.SettlementAccessModel;

namespace TournamentsEnhanced.Wrappers
{
  public class MBSettlementAccessModel : CachedWrapperBase<MBSettlementAccessModel, SettlementAccessModel>
  {

    public bool CanMainHeroDoSettlementAction(MBSettlement settlement,
                                              SettlementAction settlementAction,
                                              out bool shouldBeDisabled,
                                              out MBTextObject wrappedDisabledText)
    {
      TextObject disabledText;
      var result = UnwrapedObject.CanMainHeroDoSettlementAction(settlement,
                                                                 settlementAction,
                                                                 out shouldBeDisabled,
                                                                 out disabledText
                                                                );

      wrappedDisabledText = disabledText;

      return result;
    }
    public static implicit operator SettlementAccessModel(MBSettlementAccessModel wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBSettlementAccessModel(SettlementAccessModel obj) => MBSettlementAccessModel.GetWrapperFor(obj);
  }

  public class MBSettlementAccessModelList : List<MBSettlementAccessModel>
  {
    public static implicit operator List<SettlementAccessModel>(MBSettlementAccessModelList wrapperList) => wrapperList.Unwrap<MBSettlementAccessModel, SettlementAccessModel>();
    public static implicit operator MBSettlementAccessModelList(List<SettlementAccessModel> objectList) => (MBSettlementAccessModelList)objectList.Wrap<MBSettlementAccessModel, SettlementAccessModel>();
  }
}
