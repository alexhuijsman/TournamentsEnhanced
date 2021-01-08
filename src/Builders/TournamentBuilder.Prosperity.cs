using TournamentsEnhanced.Finder;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult TryCreateProsperityTournament()
    {
      var payorHero = faction.Leader;
      var failureResult = CreateTournamentResult.Failure;

      if (ValidatePayorHero(payorHero).Failed || ValidateFaction(faction).Failed)
      {
        return failureResult;
      }

      var findSettlementResult = SettlementFinder.FindForPeaceTournament(faction, payorHero);

      if (findSettlementResult.Failed)
      {
        return failureResult;
      }

      var createTournamentOptions = new CreateTournamentOptions(findSettlementResult.Nominee,
                                                                TournamentType.Peace,
                                                                payorHero);

      return CreateTournament(createTournamentOptions);
    }
  }
}
