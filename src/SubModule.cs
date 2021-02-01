using System;
using ModLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TournamentsEnhanced.Behaviors;
using TournamentsEnhanced.Builders;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers;
using TournamentsEnhanced.Wrappers.Library;

namespace TournamentsEnhanced
{
  public class SubModule : MBSubModuleBase
  {
    protected TournamentBuilder TournamentBuilder { get; set; } = TournamentBuilder.Instance;
    protected ModState ModState { get; set; } = TournamentsEnhanced.Models.ModState.Instance;
    protected Harmony Harmony { get; set; } = new Harmony(Constants.Module.Name);

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
        Harmony.PatchAll();
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
