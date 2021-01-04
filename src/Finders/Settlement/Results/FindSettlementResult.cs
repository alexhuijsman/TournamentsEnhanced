using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;

namespace TournamentsEnhanced.Finder
{
  public class FindSettlementResult : FindSettlementResultBase
  {
    public static FindSettlementResult Failure() => new FindSettlementResult(ResultStatus.Failure);
    public static FindSettlementResult Success(Settlement settlement) => new FindSettlementResult(ResultStatus.Success, settlement);

    private FindSettlementResult(ResultStatus status, Settlement settlement) : base(status, settlement)
    { }

    private FindSettlementResult(ResultStatus status) : base(status) { }
  }
}
