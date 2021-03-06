﻿using System;

using HarmonyLib;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TournamentsEnhanced.Wrappers.Library;

namespace TournamentsEnhanced.Patches
{
  [HarmonyPatch(typeof(DefaultCombatXpModel), "GetXpFromHit")]
  class TournamentXpOnHitPatch
  {
    protected static Settings Settings { get; set; } = Settings.Instance;

    static void Postfix(CharacterObject attackerTroop, CharacterObject attackedTroop, int damage, bool isFatal, CombatXpModel.MissionTypeEnum missionType, out int xpAmount)
    {
      int num = attackedTroop.MaxHitPoints();
      float num2 = 0.4f * ((attackedTroop.GetPower() + 0.5f) * (float)(Math.Min(damage, num) + (isFatal ? num : 0)));
      float num3;
      if (missionType == CombatXpModel.MissionTypeEnum.NoXp)
      {
        num3 = 0f;
      }
      else if (missionType == CombatXpModel.MissionTypeEnum.PracticeFight)
      {
        num3 = Settings.ArenaHitXP;
      }
      else if (missionType == CombatXpModel.MissionTypeEnum.Tournament)
      {
        num3 = Settings.TournamentHitXP;
      }
      else if (missionType == CombatXpModel.MissionTypeEnum.SimulationBattle)
      {
        num3 = 0.9f;
      }
      else
      {
        num3 = ((missionType == CombatXpModel.MissionTypeEnum.Battle) ? 1f : 1f);
      }
      xpAmount = MBMathF.Round(num2 * num3);
    }
  }
}
