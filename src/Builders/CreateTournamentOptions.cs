using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public class CreateTournamentOptions
  {
    public readonly MBSettlement Settlement;
    public readonly TournamentType Type;
    public readonly Payor Payor;

    public CreateTournamentOptions(MBSettlement settlement, TournamentType type, MBHero payorHero)
      : this(settlement, type, new Payor(payorHero)) { }

    public CreateTournamentOptions(MBSettlement settlement, TournamentType type, MBSettlement payorSettlement)
      : this(settlement, type, new Payor(payorSettlement)) { }


    public CreateTournamentOptions(MBSettlement settlement, TournamentType type, Payor payor)
    {
      this.Settlement = settlement;
      this.Type = type;
      this.Payor = payor;
    }
  }
}