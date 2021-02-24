using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public class SettlementFinder
    : FinderBase<FindHostSettlementResult, FindHostSettlementOptions, MBSettlement, Settlement>
  {
    public static SettlementFinder Instance { get; } = new SettlementFinder();
    protected HeroFinder HeroFinder { get; set; } = HeroFinder.Instance;
    protected MBSettlementFacade MBSettlementFacade { get; set; } = MBSettlementFacade.Instance;

    protected SettlementFinder() { }

    public FindHostSettlementResult FindForProsperityTournament()
    {
      return FindForProsperityTournament(MBSettlement.All);
    }

    public FindHostSettlementResult FindForInvitationTournament()
    {
      return FindForInvitationTournament(MBSettlementFacade.AllNearMainHero);
    }

    public FindHostSettlementResult FindForHighbornTournament()
    {
      var settlements =
        (List<MBSettlement>)MBSettlement
          .All
            .FindAll((settlement) => settlement.MapFaction.IsKingdomFaction &&
                      settlement.OwnerClan.Leader == settlement.MapFaction.Leader);

      return FindForHighbornTournament(settlements);
    }

    private FindHostSettlementResult FindForHighbornTournament(List<MBSettlement> settlements)
    {
      var comparers = new IComparer<MBSettlement>[]
      {
        ExistingTournamentComparer.Instance,
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers,
      };

      return Find(options);
    }

    private FindHostSettlementResult FindForInvitationTournament(List<MBSettlement> settlements)
    {
      var comparers = new IComparer<MBSettlement>[]
      {
        ExistingTournamentComparer.Instance,
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers,
      };

      return Find(options);
    }

    private FindHostSettlementResult FindForProsperityTournament(List<MBSettlement> settlements)
    {
      var comparers = new IComparer<MBSettlement>[]
      {
        ExistingTournamentComparer.Instance,
        ProsperityComparer.Instance,
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers,
      };

      return Find(options);
    }

    public FindHostSettlementResult FindForPeaceTournament(IMBFaction faction)
    {
      return FindMostProsperousAvailable(faction.Settlements);
    }

    public FindHostSettlementResult FindForWeddingTournament(MBHero firstWeddedHero, MBHero secondWeddedHero)
    {
      var failureResult = FindHostSettlementResult.Failure;
      var findHostHeroResult = HeroFinder.FindHostsFromWeddedHeroes(firstWeddedHero, secondWeddedHero);

      if (findHostHeroResult.Failed)
      {
        return failureResult;
      }

      var primaryHostHero = findHostHeroResult.Nominee;
      var secondaryHostHero = findHostHeroResult.RunnerUp;

      var result = FindForFactionLeaderWedding(findHostHeroResult.Nominee);

      if (result.Failed && findHostHeroResult.HasRunnerUp)
      {
        result = FindForFactionLeaderWedding(findHostHeroResult.RunnerUp);
      }

      return result;

    }

    private FindHostSettlementResult FindForFactionLeaderWedding(MBHero factionLeader)
      => factionLeader.MapFaction.IsKingdomFaction ?
           FindForKingdomLeaderWedding(factionLeader) :
           FindForClanLeaderWedding(factionLeader);


    private FindHostSettlementResult FindForKingdomLeaderWedding(MBHero kingdomLeader)
    {
      var candidateSettlements = kingdomLeader.Clan.Settlements;

      return FindMostProsperousAvailable(candidateSettlements);
    }

    private FindHostSettlementResult FindForClanLeaderWedding(MBHero clanLeader)
    {
      var candidateSettlements = clanLeader.Clan.Settlements;

      return FindMostProsperousAvailable(candidateSettlements);
    }

    private FindHostSettlementResult FindMostProsperousAvailable(List<MBSettlement> settlements)
    {
      var comparers = new IComparer<MBSettlement>[]
      {
        ExistingTournamentComparer.Instance,
        ProsperityComparer.Instance
      };

      var fallbackComparers = new IComparer<MBSettlement>[]
      {
        InitiatingHeroRankComparer.Instance,
        ProsperityComparer.Instance
      };

      var options = new FindHostSettlementOptions()
      {
        Candidates = settlements,
        Comparers = comparers,
        FallbackComparers = fallbackComparers
      };

      return Find(options);
    }

    public FindHostSettlementResult FindForBirthTournament(MBHero mother)
    {
      var findCandidateSettlementResults = FindBirthTournamentSettlementsForParent(mother);

      if (findCandidateSettlementResults.Failed)
      {
        return FindHostSettlementResult.Failure;
      }

      var options = new FindHostSettlementOptions()
      {
        Candidates = findCandidateSettlementResults.AllQualifiedCandidates,
        Comparers = new IComparer<MBSettlement>[]
        {
          ProsperityComparer.Instance
        },
      };

      return Find(options);
    }

    private FindHostSettlementResult FindBirthTournamentSettlementsForParent(MBHero parent)
    {
      var leaderResult = HeroFinder.FindFactionLeaders(parent, parent.Spouse);

      if (leaderResult.Failed)
      {
        return FindHostSettlementResult.Failure;
      }

      var candidateSettlements = new List<MBSettlement>();

      foreach (var leader in leaderResult.AllQualifiedCandidates)
      {
        var findOwnedSettlementsResult = FindHostSettlementsOwnedByHero(leader);
        if (findOwnedSettlementsResult.Succeeded)
        {
          candidateSettlements.AddRange(findOwnedSettlementsResult.AllQualifiedCandidates);
        }
      }

      if (candidateSettlements.IsEmpty())
      {
        return FindHostSettlementResult.Failure;
      }

      return FindHostSettlementResult.Success(candidateSettlements);
    }

    private FindHostSettlementResult FindHostSettlementsOwnedByHero(MBHero hero)
    {
      var options = new FindHostSettlementOptions()
      {
        Candidates = hero.Clan.Settlements,
        Comparers = new IComparer<MBSettlement>[]
        {
          InitiatingHeroOwnershipComparer.Instance,
        }
      };

      return Find(options);
    }
  }
}
