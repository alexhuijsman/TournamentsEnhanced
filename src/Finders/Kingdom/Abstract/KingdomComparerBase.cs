using TournamentsEnhanced.Finder.Comparers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers
{
  public abstract class KingdomComparerBase : ComparerBase<MBKingdom>
  {
    public KingdomComparerBase(MBHero initiatingHero) : base(initiatingHero) { }
  }
}