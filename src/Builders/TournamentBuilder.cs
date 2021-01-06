using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public static class TournamentBuilder
  {
    public static CreateTournamentResult TryMakePeaceTournamentForFaction(IMBFaction faction)
    {
      var payor = new Payor(faction.Leader);
      var payorHero = payor.Hero;
      var failureResult = CreateTournamentResult.Failure();

      //TODO payor !payorHero.IsDead && !payorHero.IsPrisoner && canAffordTournamentCost
      //TODO  faction faction.Settlements.IsEmpty || has no towns
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

    private static bool ValidatePayorHero(MBHero payorHero)
    {
      return HeroFinder.Find(new FindHeroOptions() { Candidates = new MBHeroList(payorHero) };
    }

    private static bool ValidateFaction(IMBFaction faction)
    {
      return FactionFinder.FindHostFaction(new FindFactionOptions() { factions = [faction] }).Succeeded;
    }

    private static FindSettlementResult TryFindSettlementForPeaceTournament(IMBFaction faction)
    {
      var payor = new Payor(faction.Leader);
      var candidateSettlements = faction.Settlements;
      var result = FindSettlementForPeaceTournament(candidateSettlements, payor);

      if (result.Failed)
      {
        result = FindSettlementWithExistingTournamentForPeace(candidateSettlements, payor);
      }

      return result;
    }

    private static FindSettlementResult FindSettlementForPeaceTournament(MBSettlementList settlements, Payor payor)
    {
      var comparers = new IComparer<MBSettlement>[] { new ExistingTournamentComparer(payor, false) };
      var options = new FindHostSettlementOptions() { Candidates = settlements, Comparers = comparers };
      return SettlementFinder.FindHostSettlement(options);
    }

    private static FindSettlementResult FindSettlementWithExistingTournamentForPeace(MBSettlementList settlements, Payor payor)
    {
      var comparers = new IComparer<MBSettlement>[] { new ExistingTournamentComparer(payor, true) };
      var options = new FindHostSettlementOptions() { Candidates = settlements, Comparers = comparers };
      return SettlementFinder.FindHostSettlement(options);
    }

    public static CreateTournamentResult TryMakeLordTournamentForFaction(IMBFaction faction)
    {
      var findHostTownResult =
        HostTownFinder.FindForFaction(faction,
                                      FindHostSettlementOptions.RejectExistingTournaments
                                      );
      var payor = new Payor(findHostTownResult.Town.FactionLeader());

      return CreateTournament(new CreateTournamentOptions(findHostTownResult, TournamentType.Peace, payor));
    }

    public static CreateTournamentResult TryMakeHostTournament()
    {

    }

    public static CreateTournamentResult CreateInvitationTournamentFromSettlements(MBSettlementList settlements)
    {
      var result = HostTownFinder.FindHostTownFromSettlements(settlements);

      if (!result.Status)
      {
        return CreateInvitationTournamentFromFindSettlementResult(result);
      }
      else
      {
        return CreateTournamentResult.Failure;
      }
    }

    public static CreateTournamentResult CreateHostedTournamentAtSettlement(Settlement settlement)
    {
      TournamentRecords.CreateTournament(settlement, TournamentType.Hosted);

      Hero.MainHero.ChangeHeroGold(-Settings.Instance.TournamentCost);
      NotificationUtils.DisplayBannerMessage($"You've spent {Settings.Instance.TournamentCost.ToString()} gold on hosting a Tournament at {settlement.Town.Name}");

      return CreateTournamentResult.Success(settlement.Town);
    }

    public static void CreateInitialTournaments()
    {
      var townsWithExisting = MBTown.AllTownsWithTournaments;
      var townsWithoutExisting = MBTown.AllTownsWithoutTournaments;
      var numExisting = townsWithExisting.Count;
      var maxPossible = townsWithoutExisting.Count;
      var numRequested = Settings.Instance.TournamentInitialSpawnCount - numExisting;
      var numToCreate = (numRequested <= maxPossible) ? numRequested : maxPossible;

      CreateTournamentOptions options;
      foreach (var town in townsWithExisting)
      {
        options = new CreateTournamentOptions(FindSettlementResult.Success(town), TournamentType.Initial, Payor.NoPayor);
        CreateTournament(options);
      }

      var numCreated = 0;
      foreach (var town in townsWithoutExisting.Shuffle())
      {
        if (numCreated >= numToCreate)
        {
          break;
        }

        options = new CreateTournamentOptions(FindSettlementResult.Success(town), TournamentType.Initial, Payor.NoPayor);
        CreateTournament(options);

        numCreated++;
      }
    }


    private static CreateTournamentResult CreateTournament(CreateTournamentOptions options)
    {
      var town = options.Settlement;
      var type = options.Type;
      var townHadExistingTournament = town.HasTournament;

      if (!townHadExistingTournament)
      {
        InstantiateTournamentForTown(town);
        ApplyHostingEffectsOfTypeToTown(type, town);
      }

      DebitPayor(options.Payor);

      return CreateTournamentResult.Success(town, townHadExistingTournament);
    }

    private static void InstantiateTournamentForTown(Town town)
    {
      var tournament = new FightTournamentGame(town);
      Campaign.Current.TournamentManager.AddTournament(tournament);
    }


    private static void ApplyHostingEffectsOfTypeToTown(TournamentType type, Town town)
    {
      if (type == TournamentType.Initial)
      {
        return;
      }

      town.Settlement.Prosperity += Settings.Instance.ProsperityIncrease;
      town.Loyalty += Settings.Instance.LoyaltyIncrease;
      town.Security += Settings.Instance.SecurityIncrease;
      town.FoodStocks -= Settings.Instance.FoodStocksDecrease;

      if (town.MapFaction.Leader.IsHumanPlayerCharacter && Settings.Instance.SettlementStatNotification)
      {
        NotificationUtils.DisplayMessage(town.Name + "'s prosperity, loyalty and security have increased and food stocks have decreased");
      }
    }

    private static void DebitPayor(Payor payor)
    {
      if (payor.IsHero)
      {
        payor.Hero.ChangeHeroGold(-Settings.Instance.TournamentCost);
      }
      else if (payor.IsSettlement)
      {
        payor.Settlement.ChangeGold(-Settings.Instance.TournamentCost);
      }
    }

    public static void ApplyRelationsGainFromTypeInTown(TournamentType type, Town town)
    {
      if (type != TournamentType.Hosted)
      {
        return;
      }

      foreach (var notable in town.Settlement.Notables)
      {
        if (notable == Hero.MainHero)
        {
          continue;
        }

        notable.SetPersonalRelation(town.OwnerClan.Leader, notable.GetRelation(town.OwnerClan.Leader) + TournamentConstants.HostedTournamentEffects.NobleRelationshipModifier);
      }

      if (town.OwnerClan.Leader.IsHumanPlayerCharacter)
      {
        NotificationUtils.DisplayBannerMessage("Your relationship with local notables at " + town.Name + " has improved");
      }
    }

    public static ValueTuple<SkillObject, int> TournamentSkillXpGain(Hero winner)
    {
      float randomFloat = MBRandom.DeterministicRandom.NextFloat();
      SkillObject item = (randomFloat < 0.2f) ? DefaultSkills.OneHanded : ((randomFloat < 0.4f) ? DefaultSkills.TwoHanded : ((randomFloat < 0.6f) ? DefaultSkills.Polearm : ((randomFloat < 0.8f) ? DefaultSkills.Riding : DefaultSkills.Athletics)));
      int item2 = Settings.Instance.TournamentSkillXp;
      return new ValueTuple<SkillObject, int>(item, item2);
    }
  }
}
