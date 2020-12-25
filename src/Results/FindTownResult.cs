using System.Xml;
using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
    public class FindTownResult : TownResultBase
    {
        public static FindTownResult Failure() => new FindTownResult(ResultStatus.Failure);
        public static FindTownResult Success(Town town) => new FindTownResult(ResultStatus.Success, town);

        private FindTownResult(ResultStatus status, Town town) : base(status, town)
        { }

        private FindTownResult(ResultStatus status) : base(status) { }
    }
}
