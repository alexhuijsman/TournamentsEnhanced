using TournamentsEnhanced.Builders.Abstract;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public CreateTournamentResult TryMakeBirthTournament(MBHero mother)
    {
      var findSettlementResult = SettlementFinder.FindForBirthTournament(mother);

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
