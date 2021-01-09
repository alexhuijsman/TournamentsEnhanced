using System;
using System.Windows;

using HarmonyLib;

using ModLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

using TournamentsEnhanced.Behaviors;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models.ModState;

namespace TournamentsEnhanced
{
  public class Main : MBSubModuleBase
  {
    protected override void OnSubModuleLoad()
    {
      try
      {
        FileDatabase.Initialise(Constants.Module.Name);
      }
      catch (Exception ex)
      {
        throw new UnauthorizedAccessException($"Exception while calling ModLib.FileDataBase.Initialise(\"${Constants.Module.Name}\"", ex);
      }
    }

    protected override void OnBeforeInitialModuleScreenSetAsRoot()
    {
      InformationManager.DisplayMessage(new InformationMessage($"Loaded {Constants.Module.Name} v{Constants.Module.Version}", Color.FromUint(4282569842U)));

      try
      {
        var harmony = new Harmony(Constants.Module.Name);
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
        var campaignStarter = (CampaignGameStarter)gameStarter;

        campaignStarter.AddBehavior(new LotteryBehavior());
        campaignStarter.AddBehavior(new ModStateBehavior());
        campaignStarter.AddBehavior(new TownMenuBehavior());
        campaignStarter.AddBehavior(new SyncDataBehavior());
        campaignStarter.AddBehavior(new TournamentSpawnBehavior());
      }
    }

    public override void OnNewGameCreated(Game game, object obj)
    {
      if (game.GameType is Campaign)
      {
        ModState.Reset();

        TournamentBuilder.CreateInitialTournaments();
      }
    }
  }
}
