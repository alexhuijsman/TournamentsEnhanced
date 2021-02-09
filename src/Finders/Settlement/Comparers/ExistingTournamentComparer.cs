using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ExistingTournamentComparer : BasicSettlementHostRequirementsComparer
  {
    protected ModState ModState { get; set; } = ModState.Instance;

    public new static ExistingTournamentComparer Instance { get; } = new ExistingTournamentComparer(false);
    public static ExistingTournamentComparer InstanceIncludingExisting { get; } = new ExistingTournamentComparer(true);

    public bool CanOverrideExisting { get; protected set; }

    protected ExistingTournamentComparer(bool canOverrideExisting = false, MBHero initiatingHero = null) : base(initiatingHero)
    {
      CanOverrideExisting = canOverrideExisting;
    }

    protected override bool MeetsRequirements(MBSettlement settlement) =>
      base.MeetsRequirements(settlement) &&
      (CanOverrideExisting || !ModState.TournamentRecords.ContainsSettlement(settlement));
  }
}
