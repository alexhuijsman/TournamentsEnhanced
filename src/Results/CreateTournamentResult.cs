using System.Xml.XPath;

using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public class CreateTournamentResult : TownResultBase
  {
    public bool TownHadExistingTournament { get; private set; }
    public static CreateTournamentResult Success(Town town, bool townHadExistingTournament) => new CreateTournamentResult(ResultStatus.Success, town, townHadExistingTournament);
    public static CreateTournamentResult Failure() => new CreateTournamentResult(ResultStatus.Failure);

    protected CreateTournamentResult(ResultStatus status) : base(status) { }
    protected CreateTournamentResult(ResultStatus status, Town town, bool townHadExistingTournament) : base(status, town)
    {
      TownHadExistingTournament = townHadExistingTournament;
    }
  }
}
