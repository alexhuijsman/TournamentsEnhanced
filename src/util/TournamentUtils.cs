using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  public static class TournamentUtils
  {
    public static TournamentCreationResult CreatePeaceTournamentInTownBelongingToFaction(IFaction faction)
    {
      var result = SettlementUtils.FindNewOrExistingTournamentHostTownForFaction(faction);

      if (result.Succeeded)
      {
        return CreatePeaceTournamentFromFindSettlementResult(result);
      }
      else
      {
        return TournamentCreationResult.Failure;
      }
    }
    private static TournamentCreationResult CreatePeaceTournamentFromFindSettlementResult(SettlementUtils.FindSettlementResult result)
    {
      var settlement = result.Settlement;

      if (!result.HadExistingTournament)
      {
        TournamentUtils.CreateTournament(settlement, TournamentType.Peace);
      }

      return TournamentCreationResult.Success(settlement.Town);
    }

    public static TournamentCreationResult CreateLordTournamentInSettlements(IList<Settlement> settlements)
    {
      var result = SettlementUtils.FindNewTournamentHostTownFromSettlements(settlements);

      if (result.Succeeded)
      {
        return CreateLordTournamentFromFindSettlementResult(result);
      }
      else
      {
        return TournamentCreationResult.Failure;
      }
    }

    private static TournamentCreationResult CreateLordTournamentFromFindSettlementResult(SettlementUtils.FindSettlementResult result)
    {
      var settlement = result.Settlement;
      var kingdom = settlement.OwnerClan.Kingdom;

      if (!result.HadExistingTournament)
      {
        TournamentUtils.CreateTournament(settlement, TournamentType.Lord);
      }

      if (Hero.MainHero.Clan.Kingdom != null && Hero.MainHero.Clan.Kingdom.Name.Equals(kingdom.Name))
      {
        NotificationUtils.DisplayBannerMessage($"{kingdom.Leader.Name} invites you to a Highborn tournament at {settlement.Name}");
      }

      return TournamentCreationResult.Success(settlement.Town);
    }

    public static TournamentCreationResult CreateProsperityTournamentInSettlements(IList<Settlement> settlements)
    {
      var result = SettlementUtils.FindNewTournamentHostTownFromSettlements(settlements);

      if (result.Succeeded)
      {
        return CreateProsperityTournamentFromFindSettlementResult(result);
      }
      else
      {
        return TournamentCreationResult.Failure;
      }
    }

    private static TournamentCreationResult CreateProsperityTournamentFromFindSettlementResult(SettlementUtils.FindSettlementResult result)
    {
      var settlement = result.Settlement;
      var kingdom = settlement.OwnerClan.Kingdom;

      if (!result.HadExistingTournament)
      {
        TournamentUtils.CreateTournament(settlement, TournamentType.Prosperity);
      }
      return TournamentCreationResult.Success(settlement.Town);
    }

    public static TournamentCreationResult CreateInvitationTournamentFromSettlements(IList<Settlement> settlements)
    {
      var result = SettlementUtils.FindNewOrExistingTournamentHostTownFromSettlements(settlements);

      if (!result.Succeeded)
      {
        return CreateInvitationTournamentFromFindSettlementResult(result);
      }
      else
      {
        return TournamentCreationResult.Failure;
      }
    }

    private static TournamentCreationResult CreateInvitationTournamentFromFindSettlementResult(SettlementUtils.FindSettlementResult result)
    {
      var settlement = result.Settlement;

      if (!result.HadExistingTournament)
      {
        TournamentUtils.CreateTournament(settlement, TournamentType.Invitation);
      }

      return TournamentCreationResult.Success(settlement.Town);
    }

    public static TournamentCreationResult CreateHostedTournamentAtSettlement(Settlement settlement)
    {
      TournamentUtils.CreateTournament(settlement, TournamentType.Hosted);

      Hero.MainHero.ChangeHeroGold(-Settings.Instance.TournamentCost);
      NotificationUtils.DisplayBannerMessage($"You've spent {Settings.Instance.TournamentCost.ToString()} gold on hosting a Tournament at {settlement.Town.Name}");

      return TournamentCreationResult.Success(settlement.Town);
    }

    private static void CreateTournament(Settlement settlement, TournamentType type)
    {
      TournamentGame tournament = new FightTournamentGame(settlement.Town);
      Campaign.Current.TournamentManager.AddTournament(tournament);
      TournamentKB tournamentKB = new TournamentKB(settlement, type);

      settlement.ApplyTournamentCreationEffects();

      if (type == TournamentType.Hosted)
      {
        settlement.ApplyHostedTournamentRelationsGain();
      }
    }

    public static void CreateInitialTournaments()
    {
      int max = Settings.Instance.TournamentInitialSpawnCount;
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

    public static ValueTuple<SkillObject, int> TournamentSkillXpGain(Hero winner)
    {
      float randomFloat = MBRandom.DeterministicRandom.NextFloat();
      SkillObject item = (randomFloat < 0.2f) ? DefaultSkills.OneHanded : ((randomFloat < 0.4f) ? DefaultSkills.TwoHanded : ((randomFloat < 0.6f) ? DefaultSkills.Polearm : ((randomFloat < 0.8f) ? DefaultSkills.Riding : DefaultSkills.Athletics)));
      int item2 = Settings.Instance.TournamentSkillXp;
      return new ValueTuple<SkillObject, int>(item, item2);
    }

    public class TournamentCreationResult
    {
      public readonly static TournamentCreationResult Failure = new TournamentCreationResult();
      public static TournamentCreationResult Success(Town town)
      {
        return new TournamentCreationResult(town);
      }

      public bool Succeeded => Town != null;
      public Town Town { get; private set; }

      private TournamentCreationResult(Town town)
      {
        Town = town;
      }
      private TournamentCreationResult() { }
    }
  }
}
