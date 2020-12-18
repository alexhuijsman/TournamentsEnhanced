using System;
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

    protected override void OnBeforeInitialModuleScreenSetAsRoot()
    {
      try
      {
        var harmony = new Harmony(ModuleName);
        harmony.PatchAll();
        harmony.PatchAll();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error Initialising TournamentsEnhanced:\n\n{ex}");
      }
    }

    protected override void OnSubModuleLoad()
    {

      InformationManager.DisplayMessage(new InformationMessage("Loaded " + ModuleName, Color.FromUint(4282569842U)));
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

    public static string ModuleName = "Tournaments Enhanced";
  }
}
