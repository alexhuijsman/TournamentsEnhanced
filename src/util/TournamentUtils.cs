using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TournamentsEnhanced
{
  public class TournamentUtils
  {
    public static CampaignOptions.Difficulty difficulty;

    private const int RELATIONSHIP_MODIFIER = 3;

    private static void CreateVanilTournamentInSettlement(Settlement settlement) { }
    private static void CreatePeaceTournamentInSettlement(Settlement settlement) { }
    private static void CreatePeaceTournamentInSettlement(Settlement settlement) { }
    private static void CreatePeaceTournamentInSettlement(Settlement settlement) { }
    private static void CreatePeaceTournamentInSettlement(Settlement settlement) { }
    private static void CreatePeaceTournamentInSettlement(Settlement settlement) { }
    private static void CreatePeaceTournamentInSettlement(Settlement settlement) { }
    private static void CreatePeaceTournamentInSettlement(Settlement settlement) { }
    private static void CreateTournament(Settlement settlement, TournamentType type)
    {
      TournamentGame tournament = new FightTournamentGame(settlement.Town);
      Campaign.Current.TournamentManager.AddTournament(tournament);
      TournamentKB tournamentKB = new TournamentKB(settlement, type);
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
          CreateTournament(settlement, TournamentType.Initial);
        }
      }
    }

    public static ItemObject GetTournamentPrize()
    {
      List<ItemObject>.Enumerator enumerator = ItemObject.All.GetEnumerator();
      List<ItemObject> tier = new List<ItemObject>();

      while (enumerator.MoveNext())
      {
        if (TournamentUtils.IsTierableItem(enumerator.Current) && enumerator.Current.Tier.Equals((ItemObject.ItemTiers)TournamentUtils.RewardTier()))
        {
          tier.Add(enumerator.Current);
        }
      }
      if (!tier.IsEmpty())
      {
        return tier.GetRandomElement();
      }
      else
      {
        return ItemObject.All.GetRandomElement();
      }
    }

    public static ValueTuple<SkillObject, int> TournamentSkillXpGain(Hero winner)
    {
      float randomFloat = MBRandom.RandomFloat;
      SkillObject item = (randomFloat < 0.2f) ? DefaultSkills.OneHanded : ((randomFloat < 0.4f) ? DefaultSkills.TwoHanded : ((randomFloat < 0.6f) ? DefaultSkills.Polearm : ((randomFloat < 0.8f) ? DefaultSkills.Riding : DefaultSkills.Athletics)));
      int item2 = TournamentsEnhancedSettings.Instance.TournamentSkillXp;
      return new ValueTuple<SkillObject, int>(item, item2);
    }

    public static ItemObject GetTournamentPrize()
    {
      List<ItemObject> tier = new List<ItemObject>();

      foreach (ItemObject itemObject in ItemObject.All)
      {

        if (itemObject.IsTierable() && itemObject.Tier.Equals((ItemObject.ItemTiers)BannerlordUtils.RewardTier()))
        {
          tier.Add(itemObject);
        }
      }
      if (!tier.IsEmpty())
      {
        return tier.GetRandomElement();
      }
      else
      {
        return ItemObject.All.GetRandomElement();
      }
    }

    public static ValueTuple<SkillObject, int> TournamentSkillXpGain(Hero winner)
    {
      float randomFloat = MBRandom.RandomFloat;
      SkillObject item = (randomFloat < 0.2f) ? DefaultSkills.OneHanded : ((randomFloat < 0.4f) ? DefaultSkills.TwoHanded : ((randomFloat < 0.6f) ? DefaultSkills.Polearm : ((randomFloat < 0.8f) ? DefaultSkills.Riding : DefaultSkills.Athletics)));
      int item2 = TournamentsEnhancedSettings.Instance.TournamentSkillXp;
      return new ValueTuple<SkillObject, int>(item, item2);
    }
  }
}
