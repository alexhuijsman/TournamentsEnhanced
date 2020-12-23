using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using TaleWorlds.Core;

namespace TournamentsEnhanced
{
  public static class TournamentUtils
  {
    public static TournamentCreationResult TryCreateTournamentForFaction(IFaction faction)
    {
      var settlement = FindTournamentHostTownForFaction(faction);

      if (settlement != null)
      {
        TournamentUtils.CreateTournament(settlement, TournamentType.Peace);
        settlement.ApplyTournamentHostingEffects();
      }

      return new TournamentCreationResult(settlement);
    }

    public static void CreateTournament(Settlement settlement, TournamentType type)
    {
      TournamentGame tournament = new FightTournamentGame(settlement.Town);
      Campaign.Current.TournamentManager.AddTournament(tournament);
      TournamentKB tournamentKB = new TournamentKB(settlement, type);
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
    private static Settlement FindTournamentHostTownForFaction(IFaction faction)
    {
      var settlements = new List<Settlement>(faction.Settlements).Shuffle();

      foreach (var settlement in settlements)
      {
        if (!settlement.IsTown || settlement.Town.HasTournament)
        {
          continue;
        }

        return settlement;
      }

      return null;
    }

    public class TournamentCreationResult
    {
      public bool Succeeded => Town != null;
      public Town Town { get; private set; }

      public TournamentCreationResult(Settlement settlement = null)
      {
        Town = settlement?.Town;
      }
    }

  }
}
