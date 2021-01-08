using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

using TournamentsEnhanced.Behaviors.Abstract;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Behaviors
{
  class TournamentSpawnBehavior : MBEncounterGameMenuBehavior
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
      if (TryCreateLordTournament().Failed)
      {
        TryCreateInvitationTournament();
      }
      ModState.WeeksSinceHostedTournament++;
    }

    private static void TryCreateInvitationTournament()
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
      TryCreateProsperityTournament();
    }

    private void TryCreateLordTournament()
    {
      foreach (var kingdom in MBKingdom.All)
      {
        if (kingdom.Leader.Clan.Settlements.IsEmpty() ||
            kingdom.Leader == MBHero.MainHero ||
            MBRandom.RandomFloat >= 0.7)
        {
          continue;
        }

        var result = TournamentBuilder.TryCreateLordTournament(kingdom.MapFaction);

        if (result.Succeeded)
        {
          MBInformationManager.DisplayBanner(kingdom.Leader.Name + " invites you to a Highborn tournament at " + result.HostSettlement.Name);
        }


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

    private void TryCreateProsperityTournament()
    {
      var result = TournamentBuilder.TryCreateProsperityTournament();

      if (result.Succeeded)
      {

      }
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

    private void OnMakePeace(IFaction a, IFaction b)
    {
      var factionA = a.ToIMBFaction();
      var factionB = b.ToIMBFaction();

      OnMakePeace(factionA, factionB);
    }

    private void OnMakePeace(IMBFaction factionA, IMBFaction factionB)
    {
      var resultsA = TournamentBuilder.TryCreatePeaceTournamentForFaction(factionA);
      var resultsB = TournamentBuilder.TryCreatePeaceTournamentForFaction(factionB);

      if (!Settings.Instance.PeaceNotification ||
         (!resultsA.Succeeded && !resultsB.Succeeded))
      {
        return;
      }

      string hostTownNames;
      if (resultsA.Succeeded && resultsB.Succeeded)
      {
        hostTownNames = $"{resultsA.HostSettlement.Name} and {resultsB.HostSettlement.Name}";
      }
      else
      {
        hostTownNames = resultsA.Succeeded ? $"{resultsA.HostSettlement.Name}" : $"{resultsB.HostSettlement.Name}";
      }

      NotificationUtils.DisplayMessage(
        $"To celebrate the peace of { factionA.Name } and { factionB.Name }, faction leaders have called a tournament at { hostTownNames }");

    }

    private void OnHeroesMarried(Hero firstHero, Hero secondHero, bool showNotification)
    {
      var result = TournamentBuilder.TryCreateWeddingTournament(firstHero, secondHero);

      if (result.Succeeded)
      {
        NotificationUtils.DisplayBannerMessage($"To celebrate the wedding of {firstHero.Name} and {secondHero.Name}, local nobles have called a tournament at {result.HostSettlement.Name}");
      }
    }

    private void OnGivenBirth(Hero mother, List<Hero> aliveChildren, int stillBornCount)
    {
      var resultsA = TournamentBuilder.TryMakeBirthTournamentForMother(mother);

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
