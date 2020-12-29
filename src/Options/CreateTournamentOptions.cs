using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public struct PrepareTournamentOptions
  {
    public TournamentType type;
    public bool shouldApplyProsperityBonus;
    public bool shouldApplySecurityBonus;
    public bool shouldApplyFoodStocksPenalty;
    public PatronType patron;
    public bool shouldApplySettlementStatNotification;
  }
}
