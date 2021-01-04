using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Finder.Results
{
  public class FindHostSettlementResult : FindSettlementResultBase
  {
    public static FindHostSettlementResult Failure() => new FindHostSettlementResult(ResultStatus.Failure);
    public static FindHostSettlementResult Success(Settlement settlement) => new FindHostSettlementResult(ResultStatus.Success, settlement);

    private FindHostSettlementResult(ResultStatus status, Settlement settlement) : base(status, settlement)
    { }

    private FindHostSettlementResult(ResultStatus status) : base(status) { }
  }
}
