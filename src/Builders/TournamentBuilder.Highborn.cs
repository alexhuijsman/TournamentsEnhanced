using TournamentsEnhanced.Builders.Abstract;
using TournamentsEnhanced.Finder;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public CreateTournamentResult TryCreateHighbornTournament()
    {
      var findSettlementResult = SettlementFinder.FindForHighbornTournament();

      if (findSettlementResult.Failed)
      {
        return CreateTournamentResult.Failure;
      }

      var createTournamentOptions = new CreateTournamentOptions()
      {
        InitiatingHero = findSettlementResult.InitiatingHero,
        Settlement = findSettlementResult.Nominee,
        Type = TournamentType.Peace
      };

      return CreateTournament(createTournamentOptions);
    }
  }
}
