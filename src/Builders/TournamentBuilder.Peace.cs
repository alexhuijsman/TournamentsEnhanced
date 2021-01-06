using System.Collections.Generic;

using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult TryCreatePeaceTournamentForFaction(IMBFaction faction)
    {
      var payor = new Payor(faction.Leader);
      var payorHero = payor.Hero;
      var failureResult = CreateTournamentResult.Failure();

      if (!ValidatePayorHero(payorHero) || !ValidateFaction(faction))
      {
        return failureResult;
      }

      var findSettlementResult = TryFindSettlementForPeaceTournament(faction);

      if (findSettlementResult.Failed)
      {
        return failureResult;
      }

      var createTournamentOptions = new CreateTournamentOptions(findSettlementResult.Nominee, TournamentType.Peace, payor);

      return CreateTournament(createTournamentOptions);
    }

    private static FindSettlementResult TryFindSettlementForPeaceTournament(IMBFaction faction)
    {
      var payor = new Payor(faction.Leader);
      var candidateSettlements = faction.Settlements;

      var result = FindSettlementForPeaceTournament(candidateSettlements, payor);

      if (result.Failed)
      {
        result = FindSettlementWithExistingForPeaceTournament(candidateSettlements, payor);
      }

      return result;
    }

    private static FindSettlementResult FindSettlementForPeaceTournament(MBSettlementList settlements, Payor payor)
    {
      var comparers = new IComparer<MBSettlement>[] {
        new ExistingTournamentComparer(payor, false),
        new ProsperityComparer(payor)};
      var options = new FindHostSettlementOptions() { Candidates = settlements, Comparers = comparers };

      return SettlementFinder.Find(options);
    }

    private static FindSettlementResult FindSettlementWithExistingForPeaceTournament(MBSettlementList settlements, Payor payor)
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
