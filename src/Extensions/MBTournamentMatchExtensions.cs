using System.Linq;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;

namespace TournamentsEnhanced
{
  static class MBTournamentMatchExtensions
  {
    public static Town GetTown(this TournamentMatch tournamentMatch)
    {
      return tournamentMatch.Winners.First().Character.HeroObject.CurrentSettlement.Town;
    }
  }
}
