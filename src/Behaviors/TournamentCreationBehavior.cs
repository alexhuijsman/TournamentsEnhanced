﻿using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Behaviors.Abstract;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Behaviors
{
  class TournamentCreationBehavior : MBEncounterGameMenuBehavior
  {
    protected TournamentBuilder TournamentBuilder { get; set; } = TournamentBuilder.Instance;
    protected ModState ModState { get; set; } = ModState.Instance;
    protected Settings Settings { get; set; } = Settings.Instance;
    protected MBHero MBHero { get; set; } = MBHero.Instance;
    protected MBInformationManagerFacade MBInformationManagerFacade { get; set; } = MBInformationManagerFacade.Instance;

    public override void RegisterEvents()
    {
      MBCampaignEvents.DailyTickEvent
        .AddNonSerializedListener(this, new Action(this.DailyTick));
      MBCampaignEvents.MakePeace
        .AddNonSerializedListener(this, new Action<IFaction, IFaction>(this.OnMakePeace));
      MBCampaignEvents.HeroesMarried
        .AddNonSerializedListener(this, new Action<Hero, Hero, bool>(this.OnHeroesMarried));
      MBCampaignEvents.OnGivenBirthEvent
        .AddNonSerializedListener(this, new Action<Hero, List<Hero>, int>(this.OnGivenBirth));
    }

    private void DailyTick()
    {
      TryCreateTournaments();
    }

    private void OnMakePeace(IFaction a, IFaction b)
    {
      var factionA = a.ToIMBFaction();
      var factionB = b.ToIMBFaction();

      var result = TryCreatePeaceTournaments(factionA, factionB);

      if (!Settings.PeaceNotification || result.Failed)
      {
        return;
      }

      MBInformationManagerFacade.DisplayAsLogEntry(
        $"To celebrate the peace between { factionA.Name } and { factionB.Name }, faction leaders have called a tournament at { result.HostSettlementNames }");
    }

    private CreatePeaceTournamentsResult TryCreatePeaceTournaments(IMBFaction factionA, IMBFaction factionB)
    {
      var factionAResults = TournamentBuilder.TryCreatePeaceTournamentForFaction(factionA);
      var factionBResults = TournamentBuilder.TryCreatePeaceTournamentForFaction(factionB);

      if (factionAResults.Failed && factionBResults.Failed)
      {
        return CreatePeaceTournamentsResult.Failure;
      }

      return CreatePeaceTournamentsResult.Success(factionAResults, factionBResults);
    }

    private void OnHeroesMarried(Hero firstHero, Hero secondHero, bool showNotification)
    {
      var result = TournamentBuilder.TryCreateWeddingTournament(firstHero, secondHero);

      if (result.Succeeded)
      {
        MBInformationManagerFacade.DisplayAsQuickBanner(
          $"To celebrate the wedding of {firstHero.Name} and {secondHero.Name}, local nobles have called a tournament at {result.HostSettlement.Name}");
      }
    }

    private void OnGivenBirth(Hero mother, List<Hero> aliveChildren, int stillBornCount)
    {
      var result = TournamentBuilder.TryMakeBirthTournament(mother);

      MBInformationManagerFacade.DisplayAsQuickBanner(
        $"To celebrate the birth of {mother.Name} and {mother.Spouse.Name}'s child, local nobles have called a tournament at {result.HostSettlement.Name}");
    }


    private void TryCreateTournaments()
    {
      TryCreateProsperityTournament();
      TryCreateHighbornTournament();
      TryCreateInvitationTournament();
    }

    private void TryCreateProsperityTournament()
    {
      if (!ModState.IsLotteryWinner(TournamentType.Prosperity))
      {
        return;
      }

      var result = TournamentBuilder.TryCreateProsperityTournament();

      if (result.Succeeded)
      {
        MBInformationManagerFacade.DisplayAsLogEntry($"Local nobles at {result.HostSettlement.Name} have called a tournament due to high prosperity");
      }
    }

    private void TryCreateHighbornTournament()
    {
      if (!ModState.IsLotteryWinner(TournamentType.Highborn))
      {
        return;
      }

      var result = TournamentBuilder.TryCreateHighbornTournament();

      if (result.Succeeded)
      {
        MBInformationManagerFacade.DisplayAsQuickBanner($"{result.Payor.Name} invites you to a Highborn tournament at {result.HostSettlement.Name}");
      }
    }

    private void TryCreateInvitationTournament()
    {
      if (!ModState.IsLotteryWinner(TournamentType.Invitation) &&
          MBHero.MainHero.Clan.Renown >= Settings.MaxRenownForInvitationTournaments)
      {
        return;
      }
      var result = TournamentBuilder.TryCreateInvitationTournament();

      if (result.Succeeded)
      {
        MBInformationManagerFacade.DisplayAsQuickBanner("A local lord is looking for tournament contestants at " + result.HostSettlement.Name);
      }
    }
  }
}
