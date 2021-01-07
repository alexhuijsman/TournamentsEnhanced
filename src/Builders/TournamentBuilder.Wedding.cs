using System.Collections.Generic;

using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public partial class TournamentBuilder : TournamentBuilderBase
  {
    public static CreateTournamentResult TryCreateWeddingTournament(MBHero firstWeddedHero, MBHero secondWeddedHero)
    {
      var failureResult = CreateTournamentResult.Failure();
      MBSettlementList candidateSettlements;
      MBSettlementList partnerCandidateSettlements;

      var options = new FindHeroOptions()
      {
        Candidates = new MBHeroList(firstWeddedHero, secondWeddedHero),
        Comparers = new IComparer<MBHero>[] { new WeddedKingdomLeaderComparer() }
      };

      var result = HeroFinder.Find(options);

      if (result.Succeeded)
      {
        candidateSettlements = result.Nominee.MapFaction.Settlements;
        partnerCandidateSettlements = result.Nominee == firstWeddedHero ? secondWeddedHero.
      }
      else
      {
        options = new FindHeroOptions()
        {
          Candidates = new MBHeroList(firstWeddedHero, secondWeddedHero),
          Comparers = new IComparer<MBHero>[] { new WeddedClanLeaderComparer() }
        };
        result = HeroFinder.Find(options);
      }

      if (!result.Succeeded)
      {
        //TODO ask clan leader to host
      }

      if (!result.Succeeded)
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

    private void OnInterFactionMarriage(Hero firstHero, Hero secondHero, bool showNotification)
    {
      var resultsA = TournamentBuilder.CreateTournamentTypeInTownBelongingToFaction(TournamentType.Wedding, firstHero.MapFaction);
      var resultsB = TournamentBuilder.CreateTournamentTypeInTownBelongingToFaction(TournamentType.Wedding, secondHero.MapFaction);

      if (!resultsA.Succeeded && !resultsB.Succeeded)
      {
        return;
      }

      string hostTownNames;
      if (resultsA.Succeeded && resultsB.Succeeded)
      {
        hostTownNames = $"{resultsA.Town.Name} and {resultsB.Town.Name}";
      }
      else
      {
        hostTownNames = resultsA.Succeeded ? $"{resultsA.Town.Name}" : $"{resultsB.Town.Name}";
      }

      NotificationUtils.DisplayBannerMessage($"To celebrate the wedding of {firstHero.Name} and {secondHero.Name}, local nobles have called a tournament at {hostTownNames}");
    }

    private static FindSettlementResult TryFindSettlementForWeddingTournament(IMBFaction faction)
    {
      var payor = new Payor(faction.Leader);
      var candidateSettlements = faction.Settlements;

      var result = FindSettlementForWeddingTournament(candidateSettlements, payor);

      if (result.Failed)
      {
        result = FindSettlementWithExistingForWeddingTournament(candidateSettlements, payor);
      }

      return result;
    }

    private static FindSettlementResult FindSettlementForWeddingTournament(MBSettlementList settlements, Payor payor)
    {
      var comparers = new IComparer<MBSettlement>[] {
        new ExistingTournamentComparer(payor, false),
        new ProsperityComparer(payor)};
      var options = new FindHostSettlementOptions() { Candidates = settlements, Comparers = comparers };

      return SettlementFinder.Find(options);
    }

    private static FindSettlementResult FindSettlementWithExistingForWeddingTournament(MBSettlementList settlements, Payor payor)
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
