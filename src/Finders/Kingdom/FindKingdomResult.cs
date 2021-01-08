using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindKingdomResult
    : FindResultBase<FindKingdomResult, MBKingdom, MBKingdomList, Kingdom>
  {
    public FindKingdomResult() { }

    public new static FindKingdomResult Success(MBKingdomList nominees) =>
      new FindKingdomResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees
      };
  }
}
