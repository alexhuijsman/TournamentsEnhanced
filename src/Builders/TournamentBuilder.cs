using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced.Builders
{
  public static class TournamentBuilder
  {
    public static CreateTournamentResult TryMakePeaceTournamentForFaction(IFaction faction)
    {
      var findHostTownOptions = new FindHostTownOptions();
      var findHostTownResult = HostTownFinder.FindForFaction(faction, findHostTownOptions);
      var payor = new Payor(findHostTownResult.Town.FactionLeader());
      var createTournamentOptions = new CreateTournamentOptions(findHostTownResult, TournamentType.Peace, payor);

      return CreateTournament(createTournamentOptions);
    }
    public static CreateTournamentResult TryMakeLordTournamentForFaction(IFaction faction)
    {
      var findHostTownResult =
        HostTownFinder.FindForFaction(faction,
                                      FindHostTownOptions.RejectExistingTournaments
                                      );
      var payor = new Payor(findHostTownResult.Town.FactionLeader());

      return CreateTournament(new CreateTournamentOptions(findHostTownResult, TournamentType.Peace, payor));
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
        options = new CreateTournamentOptions(FindHostTownResult.Success(town), TournamentType.Initial, Payor.NoPayor);
        CreateTournament(options);
      }

      var numCreated = 0;
      foreach (var town in townsWithoutExisting.Shuffle())
      {
        if (numCreated >= numToCreate)
        {
          break;
        }

        options = new CreateTournamentOptions(FindHostTownResult.Success(town), TournamentType.Initial, Payor.NoPayor);
        CreateTournament(options);

        numCreated++;
      }
    }


    private static CreateTournamentResult CreateTournament(CreateTournamentOptions options)
    {
      var town = options.town;
      var type = options.type;
      var townHadExistingTournament = town.HasTournament;

      if (!townHadExistingTournament)
      {
        InstantiateTournamentForTown(town);
        ApplyHostingEffectsOfTypeToTown(type, town);
      }

      DebitPayor(options.payor);

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
      else if (payor.IsTown)
      {
        payor.Town.ChangeGold(-Settings.Instance.TournamentCost);
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

    private class CreateTournamentOptions
    {
      public readonly Town town;
      public readonly TournamentType type;
      public readonly Payor payor;

      public CreateTournamentOptions(FindHostTownResult result, TournamentType type, Payor payor)
      {
        this.town = result.Town;
        this.type = type;
        this.payor = payor;
      }
    }
  }
}
