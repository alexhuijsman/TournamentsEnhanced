using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class MBTownExtensions
  {
    public static Hero FactionLeader(this Town town) => town.MapFaction.Leader;
  }
}
