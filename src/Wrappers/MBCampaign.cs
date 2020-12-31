using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

namespace TournamentsEnhanced.Wrappers
{
  public class MBCampaign : CachedWrapper<MBCampaign, Campaign>
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

      disabledText = MBTextObject.GetWrapperFor(unwrappedDisabledText);

      return result;
    }

    public static MBCampaign Current => MBCampaign.GetWrapperFor(Campaign.Current);
    private MBGameModels Models => MBGameModels.GetWrapperFor(UnwrappedObject.Models);

  }
}
