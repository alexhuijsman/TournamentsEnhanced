using System;
using System.IO;
using System.Reflection;
using System.Windows;
using HarmonyLib;
using ModLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace TournamentsEnhanced
{
  public class Main : MBSubModuleBase
  {
    protected override void OnSubModuleLoad()
    {
      try
      {
        FileDatabase.Initialise(ModuleConstants.Name);
      }
      catch (Exception ex)
      {
        throw new UnauthorizedAccessException($"Exception while calling ModLib.FileDataBase.Initialise(\"${ModuleConstants.Name}\"", ex);
      }
    }

    protected override void OnBeforeInitialModuleScreenSetAsRoot()
    {
      InformationManager.DisplayMessage(new InformationMessage($"Loaded {ModuleConstants.Name} v{ModuleConstants.Version}", Color.FromUint(4282569842U)));

      try
      {
        var harmony = new Harmony(ModuleConstants.Name);
        harmony.PatchAll();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error Initialising Tournaments Enhanced:\n\n{ex}");
      }
    }

    protected override void OnGameStart(Game game, IGameStarter gameStarter)
    {
      if (game.GameType is Campaign)
      {
        CampaignGameStarter campaignStarter = (CampaignGameStarter)gameStarter;

        campaignStarter.AddBehavior(new BehaviorBase());
      }
    }

    public override void OnNewGameCreated(Game game, object obj)
    {
      if (game.GameType is Campaign)
      {
        BehaviorBase.WeeksSinceHostedTournament = 1;
        TournamentBuilder.CreateInitialTournaments();
      }
    }
  }
}
