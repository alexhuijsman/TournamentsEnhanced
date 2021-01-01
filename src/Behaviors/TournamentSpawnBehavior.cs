using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;

using TournamentsEnhanced.Wrappers;

namespace TournamentsEnhanced
{
  class TournamentSpawnBehavior : EncounterGameMenuBehavior
  {
    public override void RegisterEvents()
    {
      CampaignEvents.OnGivenBirthEvent
        .AddNonSerializedListener(this, new Action<Hero, List<Hero>, int>(this.OnGivenBirth));
      CampaignEvents.WeeklyTickEvent
        .AddNonSerializedListener(this, new Action(this.WeeklyTick));
      CampaignEvents.HeroesMarried
        .AddNonSerializedListener(this, new Action<Hero, Hero, bool>(this.OnHeroesMarried));
      CampaignEvents.MakePeace
        .AddNonSerializedListener(this, new Action<IFaction, IFaction>(this.OnMakePeace));
      CampaignEvents.DailyTickEvent
        .AddNonSerializedListener(this, new Action(this.DailyTick));
    }

    private void WeeklyTick()
    {
      TrySpawnLordTournament();
      SerializableState.WeeksSinceHostedTournament++;
      InvitePlayer();
    }

    private static void InvitePlayer()
    {
      if (Hero.MainHero.Clan.Renown > 800.00f || MBRandom.RandomFloat >= 0.8f)
      {
        return;
      }

      var settlements = Settlement.FindSettlementsAroundPosition(Hero.MainHero.GetPosition().AsVec2, 60.00f).ToList().Shuffle();

      TournamentBuilder.CreateInvitationTournamentFromSettlements(settlements);
    }

    private void DailyTick()
    {
      TrySpawnProsperityTournament();
    }

    private void TrySpawnLordTournament()
    {
      foreach (var kingdom in MBKingdom.All)
      {
        if (kingdom.Leader.Clan.Settlements.IsEmpty() ||
            kingdom.Leader == MBHero.MainHero ||
            MBRandom.RandomFloat >= 0.7)
        {
          continue;
        }

        TournamentBuilder.TryMakeLordTournamentForFaction(kingdom.MapFaction);

        break;
      }
    }

    public bool SettlementIsEligibleForProsperityTournament(MBSettlement settlement)
    {
      return settlement.IsTown &&
            !settlement.Town.HasTournament &&
             settlement.Prosperity >= 5000.00f &&
             settlement.Town.Gold >= 10000;
    }

    private void TrySpawnProsperityTournament()
    {
      var settlements =
        MBSettlement
          .All
          .FindAll(settlement => SettlementIsEligibleForProsperityTournament(settlement));
      settlements.Shuffle();

      TrySpawnProsperityTournamentInSettlements(settlements);
    }

    private CreateTournamentResult TrySpawnProsperityTournamentInSettlements(MBSettlementList settlements)
    {
      var finderOptions = new Options
      var result = HostTownFinder.FindInSettlements(settlements);

      if (result.Status)
      {
        return CreateProsperityTournamentFromFindSettlementResult(result);
      }
      else
      {
        return CreateTournamentResult.Failure;
      }
    }

    private void OnMakePeace(IFaction factionA, IFaction factionB)
    {
      var resultsA = TournamentBuilder.Create(factionA);
      var resultsB = TournamentBuilder.TryCreateTournamentForFaction(factionB);

      if (!Settings.Instance.PeaceNotification || (!resultsA.Succeeded && !resultsB.Succeeded))
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

      NotificationUtils.DisplayMessage(
        $"To celebrate the peace of { factionA.Name } and { factionB.Name }, faction leaders have called a tournament at { hostTownNames }");

    }

    private void OnHeroesMarried(Hero firstHero, Hero secondHero, bool showNotification)
    {
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

    private void OnGivenBirth(Hero mother, List<Hero> aliveChildren, int stillBornCount)
    {
      var resultsA = TournamentBuilder.TryCreateTournamentTypeInTownLedByAny(TournamentType.Birth, mother, mother.Spouse);

      foreach (var settlement in settlements)
      {
        if (maxTournaments >= MAX_TOURNAMENTS)
        {
          break;
        }
        if (settlement.IsTown)
        {
          if (!settlement.Town.HasTournament && settlement.OwnerClan.Leader.Name.Equals(mother.Name) || mother.Spouse != null && settlement.OwnerClan.Leader.Name.Equals(mother.Spouse.Name))
          {
            TournamentBuilder.CreateTournament(settlement, TournamentType.Birth);
            NotificationUtils.DisplayBannerMessage("To celebrate the birth of " + mother.Name + "and " + mother.Spouse.Name + "'s child, local nobles have called a tournament at " + settlement.Town.Name);
            BannerlordUtils.WeddingSettlementStatChange(settlement);
            maxTournaments++;
          }
          else if (settlement.Town.HasTournament && settlement.OwnerClan.Leader.Name.Equals(mother.Name) || mother.Spouse != null && settlement.OwnerClan.Leader.Name.Equals(mother.Spouse.Name))
          {
            NotificationUtils.DisplayBannerMessage("To celebrate the birth of " + mother.Name + " and " + mother.Spouse.Name + "'s child, local nobles have called a tournament at " + settlement.Town.Name);
            BannerlordUtils.WeddingSettlementStatChange(settlement);
            maxTournaments++;
          }
        }
      }
    }
  }
}
