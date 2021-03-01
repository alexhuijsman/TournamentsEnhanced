using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Builders
{
  public class CreateTournamentOptions
  {
    public virtual MBHero InitiatingHero { get; set; } = MBHero.Null;
    public virtual MBSettlement Settlement { get; set; } = MBSettlement.Null;
    public virtual TournamentType Type { get; set; } = TournamentType.None;

    public virtual bool AreValid => !Settlement.IsNull && Settlement.IsTown && Type != TournamentType.None;
  }
}