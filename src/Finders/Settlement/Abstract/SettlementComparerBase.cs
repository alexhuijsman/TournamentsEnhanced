using TournamentsEnhanced.Finder.Comparers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class SettlementComparerBase : ComparerBase<MBSettlement>
  {
    public SettlementComparerBase(MBHero initiatingHero) : base(initiatingHero) { }
  }
}