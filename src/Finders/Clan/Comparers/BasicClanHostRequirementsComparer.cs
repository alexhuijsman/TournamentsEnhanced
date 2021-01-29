using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Clan
{
  public class BasicClanHostRequirementsComparer : ClanComparerBase
  {
    public static BasicClanHostRequirementsComparer Instance { get; } = new BasicClanHostRequirementsComparer();

    protected BasicClanHostRequirementsComparer(MBHero initiatingHero = null) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBClan clan) =>
      clan.Settlements.FindIndex((settlement) => settlement.IsTown) != -1;
  }
}
