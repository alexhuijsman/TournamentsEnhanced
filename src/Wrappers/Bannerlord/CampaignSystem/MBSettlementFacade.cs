using System.Collections.Generic;

namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public class MBSettlementFacade
  {
    public static MBSettlementFacade Instance { get; } = new MBSettlementFacade();
    protected MBSettlement MBSettlement { get; set; } = MBSettlement.Instance;

    public virtual List<MBSettlement> AllNearMainHero => MBSettlement.FindSettlementsAroundPosition(MBHero.MainHero.GetPosition().AsVec2, 60.00f);
  }
}
