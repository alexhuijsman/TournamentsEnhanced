using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class IFactionExtensions
  {
    public static IMBFaction ToIMBFaction(this IFaction faction)
    {
      return (IMBFaction)faction;
    }
  }
}
