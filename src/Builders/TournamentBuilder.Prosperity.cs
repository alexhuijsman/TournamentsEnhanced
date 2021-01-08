using TournamentsEnhanced.Finder;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult TryCreateProsperityTournament()
    {
      var findSettlementResult = SettlementFinder.FindForProsperityTournament();

      if (findSettlementResult.Failed)
      {
        return CreateTournamentResult.Failure;
      }

      var options = new CreateTournamentOptions()
      {
        Settlement = findSettlementResult.Nominee,
        Type = TournamentType.PlayerInitiated
      };

      return CreateTournament(options);
    }
  }
}
