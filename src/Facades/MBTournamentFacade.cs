using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  public static class MBTournamentFacade
  {
    public static TournamentCreationResult TryMakePeaceTournamentForFaction(IFaction faction)
    {
      var findHostTownResult = MBSettlementFacade.FindHostTownForFaction(faction, FindTownOptions.UseExistingTournamentAsLastResort);

      if (findHostTownResult.Failed)
      {
        return TournamentCreationResult.Failure();
      }

      return PreparePeaceTournamentForTown(findHostTownResult.Town);
    }

    private static TournamentCreationResult PreparePeaceTournamentForTown(Town town)
    {
      PrepareTournamentForTown(town, new PrepareTournamentOptions() { type = TournamentType.Peace });
      var townHasExistingTournament = town.HasTournament;

      if (!townHasExistingTournament)
      {
        InstantiateTournamentForTown(town);
      }
      else
      {
        TournamentRecords.AddOrReplaceForTown(new TournamentRecord() { type = TournamentType.Peace }, town);

        town.ApplyTournamentCreationEffects();
      }

      return TournamentCreationResult.Success(town, townHasExistingTournament);
    }

    private static void PrepareTournamentForTown(Town town, PrepareTournamentOptions options)
    {
      throw new NotImplementedException();
    }

    public static TournamentCreationResult PrepareLordTournamentInSettlements(IList<Settlement> settlements)
    {
      var result = MBSettlementFacade.FindHostTownFromSettlements(settlements, FindTownOptions.RejectExistingTournaments);

      if (result.Status == ResultStatus.Success)
      {
        return CreateLordTournamentFromTownResult(result);
      }
      else
      {
        return TournamentCreationResult.Failure();
      }
    }

    private static TournamentCreationResult CreateLordTournamentFromTownResult(FindTownResult result)
    {
      var town = result.Town;
      var kingdom = town.OwnerClan.Kingdom;

      if (!town.HasTournament)
      {
        CreateTournament(town, TournamentType.Lord);
      }

      if (Hero.MainHero.Clan.Kingdom != null && Hero.MainHero.Clan.Kingdom.Name.Equals(kingdom.Name))
      {
        NotificationUtils.DisplayBannerMessage($"{kingdom.Leader.Name} invites you to a Highborn tournament at {town.Name}");
      }

      return TournamentCreationResult.Success(town.Town);
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
        InstantiateTournamentForTown(settlement, TournamentType.Prosperity);
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
      TournamentRecords.CreateTournament(settlement, TournamentType.Hosted);

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

    private static void InstantiateTournamentForTown(Town town)
    {
      var tournament = new FightTournamentGame(town);
      Campaign.Current.TournamentManager.AddTournament(tournament);
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
