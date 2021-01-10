using TaleWorlds.CampaignSystem;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced
{
  public static class IMBFactionExtensions
  {
    public static IFaction ToIFaction(this IMBFaction faction)
    {
      return (IFaction)faction;
    }
  }
}
