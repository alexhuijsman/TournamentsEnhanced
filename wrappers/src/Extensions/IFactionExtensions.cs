using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Wrappers
{
  public static class IFactionExtensions
  {
    public static IMBFaction ToIMBFaction(this IFaction faction)
    {
      return (IMBFaction)faction;
    }
  }
}
