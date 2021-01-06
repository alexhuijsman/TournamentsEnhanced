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
    public static implicit operator MBSettlementAccessModel(SettlementAccessModel obj) => MBSettlementAccessModel.GetWrapperFor(obj);
  }

  public class MBSettlementAccessModelList : MBListBase<MBSettlementAccessModel, MBSettlementAccessModelList>
  {
    public MBSettlementAccessModelList(IEnumerable<MBSettlementAccessModel> wrappers) => AddRange(wrappers);
    public MBSettlementAccessModelList(MBSettlementAccessModel wrapper) => Add(wrapper);
    public MBSettlementAccessModelList() { }

    public static implicit operator List<SettlementAccessModel>(MBSettlementAccessModelList wrapperList) => wrapperList.Unwrap<MBSettlementAccessModel, SettlementAccessModel>();
    public static implicit operator MBSettlementAccessModelList(List<SettlementAccessModel> objectList) => (MBSettlementAccessModelList)objectList.Wrap<MBSettlementAccessModel, SettlementAccessModel>();
  }
}
