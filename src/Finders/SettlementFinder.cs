using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class SettlementFinder
    : FinderBase<FindHostSettlementResult, FindHostSettlementOptions, MBSettlement, MBSettlementList, Settlement>
  {
    public static FindHostSettlementResult FindSettlementForPeaceTournament(IMBFaction faction, MBHero payorHero)
    {
      var payor = new Payor(payorHero);
      var candidateSettlements = faction.Settlements;

      var result = FindMostProsperousExistingHostSettlementForPeace(candidateSettlements, payor);

      if (result.Failed)
      {
        result = FindMostProsperousExistingHostSettlementForPeace(candidateSettlements, payor);
      }

      return result;
    }

    public static FindHostSettlementResult FindSettlementForWeddingTournament(MBHero firstWeddedHero, MBHero secondWeddedHero)
    {
      var failureResult = FindHostSettlementResult.Failure;
      var findHostHeroResult = HeroFinder.FindHostsFromWeddedHeroes(firstWeddedHero, secondWeddedHero);

      if (findHostHeroResult.Failed)
      {
        //TODO ask clan leader to host
        return failureResult;
      }

      //TODO "local nobles have called a tournament in your honor" but currently the wedded person is the host
      //TODO instead, check if wedded persons own current town, and ask player if they want to host a tournament.

      var primaryHostHero = findHostHeroResult.Nominee;
      var secondaryHostHero = findHostHeroResult.RunnerUp;

      var result = FindHostSettlementsForFactionLeaderWedding(findHostHeroResult.Nominee);

      if (result.Failed && findHostHeroResult.HasRunnerUp)
      {
        result = FindHostSettlementsForFactionLeaderWedding(findHostHeroResult.RunnerUp);
      }

      return result;

    }

    private static FindHostSettlementResult FindHostSettlementsForFactionLeaderWedding(MBHero factionLeader)
      => factionLeader.MapFaction.IsKingdomFaction ?
           FindHostSettlementsForKingdomLeaderWedding(factionLeader) :
           FindHostSettlementsForClanLeaderWedding(factionLeader);

    private static FindHostSettlementResult FindHostSettlementsForClanLeaderWedding(MBHero clanLeader)
    {
      var options = new FindHostSettlementOptions()
      {
        Candidates = clanLeader.MapFaction.Settlements,
        Comparers = { new Comparer}
      };

      return Find(options);
    }

    private static FindHostSettlementResult FindHostSettlementsForKingdomLeaderWedding(MBHero kingdomLeader)
    {
      throw new NotImplementedException();
    }

    public static FindHostSettlementResult FindSettlementForPeace(MBSettlementList settlements,
                                                                  Payor payor)
    {
      var result = FindMostProsperousAvailableHostSettlement(settlements, payor);
      if (result.Failed)
      {
        result = FindMostProsperousExistingHostSettlementForPeace(settlements, payor);
      }

      return result;
    }

    private static FindHostSettlementResult FindMostProsperousAvailableHostSettlement(MBSettlementList settlements, Payor payor)
    {
      var comparers = new IComparer<MBSettlement>[]
      {
        new ExistingTournamentComparer(payor, false),
        new ProsperityComparer(payor)
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers
      };

      return SettlementFinder.Find(options);
    }

    private static FindHostSettlementResult FindMostProsperousExistingHostSettlementForPeace(MBSettlementList settlements,
                                                                               Payor payor)
    {
      var comparers = new IComparer<MBSettlement>[] {
        new ExistingTournamentComparer(payor, true),
        new PayorRankComparer(payor),
        new PayorRelationComparer(payor, 50),
        new ProsperityComparer(payor)};

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers
      };

      return SettlementFinder.Find(options);
    }
  }
}
