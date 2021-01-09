using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Localization;

using static TaleWorlds.CampaignSystem.SettlementAccessModel;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCampaign : MBWrapperBase<MBCampaign, Campaign>
  {
    public static bool CanMainHeroJoinTournamentAtCurrentSettlement(out bool shouldBeDisabled, out MBTextObject disabledText)
    {
      return Current.Models.SettlementAccessModel
                      .CanMainHeroDoSettlementAction(
                                                     MBSettlement.CurrentSettlement.UnwrappedObject,
                                                     SettlementAction.JoinTournament,
                                                     out shouldBeDisabled,
                                                     out disabledText
                                                    );
    }

    public static MBCampaign Current => Campaign.Current;
    public MBTournamentManager TournamentManager => (MBTournamentManager)UnwrappedObject.TournamentManager;
    public MBGameModels Models => UnwrappedObject.Models;

    public static implicit operator Campaign(MBCampaign wrapper) => wrapper.UnwrappedObject;
    public static implicit operator MBCampaign(Campaign obj) => MBCampaign.GetWrapper(obj);
  }

  public class MBCampaignList : MBListBase<MBCampaign, MBCampaignList>
  {
    public MBCampaignList(params MBCampaign[] wrappers) : this((IEnumerable<MBCampaign>)wrappers) { }
    public MBCampaignList(IEnumerable<MBCampaign> wrappers) => AddRange(wrappers);
    public MBCampaignList(MBCampaign wrapper) => Add(wrapper);
    public MBCampaignList() { }

    public static implicit operator List<Campaign>(MBCampaignList wrapperList) => wrapperList.Unwrap<MBCampaign, Campaign>();
    public static implicit operator MBCampaignList(List<Campaign> objectList) => (MBCampaignList)objectList.Wrap<MBCampaign, Campaign>();
    public static implicit operator MBCampaign[](MBCampaignList wrapperList) => wrapperList.ToArray();
  }
}
