using System.Collections.Generic;

using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult CreateWeddingTournament(MBHero firstWeddedHero, MBHero secondWeddedHero)
    {
      var failureResult = CreateTournamentResult.Failure;

      var findSettlementResult =
        SettlementFinder.FindForWeddingTournament(firstWeddedHero, secondWeddedHero);

      if (findSettlementResult.Failed)
      {
        return failureResult;
      }

      var options = new CreateTournamentOptions(findSettlementResult.Nominee,
                                                TournamentType.Wedding,
                                                findSettlementResult.Payor.Hero);

      var createTournamentResult = CreateTournament(options);

      if (createTournamentResult.Failed)
      {
        return failureResult;
      }

      NotificationUtils.DisplayBannerMessage($"To celebrate the wedding of {firstWeddedHero.Name} and {secondWeddedHero.Name}, local nobles have called a tournament at {createTournamentResult.HostSettlement.Name}");

      return CreateTournamentResult.Success(findSettlementResult.Nominee,
                                            options.Payor,
                                            findSettlementResult.Nominee.Town.HasTournament);
    }

    private static FindHostSettlementResult FindSettlementWithExistingForWeddingTournament(
      MBSettlementList settlements,
      Payor payor)
    {
      var comparers = new IComparer<MBSettlement>[] {
        new ExistingTournamentComparer(payor, true),
        new PayorRankComparer(payor),
        new PayorRelationComparer(payor, 50),
        new ProsperityComparer(payor)};
      var options = new FindHostSettlementOptions() { Candidates = settlements, Comparers = comparers };

      return SettlementFinder.Find(options);
    }
  }
}
