using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult TryCreatePeaceTournamentForFaction(IMBFaction faction)
    {
      var initiatingHero = faction.Leader;

      var findSettlementResult = SettlementFinder.FindForPeaceTournament(faction, initiatingHero);

      if (findSettlementResult.Failed)
      {
        return CreateTournamentResult.Failure;
      }

      var createTournamentOptions = new CreateTournamentOptions()
      {
        InitiatingHero = initiatingHero,
        Settlement = findSettlementResult.Nominee,
        Type = TournamentType.Peace
      };

      return CreateTournament(createTournamentOptions);
    }
  }
}
