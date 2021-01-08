using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class FindHostHeroResult : FindHeroResult
  {
    public FindHostHeroResult() { }

    public new static FindHostHeroResult Success(MBHeroList nominees) =>
      new FindHostHeroResult()
      {
        Status = ResultStatus.Success,
        AllQualifiedCandidates = nominees
      };
  }
}
