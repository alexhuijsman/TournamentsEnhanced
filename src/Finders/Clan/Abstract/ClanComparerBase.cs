using TournamentsEnhanced.Finder.Comparers.Abstract;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder.Comparers.Clan
{
  public abstract class ClanComparerBase : ComparerBase<MBClan>
  {
    public ClanComparerBase(MBHero initiatingHero) : base(initiatingHero) { }
  }
}