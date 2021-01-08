using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindClanResult
    : FindResultBase<FindClanResult, MBClan, MBClanList, Clan>
  {
    public FindClanResult() { }

    public new static FindClanResult Success(MBClanList nominees) =>
      new FindClanResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees
      };
  }
}
