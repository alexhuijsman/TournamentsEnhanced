using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindHostSettlementResult
    : FindResultBase<FindHostSettlementResult, MBSettlement, MBSettlementList, Settlement>
  {
    public Payor Payor { get; private set; }

    public FindHostSettlementResult() { }

    public new static FindHostSettlementResult Success(MBSettlementList nominees)
      => new FindHostSettlementResult() { Status = ResultStatus.Success, AllQualifiedCandidates = nominees };
    public new static FindHostSettlementResult Failure() => new FindHostSettlementResult() { Status = ResultStatus.Failure };
  }
}
