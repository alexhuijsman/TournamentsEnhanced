using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public static class MBSettlementFacade
  {
    public static List<MBSettlement> AllNearMainHero => MBSettlement.FindSettlementsAroundPosition(MBHero.MainHero.GetPosition().AsVec2, 60.00f);
  }
}
