using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindHostSettlementResult
    : FindResultBase<FindHostSettlementResult, MBSettlement, Settlement>
  {
    public MBHero InitiatingHero { get; private set; }

    public FindHostSettlementResult() { }

    public new static FindHostSettlementResult Success(List<MBSettlement> nominees, MBHero initiatingHero) =>
      new FindHostSettlementResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees,
        InitiatingHero = initiatingHero
      };
  }
}
