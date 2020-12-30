using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public class FindHostTownResult : TownResultBase
  {
    public static FindHostTownResult Failure() => new FindHostTownResult(ResultStatus.Failure);
    public static FindHostTownResult Success(Town town) => new FindHostTownResult(ResultStatus.Success, town);

    private FindHostTownResult(ResultStatus status, Town town) : base(status, town)
    { }

    private FindHostTownResult(ResultStatus status) : base(status) { }
  }
}
