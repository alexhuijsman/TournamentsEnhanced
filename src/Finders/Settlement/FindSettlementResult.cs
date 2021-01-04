using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindSettlementResult
    : FindResultBase<FindSettlementResult, MBSettlement, MBSettlementList, Settlement>
  {
    public FindSettlementResult() { }

    public new static FindSettlementResult Success(MBSettlement nominee) => new FindSettlementResult() { Status = ResultStatus.Success, Nominee = nominee };
    public new static FindSettlementResult Failure() => new FindSettlementResult() { Status = ResultStatus.Failure };
  }
}
