
namespace TournamentsEnhanced.Wrappers.CampaignSystem
{
  public static class MBSettlementFacade
  {
    public static MBSettlementList AllNearMainHero => MBSettlement.FindSettlementsAroundPosition(MBHero.MainHero.GetPosition().AsVec2, 60.00f);
  }
}
