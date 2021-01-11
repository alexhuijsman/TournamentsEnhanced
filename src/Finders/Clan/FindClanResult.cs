using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindClanResult
    : FindResultBase<FindClanResult, MBClan, Clan>
  {
    public FindClanResult() { }

    public new static FindClanResult Success(List<MBClan> nominees) =>
      new FindClanResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees
      };
  }
}
