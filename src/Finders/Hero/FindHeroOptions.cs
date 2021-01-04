using System.Collections.Generic;

using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace TournamentsEnhanced.Finder
{
  public struct FindHeroOptions : IFindOptionsBase<MBHero, MBHeroList>
  {
    public MBHeroList Candidates { get; set; }
    public IComparer<MBHero>[] Comparers { get; set; }
  }
}
