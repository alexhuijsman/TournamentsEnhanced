using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class CreateTournamentResult : CreateResultBase<CreateTournamentResult>
  {
    public MBSettlement HostSettlement { get; protected set; }
    public bool HadExistingTournament { get; protected set; }
    public static CreateTournamentResult Success(MBSettlement settlement, bool hadExistingTournament)
    {
      return new CreateTournamentResult()
      {
        HadExistingTournament = hadExistingTournament,
        Status = ResultStatus.Success
      };
    }

    public new static CreateTournamentResult Failure() =>
      new CreateTournamentResult() { Status = ResultStatus.Failure };

  }
}
