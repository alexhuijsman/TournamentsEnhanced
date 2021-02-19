using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace TournamentsEnhanced
{
  [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
  class TournamentRenownPatch
  {
    static void Postfix(ref int __result)
    {
      __result = TournamentsEnhancedSettings.Instance.RenownReward;
      if (winner.GetPerkValue(DefaultPerks.OneHanded.Duelist))
      {
        // needs to add it, since SecondaryBonus = 1 (for now at least)
        __result += (int)Math.Round(__result * DefaultPerks.OneHanded.Duelist.SecondaryBonus);
      }
    }
  }
}
