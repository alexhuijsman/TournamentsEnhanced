using System.Collections.Generic;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindFactionResult
    : FindResultBase<FindFactionResult, MBFaction, MBFactionImpl>
  {
    public FindFactionResult() { }

    public new static FindFactionResult Success(List<MBFaction> nominees) =>
      new FindFactionResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees
      };
  }
}
