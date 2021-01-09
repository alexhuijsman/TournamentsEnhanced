using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindHostSettlementResult
    : FindResultBase<FindHostSettlementResult, MBSettlement, MBSettlementList, Settlement>
  {
    public MBHero InitiatingHero { get; private set; }

    public FindHostSettlementResult() { }

    public new static FindHostSettlementResult Success(MBSettlementList nominees, MBHero initiatingHero) =>
      new FindHostSettlementResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees,
        InitiatingHero = initiatingHero
      };
  }
}
