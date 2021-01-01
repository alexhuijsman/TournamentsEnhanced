using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

using TournamentsEnhanced.Wrappers.Abstract;

namespace TournamentsEnhanced.Wrappers
{
  public class MBCampaign : CachedWrapperBase<MBCampaign, Campaign>
  {
    public MBCampaign() { }
    public MBCampaign(Campaign obj) : base(obj) { }
    public static bool CanMainHeroJoinTournamentAtCurrentSettlement(out bool shouldBeDisabled, out MBTextObject disabledText)
    {
      TextObject unwrappedDisabledText;
      var result = Current.Models.SettlementAccessModel
                    .CanMainHeroDoSettlementAction(
                                                   MBSettlement.CurrentSettlement.UnwrappedObject,
                                                   SettlementAccessModel.SettlementAction.JoinTournament,
                                                   out shouldBeDisabled,
                                                   out unwrappedDisabledText
      );

      disabledText = unwrappedDisabledText;

      return result;
    }

    public static MBCampaign Current => Campaign.Current;
    private MBGameModels Models => UnwrappedObject.Models;

    public static implicit operator Campaign(MBCampaign wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCampaign(Campaign obj) => MBCampaign.GetWrapperFor(obj);
  }

  public class MBCampaignList : List<MBCampaign>
  {
    public static implicit operator List<Campaign>(MBCampaignList wrapperList) => wrapperList.Unwrap<MBCampaign, Campaign>();
    public static implicit operator MBCampaignList(List<Campaign> objectList) => (MBCampaignList)objectList.Wrap<MBCampaign, Campaign>();
  }
}
