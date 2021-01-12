using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBSettlementFacade
  {
    public static MBSettlementFacade Instance { get; } = new MBSettlementFacade();

    public List<MBSettlement> AllNearMainHero => MBSettlement.FindSettlementsAroundPosition(MBHero.MainHero.GetPosition().AsVec2, 60.00f);
  }
}
