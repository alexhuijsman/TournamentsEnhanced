using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult TryCreatePeaceTournamentForFaction(IMBFaction faction)
    {
      var payorHero = faction.Leader;
      var failureResult = CreateTournamentResult.Failure();

      if (ValidatePayorHero(payorHero).Failed || ValidateFaction(faction).Failed)
      {
        return failureResult;
      }

      var findSettlementResult = SettlementFinder.FindSettlementForPeaceTournament(faction, payorHero);

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
