using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ExistingTournamentComparer : HostSettlementComparerBase
  {
    public bool CanOverrideExisting { get; private set; }

    public ExistingTournamentComparer(MBHero initiatingHero, bool canOverrideExisting) : base(initiatingHero)
    {
      CanOverrideExisting = canOverrideExisting;
    }

    protected override bool MeetsRequirements(MBSettlement settlement)
    {
      return CanOverrideExisting ||
             !HasExistingTournament(settlement);
    }
  }
}
