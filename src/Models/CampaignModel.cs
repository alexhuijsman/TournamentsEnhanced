using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced.Models
{
  public class CampaignModel
  {
    public static CampaignModel Instance { get; } = new CampaignModel();
    public CampaignOptions.Difficulty NonTournamentDifficulty { get; set; }
  }
}
