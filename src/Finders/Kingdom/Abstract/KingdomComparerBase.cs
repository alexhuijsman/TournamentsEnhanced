using TournamentsEnhanced.Finder.Comparers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class KingdomComparerBase : ComparerBase<MBKingdom>
  {
    protected KingdomComparerBase(MBHero initiatingHero = null) : base(initiatingHero) { }
  }
}