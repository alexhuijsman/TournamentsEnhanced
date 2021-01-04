using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class CreateTournamentResult : FindSettlementResultBase
  {
    public bool HadExistingTournament { get; private set; }
    public static CreateTournamentResult Success(MBSettlement settlement, bool hadExistingTournament) => new CreateTournamentResult(ResultStatus.Success, settlement, hadExistingTournament);
    public static CreateTournamentResult Failure() => new CreateTournamentResult(ResultStatus.Failure);

    protected CreateTournamentResult(ResultStatus status) : base(status) { }
    protected CreateTournamentResult(ResultStatus status, MBSettlement settlement, bool hadExistingTournament) : base(status, settlement)
    {
      HadExistingTournament = hadExistingTournament;
    }
  }
}
