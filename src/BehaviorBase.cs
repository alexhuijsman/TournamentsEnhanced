using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TournamentsEnhanced
{
  class BehaviorBase : EncounterGameMenuBehavior
  {
    public static int WeeksSinceHostedTournament { get; set; }

    private const int MAX_TOURNAMENTS = 2;

    public static bool PrizeSelectMenuOptionCondition(MenuCallbackArgs args)
    {
      bool shouldBeDisabled;
      TextObject disabledText;
      bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(Settlement.CurrentSettlement,
        SettlementAccessModel.SettlementAction.JoinTournament, out shouldBeDisabled, out disabledText);
      args.optionLeaveType = GameMenuOption.LeaveType.Manage;
      return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
    }

    public static void PrizeSelectConsequence(MenuCallbackArgs args)
    {
      var prizeList = ItemUtils.GetRandomlySelectedPrizeList();
      InformationManagerUtils.ShowMultiSelectionScreenFromItemList(prizeList);
    }

    private static List<InquiryElement> CreateInquiryElementsFromItems(IList<ItemObject> itemList)
    {
      var inquiryElements = new List<InquiryElement>();

      foreach (var item in itemList)
      {
        inquiryElements.Add(CreateInquiryElementFromItem(item));
      }

      return inquiryElements;
    }

    public override void RegisterEvents()
    {
      CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
      CampaignEvents.OnGivenBirthEvent.AddNonSerializedListener(this, new Action<Hero, List<Hero>, int>(this.OnGivenBirth));
      CampaignEvents.WeeklyTickEvent.AddNonSerializedListener(this, new Action(this.WeeklyTick));
      CampaignEvents.HeroesMarried.AddNonSerializedListener(this, new Action<Hero, Hero, bool>(this.OnHeroesMarried));
      CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction>(this.OnMakePeace));
      CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.DailyTick));
      CampaignEvents.TournamentWon.AddNonSerializedListener(this, new Action<CharacterObject, Town>(this.OnTournamentWin));
    }

    private static void OnSelectPrize(List<InquiryElement> prizes)
    {
      if (prizes.Count > 0)
      {
        TournamentGame tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(Settlement.CurrentSettlement.Town);
        ItemObject prize = MBObjectManager.Instance.GetObject<ItemObject>(prizes.First().Identifier.ToString());
        typeof(TournamentGame).GetProperty("Prize").SetValue(tournamentGame, prize);
        GameMenu.SwitchToMenu("town_arena");
      }

    }

    private static void OnDeSelectPrize(List<InquiryElement> prizeSelections)
    {

    }

    private static void InvitePlayer()
    {
      if (Hero.MainHero.Clan.Renown <= 800.00f && MBRandom.RandomFloat < 0.8f)
      {
        IEnumerable<Settlement> settlementList = Settlement.FindSettlementsAroundPosition(Hero.MainHero.GetPosition().AsVec2, 60.00f);
        for (int i = 0; i < settlementList.Count(); i++)
        {
          Settlement settlement = settlementList.GetRandomElement();
          if (settlement.IsTown)
          {
            if (!settlement.Town.HasTournament)
            {
              TournamentUtils.CreateTournament(settlement, TournamentType.Invitation);
            }
            NotificationUtils.DisplayBannerMessage("A local lord is looking for tournament contestants at " + settlement.Town.Name);
            break;
          }
        }
      }
    }

    private static bool game_menu_town_arena_host_tournament_condition(MenuCallbackArgs args)
    {
      args.optionLeaveType = GameMenuOption.LeaveType.Continue;
      return !Settlement.CurrentSettlement.Town.HasTournament && Settlement.CurrentSettlement.IsTown && Settlement.CurrentSettlement.OwnerClan.Leader.IsHumanPlayerCharacter && weeksSinceHost >= 1;
    }

    private static void game_menu_town_arena_host_tournament_consequence(MenuCallbackArgs args)
    {
      Settlement settlement = Settlement.CurrentSettlement;
      if (settlement.IsTown)
      {
        settlement.OwnerClan.Leader.ChangeHeroGold(-TournamentsEnhancedSettings.Instance.TournamentCost);
        TournamentUtils.CreateTournament(settlement, TournamentType.Hosted);
        NotificationUtils.DisplayBannerMessage("You've spent " + TournamentsEnhancedSettings.Instance.TournamentCost.ToString() + " gold on hosting Tournament at " + settlement.Town.Name);
        BannerlordUtils.HostedSettlementStatChange(settlement);
        BannerlordUtils.LocalRelationStatChange(settlement);
        weeksSinceHost = 0;
        GameMenu.ActivateGameMenu("town_arena");
        return;
      }
    }

    private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
    {
      //add option to the menu
      campaignGameStarter.AddGameMenuOption("town_arena", "host_tournament", "Host a tournament in your honor (" +
        TournamentsEnhancedSettings.Instance.TournamentCost.ToString() + "{GOLD_ICON})",
        new GameMenuOption.OnConditionDelegate(game_menu_town_arena_host_tournament_condition),
        new GameMenuOption.OnConsequenceDelegate(game_menu_town_arena_host_tournament_consequence), false, 1, false);

      campaignGameStarter.AddGameMenuOption("town_arena", "select_prize", "Select your prize",
        new GameMenuOption.OnConditionDelegate(BehaviorBase.PrizeSelectMenuOptionCondition),
        new GameMenuOption.OnConsequenceDelegate(BehaviorBase.PrizeSelectConsequence), false, 1, true);
    }

    private void OnTournamentWin(CharacterObject character, Town town)
    {
      ValueTuple<SkillObject, int> tuple = TournamentUtils.TournamentSkillXpGain(character.HeroObject);
      if (character.IsHero)
      {
        character.HeroObject.AddSkillXp(tuple.Item1, tuple.Item2);
      }
      TournamentKB.Remove(town.Settlement);
    }

    private void WeeklyTick()
    {
      OnLordTournament();
      weeksSinceHost++;
      InvitePlayer();
    }

    private void DailyTick()
    {

      LimitPrizeSelect = false;
      OnProsperityTournament();
    }

    private void OnLordTournament()
    {
      foreach (var kingdom in Kingdom.All)
      {
        if (!kingdom.Leader.Clan.Settlements.IsEmpty() && MBRandom.RandomFloat < 0.7)
        {
          foreach (var settlement in kingdom.RulingClan.Settlements)
          {
            if (settlement.IsTown && !settlement.Town.HasTournament)
            {
              TournamentUtils.CreateTournament(settlement, TournamentType.Lord);
              if (Hero.MainHero.Clan.Kingdom != null && Hero.MainHero.Clan.Kingdom.Name.Equals(kingdom.Name))
              {
                NotificationUtils.DisplayBannerMessage(kingdom.Leader.Name + " invites you to a Highborn tournament at " + settlement.Name);
              }
              break;
            }
          }
        }
      }
    }

    private void OnProsperityTournament()
    {
      var settlements = GetShuffledSettlements();

      foreach (var settlement in settlements)
      {
        if (settlement.IsEligibleForProsperityTournament() && MBRandom.RandomFloat < 0.08f)
        {
          TournamentUtils.CreateTournament(settlement, TournamentType.Prosperity);
          if (settlement.OwnerClan.Leader.IsHumanPlayerCharacter)
          {
            NotificationUtils.DisplayBannerMessage("Local nobles at " + settlement.Town.Name + " have called a tournament, using " + TournamentsEnhancedSettings.Instance.TournamentCost.ToString() + " gold from clan coffers.");
          }
          else
          {
            if (TournamentsEnhancedSettings.Instance.ProsperityNotification)
            {
              NotificationUtils.DisplayMessage("Local nobles at " + settlement.Town.Name + " have called a tournament due to high prosperity");
            }
          }
          settlement.OwnerClan.Leader.ChangeHeroGold(-TournamentsEnhancedSettings.Instance.TournamentCost);
          BannerlordUtils.LocalRelationStatChange(settlement);
        }
      }
    }

    private IList<Settlement> GetShuffledSettlements()
    {
      var settlements = Settlement.All.ToList();
      settlements.Shuffle();

      return settlements;
    }

    private void OnMakePeace(IFaction factionA, IFaction factionB)
    {
      var settlements = GetShuffledSettlements();
      var leaderA = factionA.Leader;
      var leaderB = factionB.Leader;

      int numberOfNewTournaments = 0;
      foreach (var settlement in settlements)
      {
        if (numberOfNewTournaments == MAX_TOURNAMENTS)
        {
          break;
        }

        if (!settlement.IsTown || settlement.Town.HasTournament)
        {
          continue;
        }

        TextObject townClanLeaderName = settlement.OwnerClan.Leader.Name;

        if (!settlement.IsLedBy(leaderA) && !settlement.IsLedBy(leaderB))
        {
          continue;
        }

        BannerlordUtils.CreatePeaceTournament(settlement, TournamentType.Peace);

        if (TournamentsEnhancedSettings.Instance.PeaceNotification)
        {
          NotificationUtils.DisplayMessage("To celebrate the peace of " + factionA.Name + " and " + factionB.Name + ", faction leaders have called a tournament at " + settlement.Town.Name);
        }

        numberOfNewTournaments++;
      }
    }

    private void OnHeroesMarried(Hero firstHero, Hero secondHero, bool showNotification)
    {
      int maxTournaments = 0;
      var settlements = GetShuffledSettlements();
      foreach (var settlement in settlements)
      {
        if (maxTournaments >= MAX_TOURNAMENTS)
        {
          break;
        }
        if (settlement.IsTown)
        {
          if (!settlement.Town.HasTournament && settlement.OwnerClan.Leader.Name.Equals(firstHero.Name) || settlement.OwnerClan.Leader.Name.Equals(secondHero.Name))
          {
            TournamentUtils.CreateTournament(settlement, TournamentType.Wedding);
            NotificationUtils.DisplayBannerMessage("To celebrate the wedding of " + firstHero.Name + " and " + secondHero.Name + ", local nobles have called a tournament at " + settlement.Town.Name);
            BannerlordUtils.WeddingSettlementStatChange(settlement);
            maxTournaments++;
          }
          else if (settlement.Town.HasTournament && settlement.OwnerClan.Leader.Name.Equals(firstHero.Name) || settlement.OwnerClan.Leader.Name.Equals(secondHero.Name))
          {
            NotificationUtils.DisplayBannerMessage("To celebrate the wedding of " + firstHero.Name + " and " + secondHero.Name + ", local nobles have called a tournament at " + settlement.Town.Name);
            BannerlordUtils.WeddingSettlementStatChange(settlement);
            maxTournaments++;
          }
        }
      }
    }

    private void OnGivenBirth(Hero mother, List<Hero> aliveChildren, int stillBornCount)
    {
      int maxTournaments = 0;
      var settlements = GetShuffledSettlements();
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
            TournamentUtils.CreateTournament(settlement, TournamentType.Birth);
            NotificationUtils.DisplayBannerMessage("To celebrate the birth of " + mother.Name + " and " + mother.Spouse.Name + "'s child, local nobles have called a tournament at " + settlement.Town.Name);
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
