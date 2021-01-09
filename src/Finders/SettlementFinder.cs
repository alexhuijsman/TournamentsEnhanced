using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class SettlementFinder
    : FinderBase<FindHostSettlementResult, FindHostSettlementOptions, MBSettlement, MBSettlementList, Settlement>
  {
    public static FindHostSettlementResult FindForProsperityTournament()
    {
      var MBKingdom.All.Shuffle();
      //TODO FindKingdomForProsperityTournament
      return FindMostProsperousAvailable(faction.Leader, faction.Settlements);
    }

    public static FindHostSettlementResult FindForPeaceTournament(IMBFaction faction)
    {
      return FindMostProsperousAvailable(faction.Leader, faction.Settlements);
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

      return FindMostProsperousAvailable(kingdomLeader, candidateSettlements);
    }

    private static FindHostSettlementResult FindForClanLeaderWedding(MBHero clanLeader)
    {
      var candidateSettlements = clanLeader.Clan.Settlements;

      return FindMostProsperousAvailable(clanLeader, candidateSettlements);
    }

    private static FindHostSettlementResult FindMostProsperousAvailable(MBHero initiatingHero, MBSettlementList settlements)
    {
      var comparers = new IComparer<MBSettlement>[]
      {
        new ExistingTournamentComparer(initiatingHero, false),
        new ProsperityComparer(initiatingHero)
      };

      var fallbackComparers = new IComparer<MBSettlement>[]
      {
        new InitiatingHeroRankComparer(initiatingHero),
        new ProsperityComparer(initiatingHero)
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers,
        FallbackComparers = fallbackComparers
      };

      return SettlementFinder.Find(options);
    }
  }
}
