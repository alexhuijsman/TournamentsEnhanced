using System;
using System.Windows;
using HarmonyLib;
using ModLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TournamentsEnhanced.Behaviors;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models.ModState;
using TournamentsEnhanced.Wrappers.Library;

namespace TournamentsEnhanced
{
  public class SubModule : MBSubModuleBase
  {
    public TournamentBuilder TournamentBuilder { protected get; set; } = TournamentBuilder.Instance;
    public ModState ModState { protected get; set; } = ModState.Instance;

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
      var hadHarmonyException = false;

      try
      {
        var harmony = new Harmony(Constants.Module.Name);
        harmony.PatchAll();
      }
      catch (Exception)
      {
        hadHarmonyException = true;
      }

      InformationManager.DisplayMessage(new InformationMessage($"{(hadHarmonyException ? "Error Initialising" : "Loaded")} {Constants.Module.Name} v{Constants.Module.Version}", MBColor.FromUint(4282569842U)));
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
        campaignStarter.AddBehavior(new TournamentCreationBehavior());
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
