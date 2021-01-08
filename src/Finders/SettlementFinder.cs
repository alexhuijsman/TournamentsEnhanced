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
    public static FindHostSettlementResult FindForPeaceTournament(IMBFaction faction, MBHero payorHero)
    {
      var payor = new Payor(payorHero);
      var candidateSettlements = faction.Settlements;

      var result = FindMostProsperousExisting(candidateSettlements, payor);

      if (result.Failed)
      {
        result = FindMostProsperousExisting(candidateSettlements, payor);
      }

      return result;
    }

    public static FindHostSettlementResult FindForWeddingTournament(MBHero firstWeddedHero, MBHero secondWeddedHero)
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
      //TODO make host of wedding town the owner
      var primaryHostHero = findHostHeroResult.Nominee;
      var secondaryHostHero = findHostHeroResult.RunnerUp;

      var result = FindForFactionLeaderWedding(findHostHeroResult.Nominee);

      if (result.Failed && findHostHeroResult.HasRunnerUp)
      {
        result = FindForFactionLeaderWedding(findHostHeroResult.RunnerUp);
      }

      return result;

    }

    private static FindHostSettlementResult FindForFactionLeaderWedding(MBHero factionLeader)
      => factionLeader.MapFaction.IsKingdomFaction ?
           FindForKingdomLeaderWedding(factionLeader) :
           FindForClanLeaderWedding(factionLeader);


    private static FindHostSettlementResult FindForKingdomLeaderWedding(MBHero kingdomLeader)
    {
      var candidateSettlements = kingdomLeader.Clan.Settlements;
      var payor = new Payor(kingdomLeader);

      var result = FindMostProsperousAvailable(candidateSettlements, payor);
      if (result.Failed)
      {
        result = FindMostProsperousExisting(candidateSettlements, payor);
      }

      return result;
    }

    private static FindHostSettlementResult FindForClanLeaderWedding(MBHero clanLeader)
    {
      var candidateSettlements = clanLeader.Clan.Settlements;
      var payor = new Payor(clanLeader);

      var result = FindMostProsperousAvailable(candidateSettlements, payor);
      if (result.Failed)
      {
        result = FindMostProsperousExisting(candidateSettlements, payor);
      }

      return result;
    }

    private static FindHostSettlementResult FindMostProsperousAvailable(MBSettlementList settlements, Payor payor)
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

    private static FindHostSettlementResult FindMostProsperousExisting(MBSettlementList settlements,
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
