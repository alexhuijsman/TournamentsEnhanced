using TournamentsEnhanced.Builders.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult TryCreatePlayerInitiatedTournament()
    {
      var payorHero = MBHero.MainHero;

      var options = new CreateTournamentOptions()
      {
        Settlement = payorHero.CurrentSettlement,
        Type = TournamentType.PlayerInitiated
      };

      return CreateTournament(options);
    }
  }
}
