using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindFactionResult
    : FindResultBase<FindFactionResult, MBFaction, MBFactionList, MBFactionImpl>
  {
    public FindFactionResult() { }

    public new static FindFactionResult Success(MBFactionList nominees) =>
      new FindFactionResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees
      };
  }
}
