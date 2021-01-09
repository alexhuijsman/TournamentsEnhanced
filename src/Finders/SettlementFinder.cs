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
      return FindForProsperityTournament(MBSettlement.All);
    }

    public static FindHostSettlementResult FindForInvitationTournament()
    {
      return FindForInvitationTournament(MBSettlementFacade.AllNearMainHero);
    }

    public static FindHostSettlementResult FindForHighbornTournament()
    {
      var settlements =
        (MBSettlementList)MBSettlement
          .All
            .FindAll((settlement) => settlement.MapFaction.IsKingdomFaction &&
                      settlement.OwnerClan.Leader == settlement.MapFaction.Leader);

      return FindForHighbornTournament(settlements);
    }

    private static FindHostSettlementResult FindForHighbornTournament(MBSettlementList settlements)
    {
      var comparers = new IComparer<MBSettlement>[]
      {
        new ExistingTournamentComparer(MBHero.Null, false),
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers,
      };

      return SettlementFinder.Find(options);
    }

    private static FindHostSettlementResult FindForInvitationTournament(MBSettlementList settlements)
    {
      var comparers = new IComparer<MBSettlement>[]
      {
        new ExistingTournamentComparer(MBHero.Null, false),
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers,
      };

      return SettlementFinder.Find(options);
    }

    private static FindHostSettlementResult FindForProsperityTournament(MBSettlementList settlements)
    {
      var comparers = new IComparer<MBSettlement>[]
      {
        new ExistingTournamentComparer(MBHero.Null, false),
        new ProsperityComparer(MBHero.Null, Settings.Instance.RequiredMinProsperity),
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers,
      };

      return SettlementFinder.Find(options);
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
        return failureResult;
      }

      //TODO instead, check if player is wedded hero and ask player if they want to host a tournament.
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

    public static FindHostSettlementResult FindForBirthTournament(MBHero mother)
    {

      var comparers = new IComparer<MBSettlement>[]
      {
        new ExistingTournamentComparer(MBHero.Null, false),
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers,
      };

      return SettlementFinder.Find(options);
    }
  }
