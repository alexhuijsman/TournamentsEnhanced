using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindKingdomResult
    : FindResultBase<FindKingdomResult, MBKingdom, MBKingdomList, Kingdom>
  {
    public FindKingdomResult() { }

    public new static FindKingdomResult Failure()
      => new FindKingdomResult() { Status = ResultStatus.Failure };
    public new static FindKingdomResult Success(MBKingdom hero)
      => new FindKingdomResult() { Status = ResultStatus.Failure, Nominee = hero };
  }
}
