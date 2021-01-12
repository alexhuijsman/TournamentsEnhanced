using TournamentsEnhanced.Builders.Abstract;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public CreateTournamentResult TryCreateWeddingTournament(MBHero firstWeddedHero,
                                                             MBHero secondWeddedHero)
    {
      var failureResult = CreateTournamentResult.Failure;

      var findSettlementResult = SettlementFinder.FindForWeddingTournament(firstWeddedHero, secondWeddedHero);

      if (findSettlementResult.Failed)
      {
        return failureResult;
      }

      var options = new CreateTournamentOptions()
      {
        Settlement = findSettlementResult.Nominee,
        Type = TournamentType.Wedding
      };

      var createTournamentResult = CreateTournament(options);

      if (createTournamentResult.Failed)
      {
        return failureResult;
      }

      return CreateTournamentResult.Success(findSettlementResult.Nominee,
                                            findSettlementResult.Nominee.Town.HasTournament);
    }
  }
}
