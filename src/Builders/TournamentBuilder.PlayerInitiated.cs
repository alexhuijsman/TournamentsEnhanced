using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult TryCreatePlayerInitiatedTournament()
    {
      var payorHero = MBHero.MainHero;

      var createTournamentOptions = new CreateTournamentOptions(payorHero.CurrentSettlement,
                                                                TournamentType.PlayerInitiated,
                                                                payorHero);
      return CreateTournament(createTournamentOptions);
    }
  }
}
