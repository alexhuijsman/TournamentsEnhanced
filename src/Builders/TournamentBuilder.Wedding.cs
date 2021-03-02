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
      var findSettlementResult = SettlementFinder.FindForWeddingTournament(firstWeddedHero, secondWeddedHero);

      if (findSettlementResult.Failed)
      {
        return CreateTournamentResult.Failure;
      }

      var options = new CreateTournamentOptions()
      {
        Settlement = findSettlementResult.Nominee,
        Type = TournamentType.Wedding
      };

      return CreateTournament(options);
    }
  }
}
