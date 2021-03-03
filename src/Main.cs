using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using HarmonyLib;
using SandBox;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TournamentsEnhanced.TeamTournament;

namespace TournamentsEnhanced
{
  public class Main : MBSubModuleBase
  {
    public static string ModuleName = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
    private static string ModuleVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

    protected object _tournamentFairArmorSettingsCampaignBehavior;
    protected Assembly _tournamentFairArmorAssembly;
    protected Type _tournamentFairArmorSettingsCampaignBehaviourType;
    protected Type _tournamentFairArmorOverrideSpawnArmourMissionListenerType;

    protected override void OnBeforeInitialModuleScreenSetAsRoot()
    {
      InformationManager.DisplayMessage(new InformationMessage($"Loaded {ModuleName} v{ModuleVersion}", Color.FromUint(4282569842U)));

      var harmony = new Harmony(ModuleName);
      harmony.PatchAll();
    }

    protected override void OnGameStart(Game game, IGameStarter gameStarter)
    {
      if (game.GameType is Campaign)
      {
        CampaignGameStarter campaignStarter = (CampaignGameStarter)gameStarter;
        campaignStarter.AddBehavior(new BehaviorBase());

        _tournamentFairArmorAssembly =
          AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(c => c.GetName().Name.Equals("TournamentFairArmour"));

        if (_tournamentFairArmorAssembly != null)
        {
          _tournamentFairArmorOverrideSpawnArmourMissionListenerType =
            _tournamentFairArmorAssembly.ExportedTypes.FirstOrDefault(
              t => t.Name.Equals("OverrideSpawnArmourMissionListener"));

          _tournamentFairArmorSettingsCampaignBehaviourType =
            _tournamentFairArmorAssembly.ExportedTypes.FirstOrDefault(
              t => t.Name.Equals("SettingsCampaignBehaviour"));

          _tournamentFairArmorSettingsCampaignBehavior =
              campaignStarter.CampaignBehaviors.FirstOrDefault(
                b => b.GetType().Equals(_tournamentFairArmorSettingsCampaignBehaviourType));
        }
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

    public override void OnMissionBehaviourInitialize(Mission mission)
    {
      if (_tournamentFairArmorSettingsCampaignBehavior != null &&
          mission.HasMissionBehaviour<TeamTournamentMissionController>())
      {
        TournamentFairArmor_AddOverrideSpawnArmourMissionListener(mission);
      }
    }

    private void TournamentFairArmor_AddOverrideSpawnArmourMissionListener(Mission mission)
    {
      var equipment = TournamentFairArmor_GetEquipmentByCultureOfCurrentSettlement();
      if (equipment != null)
      {
        IMissionListener overrideSpawnArmourMissionListener =
          (IMissionListener)Activator.CreateInstance(
            _tournamentFairArmorOverrideSpawnArmourMissionListenerType,
            new object[] { equipment });

        mission.AddListener(overrideSpawnArmourMissionListener);
      }
    }

    private Equipment TournamentFairArmor_GetEquipmentByCultureOfCurrentSettlement()
    {
      var cultureStringId = Settlement.CurrentSettlement?.Culture?.StringId;
      MethodInfo methodInfo = _tournamentFairArmorSettingsCampaignBehaviourType.GetMethod("GetEquipmentOrDefaultIfDisabled");
      return (Equipment)methodInfo.Invoke(_tournamentFairArmorSettingsCampaignBehavior, new object[] { cultureStringId });
    }
  }
}
