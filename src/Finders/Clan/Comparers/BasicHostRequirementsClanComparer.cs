using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Clan
{
  public class BasicHostRequirementsClanComparer : ClanComparerBase
  {
    public BasicHostRequirementsClanComparer(MBHero initiatingHero) : base(initiatingHero) { }

    protected override bool MeetsRequirements(MBClan clan) =>
      !clan.Settlements.IsEmpty &&
      clan.Settlements.FindIndex((settlement) => settlement.IsTown) != -1;
  }
}
