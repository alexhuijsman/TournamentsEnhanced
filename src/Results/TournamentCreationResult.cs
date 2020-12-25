using System.Xml.XPath;
using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
    public class TournamentCreationResult : TownResultBase
    {
        public bool TownHadExistingTournament { get; private set; }
        public static TournamentCreationResult Success(Town town, bool townHadExistingTournament) => new TournamentCreationResult(ResultStatus.Success, town, townHadExistingTournament);
        public static TournamentCreationResult Failure() => new TournamentCreationResult(ResultStatus.Failure);

        protected TournamentCreationResult(ResultStatus status) : base(status) { }
        protected TournamentCreationResult(ResultStatus status, Town town, bool townHadExistingTournament) : base(status, town)
        {
            TownHadExistingTournament = townHadExistingTournament;
        }
    }
}
