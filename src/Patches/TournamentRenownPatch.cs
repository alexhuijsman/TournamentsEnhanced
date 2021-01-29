using HarmonyLib;

using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace TournamentsEnhanced.Patches
{
  [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
  class TournamentRenownPatch
  {
    protected static Settings Settings { get; set; } = Settings.Instance;

    static void Postfix(ref int __result)
    {
      __result = Settings.RenownReward;
    }
  }
}
