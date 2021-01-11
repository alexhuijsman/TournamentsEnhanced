using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Clan
{
  public class BasicHostRequirementsComparer : ClanComparerBase
  {
    public static BasicHostRequirementsComparer Instance { get; } = new BasicHostRequirementsComparer();

    protected BasicHostRequirementsComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBClan clan) =>
      !clan.Settlements.IsEmpty() &&
      clan.Settlements.FindIndex((settlement) => settlement.IsTown) != -1;
  }
}
