using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public class CreateTournamentOptions
  {
    public readonly MBSettlement Settlement;
    public readonly TournamentType Type;
    public readonly Payor Payor;

    public CreateTournamentOptions(MBSettlement settlement, TournamentType type, Payor payor)
    {
      this.Settlement = settlement;
      this.Type = type;
      this.Payor = payor;
    }
  }
}