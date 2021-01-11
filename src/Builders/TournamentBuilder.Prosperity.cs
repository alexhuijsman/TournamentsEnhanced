using TournamentsEnhanced.Builders.Abstract;
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
        Settlement = findSettlementResult.AllQualifiedCandidates.DeterministicShuffle().First(),
        Type = TournamentType.Prosperity
      };

      return CreateTournament(options);
    }
  }
}
