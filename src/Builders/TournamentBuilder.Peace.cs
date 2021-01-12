using TournamentsEnhanced.Builders.Abstract;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public CreateTournamentResult TryCreatePeaceTournamentForFaction(IMBFaction faction)
    {
      var findSettlementResult = SettlementFinder.FindForPeaceTournament(faction);

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
