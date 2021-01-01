using System;
using System.Collections.Generic;
using System.Linq;

using Helpers;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

using TournamentsEnhanced.Wrappers;

using static TaleWorlds.CampaignSystem.GameMenus.GameMenuOption;

namespace TournamentsEnhanced
{
  class TownMenuBehavior : EncounterGameMenuBehavior
  {
    //TODO remove tournament spawn behavior

    public static bool PrizeSelectCondition(MenuCallbackArgs args)
    {
      bool shouldBeDisabled;
      MBTextObject disabledText;
      bool canPlayerDo = MBCampaign.CanMainHeroJoinTournamentAtCurrentSettlement(out shouldBeDisabled, out disabledText);
      args.optionLeaveType = LeaveType.Manage;
      return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
    }

    public static void PrizeSelectConsequence(MenuCallbackArgs args)
    {
      var prizeList = MBItemObject.GetAvailableTournamentPrizes();
      MBInformationManager.ShowPrizeSelectionMenu(prizeList, OnSelectPrize, OnDeSelectPrize);
      GameMenu.SwitchToMenu("town_arena");
    }

    public override void RegisterEvents()
    {
      CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
      CampaignEvents.OnGivenBirthEvent.AddNonSerializedListener(this, new Action<Hero, List<Hero>, int>(this.OnGivenBirth));
      CampaignEvents.WeeklyTickEvent.AddNonSerializedListener(this, new Action(this.WeeklyTick));
      CampaignEvents.HeroesMarried.AddNonSerializedListener(this, new Action<Hero, Hero, bool>(this.OnHeroesMarried));
      CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction>(this.OnMakePeace));
      CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.DailyTick));
      CampaignEvents.TournamentFinished.AddNonSerializedListener(this, new Action<CharacterObject, Town>(this.OnTournamentWin));
    }

    private static void OnSelectPrize(MBInquiryElementList prizes)
    {
      if (prizes.Count > 0)
      {
        TournamentGame tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(MBSettlement.CurrentTown);
        ItemObject prize = MBObjectManager.Instance.GetObject<ItemObject>(prizes.First().Identifier.ToString());
        typeof(TournamentGame).GetProperty("Prize").SetValue(tournamentGame, prize);
        GameMenu.SwitchToMenu("town_arena");
      }

    }

    private static void OnDeSelectPrize(MBInquiryElementList prizeSelections)
    {

    }



    private static bool game_menu_town_arena_host_tournament_condition(MenuCallbackArgs args)
    {
      args.optionLeaveType = GameMenuOption.LeaveType.Continue;
      return !Settlement.CurrentSettlement.Town.HasTournament &&
              Settlement.CurrentSettlement.IsTown &&
              Settlement.CurrentSettlement.OwnerClan.Leader.IsHumanPlayerCharacter &&
              SerializableState.Instance.weeksSinceHosted >= 1;
    }

    private static void game_menu_town_arena_host_tournament_consequence(MenuCallbackArgs args)
    {
      TournamentBuilder.CreateHostedTournamentAtSettlement(Settlement.CurrentSettlement);
      SerializableState.WeeksSinceHostedTournament = 0;
      GameMenu.ActivateGameMenu("town_arena");
    }

    private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
    {
      campaignGameStarter.AddGameMenuOption("town_arena", "host_tournament", "Host a tournament in your honor (" +
        Settings.Instance.TournamentCost.ToString() + "{GOLD_ICON})",
        new GameMenuOption.OnConditionDelegate(game_menu_town_arena_host_tournament_condition),
        new GameMenuOption.OnConsequenceDelegate(game_menu_town_arena_host_tournament_consequence), false, 1, false);

      campaignGameStarter.AddGameMenuOption("town_arena", "select_prize", "Select your prize",
        new GameMenuOption.OnConditionDelegate(TownMenuBehavior.PrizeSelectCondition),
        new GameMenuOption.OnConsequenceDelegate(TownMenuBehavior.PrizeSelectConsequence), false, 1, true);
    }

    private void OnTournamentWin(CharacterObject character, Town town)
    {
      ValueTuple<SkillObject, int> tuple = TournamentBuilder.TournamentSkillXpGain(character.HeroObject);
      if (character.IsHero)
      {
        character.HeroObject.AddSkillXp(tuple.Item1, tuple.Item2);
      }
      TournamentRecords.RemoveByTown(town);
    }

    private void WeeklyTick()
    {
      TryCreateLordTournament();
      WeeksSinceHostedTournament++;
      InvitePlayer();
    }

    private void DailyTick()
    {
      TryCreateProsperityTournament();
    }

    private void TryCreateLordTournament()
    {
      foreach (var kingdom in Kingdom.All)
      {
        if (kingdom.Leader.Clan.Settlements.IsEmpty() || MBRandom.RandomFloat >= 0.7)
        {
          continue;
        }

        TournamentBuilder.TryMakeLordTournamentForFaction(kingdom);

        break;
      }
    }

    private void TryCreateProsperityTournament()
    {
      var settlements = MBSettlement.All;
      settlements.Shuffle();

      TournamentBuilder.CreateProsperityTournamentInSettlements(settlements);
      foreach (var settlement in settlements)
      {
        if (settlement.IsEligibleForProsperityTournament() && MBRandom.RandomFloat < 0.08f)
        {
          TournamentBuilder.CreateTournament(settlement, TournamentType.Prosperity);
          if (settlement.OwnerClan.Leader.IsHumanPlayerCharacter)
          {
            NotificationUtils.DisplayBannerMessage("Local nobles at " + settlement.Town.Name + " have called a tournament, using " + Settings.Instance.TournamentCost.ToString() + " gold from clan coffers.");
          }
          else
          {
            if (Settings.Instance.ProsperityNotification)
            {
              NotificationUtils.DisplayMessage("Local nobles at " + settlement.Town.Name + " have called a tournament due to high prosperity");
            }
          }
        }
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
