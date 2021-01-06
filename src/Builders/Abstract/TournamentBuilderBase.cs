using System;
using System.Collections.Generic;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Finder.Comparers.Hero;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public abstract class TournamentBuilderBase
  {
    internal static bool ValidatePayorHero(MBHero payorHero)
    {
      return HeroFinder.Find(
        new FindHeroOptions()
        {
          Candidates = new MBHeroList(payorHero),
          Comparers = new IComparer<MBHero>[] { new BasicHostRequirementsHeroComparer() }
        }).Succeeded;
    }

    internal static bool ValidateFaction(IMBFaction faction)
    {
      bool result;

      if (faction.IsKingdomFaction)
      {
        result = KingdomFinder.Find(
          new FindKingdomOptions() { Candidates = new MBKingdomList((MBKingdom)faction) })
            .Succeeded;
      }
      else
      {
        result = ClanFinder.Find(
          new FindClanOptions() { Candidates = new MBClanList((MBClan)faction) })
          .Succeeded;
      }

      return result;
    }

    internal static CreateTournamentResult CreateTournament(CreateTournamentOptions options)
    {
      var settlement = options.Settlement;
      var type = options.Type;
      var townHadExistingTournament = settlement.Town.HasTournament;

      if (!townHadExistingTournament)
      {
        InstantiateTournamentFor(settlement);
        ApplyHostingEffects(type, settlement);
        ApplyRelationsGain(type, settlement);
      }

      PayTournamentFee(options.Payor, settlement);

      if (options.Payor.IsHero && options.Payor.Hero.IsHumanPlayerCharacter)
      {
        NotificationUtils.DisplayBannerMessage($"You've spent {Settings.Instance.TournamentCost.ToString()} gold on hosting a Tournament at {settlement.Name}");
      }

      return CreateTournamentResult.Success(settlement, townHadExistingTournament);
    }

    private static void InstantiateTournamentFor(MBSettlement settlement)
    {
      var tournament = new FightTournamentGame(settlement.Town);
      Campaign.Current.TournamentManager.AddTournament(tournament);
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
        NotificationUtils.DisplayMessage(settlement.Name + "'s prosperity, loyalty and security have increased and food stocks have decreased");
      }
    }

    private static void PayTournamentFee(Payor payor, MBSettlement settlement)
    {
      if (!payor.IsHero)
      {
        return;
      }

      var tournamentCost = Settings.Instance.TournamentCost;

      payor.Hero.ChangeHeroGold(-tournamentCost);
      payor.Settlement.Town.ChangeGold(tournamentCost);
    }

    public static void ApplyRelationsGain(TournamentType type, MBSettlement settlement)
    {
      if (type != TournamentType.Hosted)
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

        newRelation = notable.GetBaseHeroRelation(mainHero) + TournamentConstants.HostedTournamentEffects.NobleRelationshipModifier;

        notable.SetPersonalRelation(mainHero, newRelation);
      }

      NotificationUtils.DisplayBannerMessage("Your relationship with local notables at " + settlement.Name + " has improved");
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
