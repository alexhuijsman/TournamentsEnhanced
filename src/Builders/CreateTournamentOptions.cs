using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public class CreateTournamentOptions
  {
    public MBHero InitiatingHero { get; set; }
    public MBSettlement Settlement { get; set; }
    public TournamentType Type { get; set; }
  }
}