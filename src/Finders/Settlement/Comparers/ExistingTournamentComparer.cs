using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Settlement
{
  public class ExistingTournamentComparer : BasicHostRequirementsComparer
  {
    public new static ExistingTournamentComparer Instance { get; } = new ExistingTournamentComparer(false);
    public static ExistingTournamentComparer InstanceIncludingExisting { get; } = new ExistingTournamentComparer(true);

    public bool CanOverrideExisting { get; private set; }

    protected ExistingTournamentComparer(bool canOverrideExisting = false, MBHero initiatingHero = null) : base(initiatingHero)
    {
      CanOverrideExisting = canOverrideExisting;
    }

    protected override bool MeetsRequirements(MBSettlement settlement) =>
      base.MeetsRequirements(settlement) &&
      (CanOverrideExisting || !HasExistingTournament(settlement));
  }
}
