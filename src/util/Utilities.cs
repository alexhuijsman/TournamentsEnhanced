using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TournamentsEnhanced
{
  public class Utilities
  {
    private const int Tier3 = 3;
    private const int Tier4 = 4;
    private const int Tier5 = 5;

    public static bool IsTierable(ItemObject item)
    {
      if (/*item.ItemType.Equals(ItemObject.ItemTypeEnum.Arrows) ||*/ item.ItemType.Equals(ItemObject.ItemTypeEnum.BodyArmor) || item.ItemType.Equals(ItemObject.ItemTypeEnum.Bow) ||
        item.ItemType.Equals(ItemObject.ItemTypeEnum.Cape) || item.ItemType.Equals(ItemObject.ItemTypeEnum.ChestArmor) || item.ItemType.Equals(ItemObject.ItemTypeEnum.Crossbow) ||
        item.ItemType.Equals(ItemObject.ItemTypeEnum.HandArmor) || item.ItemType.Equals(ItemObject.ItemTypeEnum.HeadArmor) || item.ItemType.Equals(ItemObject.ItemTypeEnum.HorseHarness) ||
        item.ItemType.Equals(ItemObject.ItemTypeEnum.LegArmor) || item.ItemType.Equals(ItemObject.ItemTypeEnum.OneHandedWeapon) || item.ItemType.Equals(ItemObject.ItemTypeEnum.Polearm) ||
        item.ItemType.Equals(ItemObject.ItemTypeEnum.Shield) || item.ItemType.Equals(ItemObject.ItemTypeEnum.Horse) || item.ItemType.Equals(ItemObject.ItemTypeEnum.Thrown) || item.ItemType.Equals(ItemObject.ItemTypeEnum.TwoHandedWeapon))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public static int RewardTier()
    {
      if (Hero.MainHero.Clan.Renown < TournamentsEnhancedSettings.Instance.MinimumRenownForTier4Prizes)
      {
        return Tier3;
      }
      else if (Hero.MainHero.Clan.Renown < TournamentsEnhancedSettings.Instance.MinimumRenownForTier5Prizes)
      {
        return Tier4;
      }
      else
      {
        return Tier5;
      }
    }

    public static ItemObject RandomObject()
    {
      List<ItemObject>.Enumerator enumerator = ItemObject.All.GetEnumerator();
      List<ItemObject> tier = new List<ItemObject>();

      while (enumerator.MoveNext())
      {
        if (Utilities.IsTierable(enumerator.Current) && enumerator.Current.Tier.Equals((ItemObject.ItemTiers)Utilities.RewardTier()))
        {
          tier.Add(enumerator.Current);
        }
      }
      if (!tier.IsEmpty())
      {
        return tier.GetRandomElement();
      }
      return ItemObject.All.GetRandomElement();
    }

    //announce in log
    public static void LogAnnouncer(string text)
    {
      InformationManager.DisplayMessage(new InformationMessage(text));
    }

    public static void BannerAnnouncer(string text)
    {
      InformationManager.AddQuickInformation(new TextObject(text));
    }

    //create tournament after various events
    public static void CreateTournament(Settlement settlement, TournamentType type)
    {
      TournamentGame tournament = new FightTournamentGame(settlement.Town);
      Campaign.Current.TournamentManager.AddTournament(tournament);
      TournamentKB tournamentKB = new TournamentKB(settlement, type, tournament);
    }

    public static void CreateInitialTournaments()
    {
      int max = TournamentsEnhancedSettings.Instance.TournamentInitialSpawnCount;
      for (int i = max; i >= 1; i--)
      {
        Settlement settlement = Settlement.All.GetRandomElement();
        if (!settlement.IsTown || settlement.Town.HasTournament)
        {
          i++;
          continue;
        }
        else
        {
          CreateTournament(settlement, TournamentType.Vanilla);
        }
      }
    }

    //Settlements with over 5000 prosperity should have auto-spawning tournaments
    public static bool SettlementProsperityCheck(Settlement settlement)
    {
      return settlement.IsTown && settlement.Prosperity >= 5000.00f && settlement.OwnerClan.Leader.Gold >= 10000;
    }

    public static void WeddingSettlementStatChange(Settlement settlement)
    {
      settlement.Prosperity += TournamentsEnhancedSettings.Instance.ProsperityIncrease;
      settlement.Town.Loyalty += TournamentsEnhancedSettings.Instance.LoyaltyIncrease;
      settlement.Town.Security += TournamentsEnhancedSettings.Instance.SecurityIncrease;
      settlement.Town.FoodStocks -= TournamentsEnhancedSettings.Instance.FoodStocksDecrease;

      if (settlement.OwnerClan.Leader.IsHumanPlayerCharacter && TournamentsEnhancedSettings.Instance.SettlementStatNotification)
      {
        LogAnnouncer(settlement.Town.Name + "'s prosperity, loyalty and security have increased and food stocks have decreased");
      }
    }

    public static void HostedSettlementStatChange(Settlement settlement)
    {
      WeddingSettlementStatChange(settlement);
    }

    public static void LocalRelationStatChange(Settlement settlement)
    {
      MBReadOnlyList<Hero> notableList = settlement.Notables;
      List<Hero>.Enumerator enumerator = notableList.GetEnumerator();

      while (enumerator.MoveNext())
      {
        if (!enumerator.Current.Name.Equals(settlement.OwnerClan.Leader.Name))
        {
          enumerator.Current.SetPersonalRelation(settlement.OwnerClan.Leader, enumerator.Current.GetRelation(settlement.OwnerClan.Leader) + RELATIONSHIP_MODIFIER);
        }
      }

      if (settlement.OwnerClan.Leader.IsHumanPlayerCharacter)
      {
        BannerAnnouncer("Your relationship with local notables at " + settlement.Town.Name + " has improved");
      }
    }

    public static ItemObject[] GetTournamentPrizes()
    {
      var itemPool = ItemObject.All.ToList().Shuffle();
      var qualifyingItems = new List<ItemObject>();

      foreach (var item in itemPool)
      {
        if (item.IsCraftedByPlayer || !Utilities.IsTierable(item))
        {
          continue;
        }

        if (item.Tier.Equals((ItemObject.ItemTiers)Utilities.RewardTier()))
        {
          qualifyingItems.Add(item);
        }
      }

      var weaponPrizes = qualifyingItems.Where(item => item.IsCraftedWeapon).ToList().Shuffle();
      var selectedPrizes 
        = new ItemObject[
          TournamentsEnhancedSettings.Instance.DebugShowAllAvailablePrizes ? 
            qualifyingItems.Count : 
            5];

      for (int i = 0; i < selectedPrizes.Length; i++)
      {
        if (i < weaponPrizes.Count &&
            i + 1 <= TournamentsEnhancedSettings.Instance.MinimumNumberOfWeaponsInPrizePool)
        {
          selectedPrizes[i] = weaponPrizes.GetRandomElement();
          weaponPrizes.Remove(selectedPrizes[i]);
        }
        else
        {
          selectedPrizes[i] = qualifyingItems.GetRandomElement();
          qualifyingItems.Remove(selectedPrizes[i]);
        }
      }

      return selectedPrizes;
    }

    public static ValueTuple<SkillObject, int> TournamentSkillXpGain(Hero winner)
    {
      float randomFloat = MBRandom.RandomFloat;
      SkillObject item = (randomFloat < 0.2f) ? DefaultSkills.OneHanded : ((randomFloat < 0.4f) ? DefaultSkills.TwoHanded : ((randomFloat < 0.6f) ? DefaultSkills.Polearm : ((randomFloat < 0.8f) ? DefaultSkills.Riding : DefaultSkills.Athletics)));
      int item2 = TournamentsEnhancedSettings.Instance.TournamentSkillXp;
      return new ValueTuple<SkillObject, int>(item, item2);
    }

    public static void SetDifficulty()
    {
      if (TournamentsEnhancedSettings.Instance.VeryHardTournaments && difficultyFlag == -1)
      {
        difficultyFlag = (short)CampaignOptions.CombatAIDifficulty;
        CampaignOptions.CombatAIDifficulty = CampaignOptions.Difficulty.Realistic;
      }
    }

    public static void UnsetDifficulty()
    {
      if (TournamentsEnhancedSettings.Instance.VeryHardTournaments && difficultyFlag > -1)
      {
        CampaignOptions.CombatAIDifficulty = (CampaignOptions.Difficulty)difficultyFlag;
        difficultyFlag = -1;
      }
    }

    private const int RELATIONSHIP_MODIFIER = 3;
    public static short difficultyFlag = -1;
  }
}
