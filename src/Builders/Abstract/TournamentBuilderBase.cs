using System;

using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using TournamentsEnhanced.Wrappers.Core;

namespace TournamentsEnhanced.Builders.Abstract
{
  public abstract class TournamentBuilderBase
  {
    internal static FindHeroResult ValidatePayorHero(MBHero initiatingHero, MBHero payorHero)
    {
      return ValidatePayorHeroes(initiatingHero, payorHero);
    }

    internal static FindHeroResult ValidatePayorHeroes(params MBHero[] payorHeroes)
    {
      return HeroFinder.FindHostsThatMeetBasicRequirements(payorHeroes);
    }

    internal static ResultBase ValidateFaction(IMBFaction faction)
      => FactionFinder.FindFactionThatMeetBasicHostRequirements(faction);

    protected static CreateTournamentResult CreateTournament(CreateTournamentOptions options)
    {
      var settlement = options.Settlement;
      var type = options.Type;
      var townHadExistingTournament = settlement.Town.HasTournament;


      if (!townHadExistingTournament)
      {
        InstantiateTournament(settlement);
        ApplyHostingEffects(type, settlement);
        ApplyRelationsGain(type, settlement);
      }

      var result = CreateTournamentResult.Success(settlement, townHadExistingTournament);
      var payor = result.Payor;

      if (result.HasPayor)
      {
        PayTournamentFee(result.Payor, settlement);
      }

      if (payor.IsHumanPlayerCharacter)
      {
        MBInformationManagerFacade.DisplayAsQuickBanner($"You've spent {Settings.Instance.TournamentCost.ToString()} gold on hosting a Tournament at {settlement.Name}");
      }

      return result;
    }

    private static void InstantiateTournament(MBSettlement settlement)
    {
      var tournament = new FightTournamentGame(settlement.Town);
      MBCampaign.Current.TournamentManager.AddTournament(tournament);
    }


    private static void ApplyHostingEffects(TournamentType type, MBSettlement settlement)
    {
      if (type == TournamentType.Initial)
      {
        return;
      }

      settlement.Prosperity += Settings.Instance.ProsperityIncrease;
      settlement.Town.Loyalty += Settings.Instance.LoyaltyIncrease;
      settlement.Town.Security += Settings.Instance.SecurityIncrease;
      settlement.Town.FoodStocks -= Settings.Instance.FoodStocksDecrease;

      if (settlement.Town.MapFaction.Leader.IsHumanPlayerCharacter && Settings.Instance.SettlementStatNotification)
      {
        MBInformationManagerFacade.DisplayAsLogEntry($"{settlement.Name}'s prosperity, loyalty and security have increased and food stocks have decreased");
      }
    }

    private static void PayTournamentFee(MBHero payor, MBSettlement settlement)
    {
      var tournamentCost = Settings.Instance.TournamentCost;

      payor.ChangeHeroGold(-tournamentCost);
      settlement.Town.ChangeGold(tournamentCost);
    }

    public static void ApplyRelationsGain(TournamentType type, MBSettlement settlement)
    {
      if (type != TournamentType.PlayerInitiated)
      {
        return;
      }

      var mainHero = MBHero.MainHero;
      int newRelation;

      foreach (var notable in settlement.Notables)
      {
        if (notable == mainHero)
        {
          continue;
        }

        newRelation = notable.GetBaseHeroRelation(mainHero) + Settings.Instance.NoblesRelationIncrease;

        notable.SetPersonalRelation(mainHero, newRelation);
      }

      MBInformationManagerFacade.DisplayAsQuickBanner($"Your relationship with local notables at {settlement.Name} has improved");
    }

    public static ValueTuple<SkillObject, int> TournamentSkillXpGain(MBHero winner)
    {
      float randomFloat = MBRandom.DeterministicRandom.NextFloat();
      SkillObject item = (randomFloat < 0.2f) ? DefaultSkills.OneHanded : ((randomFloat < 0.4f) ? DefaultSkills.TwoHanded : ((randomFloat < 0.6f) ? DefaultSkills.Polearm : ((randomFloat < 0.8f) ? DefaultSkills.Riding : DefaultSkills.Athletics)));
      int item2 = Settings.Instance.TournamentSkillXp;
      return new ValueTuple<SkillObject, int>(item, item2);
    }
  }
}
