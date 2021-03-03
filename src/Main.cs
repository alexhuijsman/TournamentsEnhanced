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

        if (Assembly.GetExecutingAssembly().GetReferencedAssemblies()
            .FirstOrDefault(c => c.FullName == "TournamentFairArmour") == null)
        {
          Type campaignStarterType = campaignStarter.GetType();
          BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
          FieldInfo field = campaignStarterType.GetField("_campaignBehaviors", bindingFlags);
          var campaignBehaviors = (List<CampaignBehaviorBase>)field.GetValue(gameStarter);
          var overrideSpawnArmourMissionListenerType
            = Type.GetType("TournamentFairArmour.OverrideSpawnArmourMissionListener");
          _tournamentFairArmorSettingsCampaignBehavior
            = campaignBehaviors.FirstOrDefault(b => b.GetType().Equals(campaignStarterType));
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
      if (_tournamentFairArmorSettingsCampaignBehavior != null && mission.HasMissionBehaviour<TeamTournamentMissionController>())
      {
        AddOverrideSpawnArmourMissionListener(mission);
      }
    }

    private void AddOverrideSpawnArmourMissionListener(Mission mission)
    {
      var equipment = GetEquipmentByCultureOfCurrentSettlement();
      if (equipment != null)
      {
        IMissionListener overrideSpawnArmourMissionListener =
          (IMissionListener)Activator.CreateInstance(
            "TournamentFairArmour",
            "TournamentFairArmour.OverrideSpawnArmourMissionListener",
            new object[] { mission });

        mission.AddListener(overrideSpawnArmourMissionListener);
      }
    }

    private Equipment GetEquipmentByCultureOfCurrentSettlement()
    {
      var cultureStringId = Settlement.CurrentSettlement?.Culture?.StringId;
      var type = Type.GetType("TournamentFairArmour.OverrideSpawnArmourMissionListener");
      MethodInfo methodInfo = type.GetMethod("GetEquipmentOrDefaultIfDisabled");
      return (Equipment)methodInfo.Invoke(_tournamentFairArmorSettingsCampaignBehavior, new object[] { cultureStringId });
    }
  }
}
