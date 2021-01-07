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

      var result = FindSettlementForPeaceTournament(candidateSettlements, payor);

      if (result.Failed)
      {
        result = FindSettlementWithExistingForPeaceTournament(candidateSettlements, payor);
      }

      return result;
    }

    public static FindHostSettlementResult FindSettlementForWeddingTournament(MBHero firstWeddedHero, MBHero secondWeddedHero)
    {
      var failureResult = CreateTournamentResult.Failure();
      MBSettlementList candidateSettlements;
      MBSettlementList partnerCandidateSettlements;
      var weddedHeroes = new MBHero[] { firstWeddedHero, secondWeddedHero };

      var maleKingdomLeaderResult = HeroFinder.FindMaleKingdomLeaderHosts(weddedHeroes);
      var maleClanLeaderResult = HeroFinder.FindMaleClanLeaderHosts(weddedHeroes);

      if (maleKingdomLeaderResult.Succeeded)
      {
        candidateSettlements = FindSettlementForKingdomLeader(maleKingdomLeaderResult.Nominee);
      }
      else if (maleClanLeaderResult.Succeeded)
      {
        candidateSettlements = FindSettlementForClanLeader(maleClanLeaderResult.Nominee);
      }

      partnerCandidateSettlements = GetCandidateSettlementsForKingdomLeader(maleKingdomLeaderResult.RunnerUp);

      if (maleKingdomLeaderResult.Succeeded)
      {
      }
      else
      {
        //TODO ask clan leader to host
      }

      if (!maleKingdomLeaderResult.Succeeded)
      {
        return failureResult;
      }

      candidateSettlements =

      // if either is (male + factionLeader), one tourn
      //TODO if a hero is male and faction leader, one tournament in most prosperous city in leader's kingdom, else in most prosperous city of partner's clan
      //TODO if a hero is male and not faction leader, one tournament in most prosperous city in male's clan, else in most prosperous city of partner's clan
      //TODO if both female and at least one has high combat stats, one tournament in most prosperous city in fighter's clan, else in most prosperous city of partner's clan
      //TODO preference for city where wedding happened
      var payor = new Payor(faction.Leader);
      var payorHero = payor.Hero;
      var failureResult = CreateTournamentResult.Failure();

      if (!ValidatePayorHero(payorHero) || !ValidateFaction(faction))
      {
        return failureResult;
      }

      var findSettlementResult = TryFindSettlementForWeddingTournament(faction);

      if (findSettlementResult.Failed)
      {
        return failureResult;
      }

      var createTournamentOptions = new CreateTournamentOptions(findSettlementResult.Nominee, TournamentType.Wedding, payor);

      return CreateTournament(createTournamentOptions);

      var marriageIsBetweenTwoFactions = !firstHero.MapFaction.Equals(secondHero.MapFaction);

      if (marriageIsBetweenTwoFactions)
      {
        OnInterFactionMarriage(firstHero, secondHero, showNotification);
      }
      else
      {
        OnIntraFactionMarriage(firstHero, secondHero, showNotification);
      }
    }

    private static MBSettlementList FindSettlementForKingdomLeader(MBHero nominee)
    {
      throw new NotImplementedException();
    }

    private static FindHostSettlementResult FindSettlementForPeaceTournament(MBSettlementList settlements, Payor payor)
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

    private static FindHostSettlementResult FindSettlementWithExistingForPeaceTournament(MBSettlementList settlements, Payor payor)
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
