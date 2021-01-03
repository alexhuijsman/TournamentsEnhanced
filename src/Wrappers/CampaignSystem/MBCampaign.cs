using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.Abstract;
using TournamentsEnhanced.Wrappers.Localization;

using static TaleWorlds.CampaignSystem.SettlementAccessModel;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBCampaign : CachedWrapperBase<MBCampaign, Campaign>
  {
    public MBCampaign() { }
    public MBCampaign(Campaign obj) : base(obj) { }
    public static bool CanMainHeroJoinTournamentAtCurrentSettlement(out bool shouldBeDisabled, out MBTextObject disabledText)
    {
      return Current.Models.SettlementAccessModel
                      .CanMainHeroDoSettlementAction(
                                                     MBSettlement.CurrentSettlement.UnwrapedObject,
                                                     SettlementAction.JoinTournament,
                                                     out shouldBeDisabled,
                                                     out disabledText
                                                    );
    }

    public static MBCampaign Current => Campaign.Current;
    public ITournamentManager TournamentManager => UnwrapedObject.TournamentManager;
    public MBGameModels Models => UnwrapedObject.Models;

    public static implicit operator Campaign(MBCampaign wrapper) => wrapper.UnwrapedObject;
    public static implicit operator MBCampaign(Campaign obj) => MBCampaign.GetWrapperFor(obj);
  }

  public class MBCampaignList : List<MBCampaign>
  {
    public static implicit operator List<Campaign>(MBCampaignList wrapperList) => wrapperList.Unwrap<MBCampaign, Campaign>();
    public static implicit operator MBCampaignList(List<Campaign> objectList) => (MBCampaignList)objectList.Wrap<MBCampaign, Campaign>();
  }
}
