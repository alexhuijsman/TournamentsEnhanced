using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindKingdomResult
    : FindResultBase<FindKingdomResult, MBKingdom, Kingdom>
  {
    public FindKingdomResult() { }

    public new static FindKingdomResult Success(List<MBKingdom> nominees) =>
      new FindKingdomResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees
      };
  }
}
