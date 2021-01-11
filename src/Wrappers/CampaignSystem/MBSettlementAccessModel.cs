using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Localization;

using static TaleWorlds.CampaignSystem.SettlementAccessModel;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBSettlementAccessModel : MBWrapperBase<MBSettlementAccessModel, SettlementAccessModel>
  {

    public bool CanMainHeroDoSettlementAction(MBSettlement settlement,
                                              SettlementAction settlementAction,
                                              out bool shouldBeDisabled,
                                              out MBTextObject wrappedDisabledText)
    {
      TextObject disabledText;
      var result = UnwrappedObject.CanMainHeroDoSettlementAction(settlement,
                                                                 settlementAction,
                                                                 out shouldBeDisabled,
                                                                 out disabledText
                                                                );

      wrappedDisabledText = disabledText;

      return result;
    }
    public static implicit operator SettlementAccessModel(MBSettlementAccessModel wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBSettlementAccessModel(SettlementAccessModel obj) => MBSettlementAccessModel.GetWrapper(obj);
  }
}
