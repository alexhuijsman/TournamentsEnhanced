using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  public static class TournamentManager
  {

    public static TournamentCreationResult CreateLordTournamentInSettlements(IList<Settlement> settlements)
    {
      var result = MBSettlementFacade.FindHostTownFromSettlements(settlements);

      if (result.Status)
      {
        return CreateLordTournamentFromFindSettlementResult(result);
      }
      else
      {
        return TournamentCreationResult.Failure;
      }
    }

    private static TournamentCreationResult CreateLordTournamentFromFindSettlementResult(MBSettlementFacade.FindSettlementResult result)
    {
      var settlement = result.Settlement;
      var kingdom = settlement.OwnerClan.Kingdom;

      if (!result.HadExistingTournament)
      {
        TournamentManager.CreateTournament(settlement, TournamentType.Lord);
      }

      if (Hero.MainHero.Clan.Kingdom != null && Hero.MainHero.Clan.Kingdom.Name.Equals(kingdom.Name))
      {
        NotificationUtils.DisplayBannerMessage($"{kingdom.Leader.Name} invites you to a Highborn tournament at {settlement.Name}");
      }

      return TournamentCreationResult.Success(settlement.Town);
    }

    public static TournamentCreationResult CreateProsperityTournamentInSettlements(IList<Settlement> settlements)
    {
      var result = MBSettlementFacade.FindHostTownFromSettlements(settlements);

      if (result.Status)
      {
        return CreateProsperityTournamentFromFindSettlementResult(result);
      }
      else
      {
        return TournamentCreationResult.Failure;
      }
    }

    private static TournamentCreationResult CreateProsperityTournamentFromFindSettlementResult(MBSettlementFacade.FindSettlementResult result)
    {
      var settlement = result.Settlement;
      var kingdom = settlement.OwnerClan.Kingdom;

      if (!result.HadExistingTournament)
      {
        TournamentManager.CreateTournament(settlement, TournamentType.Prosperity);
      }
      return TournamentCreationResult.Success(settlement.Town);
    }

    public static TournamentCreationResult CreateInvitationTournamentFromSettlements(IList<Settlement> settlements)
    {
      var result = MBSettlementFacade.FindHostTownFromSettlements(settlements);

      if (!result.Status)
      {
        return CreateInvitationTournamentFromFindSettlementResult(result);
      }
      else
      {
        return TournamentCreationResult.Failure;
      }
    }

    private static TournamentCreationResult CreateInvitationTournamentFromFindSettlementResult(MBSettlementFacade.FindSettlementResult result)
    {
      var settlement = result.Settlement;

      if (!result.HadExistingTournament)
      {
        TournamentManager.CreateTournament(settlement, TournamentType.Invitation);
      }

      return TournamentCreationResult.Success(settlement.Town);
    }

    public static TournamentCreationResult CreateHostedTournamentAtSettlement(Settlement settlement)
    {
      TournamentManager.CreateTournament(settlement, TournamentType.Hosted);

      Hero.MainHero.ChangeHeroGold(-Settings.Instance.TournamentCost);
      NotificationUtils.DisplayBannerMessage($"You've spent {Settings.Instance.TournamentCost.ToString()} gold on hosting a Tournament at {settlement.Town.Name}");

      return TournamentCreationResult.Success(settlement.Town);
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

  }
}
