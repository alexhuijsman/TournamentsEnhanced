using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder;
using TournamentsEnhanced.Wrappers.CampaignSystem;


namespace Test
{
  public class ClanFinderTest : TestBase<ClanFinderImpl>
  {
    [Test]
    public void Instance_ShouldBeSingleton()
    {
      ClanFinder.Instance.ShouldBe(ClanFinder.Instance);
    }
  }

  public class ClanFinderImpl : ClanFinder
  {
    public ClanFinderImpl() { }
  }
}