using TournamentsEnhanced.Finder;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult TryCreateProsperityTournament()
    {

      var failureResult = CreateTournamentResult.Failure;

      var findSettlementResult = SettlementFinder.FindForProsperityTournament();

      if (findSettlementResult.Failed)
      {
        return failureResult;
      }

      var options = new CreateTournamentOptions(findSettlementResult.Nominee,
                                                TournamentType.Peace,
                                                payorHero);

      return CreateTournament(options);
    }
  }
}
