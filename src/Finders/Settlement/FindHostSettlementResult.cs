using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindHostSettlementResult
    : FindResultBase<FindHostSettlementResult, MBSettlement, MBSettlementList, Settlement>
  {
    public MBHero Payor { get; private set; }

    public FindHostSettlementResult() { }

    public new static FindHostSettlementResult Success(MBSettlementList nominees, MBHero payor) =>
      new FindHostSettlementResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees,
        Payor = payor
      };
  }
}
