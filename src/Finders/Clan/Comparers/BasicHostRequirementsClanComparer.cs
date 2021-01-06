using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Hero
{
  public class BasicHostRequirementsClanComparer : ClanComparerBase
  {
    internal override bool MeetsRequirements(MBClan clan) =>
      !clan.Settlements.IsEmpty &&
      clan.Settlements.FindIndex((settlement) => settlement.IsTown) != -1;
  }
}
