using System;
using System.Reflection;
using System.Windows;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace TournamentsEnhanced
{
  public class Main : MBSubModuleBase
  {
    public static string ModuleName = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
    private static string ModuleVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

    protected override void OnBeforeInitialModuleScreenSetAsRoot()
    {
      InformationManager.DisplayMessage(new InformationMessage($"Loaded {ModuleName} v{ModuleVersion}", Color.FromUint(4282569842U)));

      try
      {
        var harmony = new Harmony(ModuleName);
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
        BehaviorBase.weeksSinceHost = 1;
        Utilities.CreateInitialTournaments();
      }
    }
  }
}

