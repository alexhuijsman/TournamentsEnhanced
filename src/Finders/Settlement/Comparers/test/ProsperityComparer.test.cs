using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace Test
{
  public partial class ProsperityComparerTest
    : ExistingTournamentComparerTestBase<ProsperityComparerImpl>
  {
    private const bool RequiresMinProsperity = true;
    private const bool DoesNotRequireMinProsperity = false;

    protected override void SetUp(bool meetsBaseRequirements, bool requiresMinProsperity)
    {
      base.SetUp(meetsBaseRequirements, false);

      _sut.Settings = _mockSettings.Object;
      _sut.ModState = _mockModState.Object;
    }

    [Test]
    public void Instance_ShouldBeSingleton()
    {
      ProsperityComparer.Instance.ShouldBe(ProsperityComparer.Instance);
    }

    [Test]
    public void InstanceIncludingExisting_ShouldBeSingleton()
    {
      ProsperityComparer.InstanceIncludingExisting.ShouldBe(ProsperityComparer.InstanceIncludingExisting);
    }

    [Test]
    public void InstanceMinProsperityRequirement_ShouldBeSingleton()
    {
      ProsperityComparer.InstanceMinProsperityRequirement.ShouldBe(ProsperityComparer.InstanceMinProsperityRequirement);
    }

    [Test]
    public void InstanceMinProsperityRequirementIncludingExisting_ShouldBeSingleton()
    {
      ProsperityComparer.InstanceMinProsperityRequirementIncludingExisting
        .ShouldBe(ProsperityComparer.InstanceMinProsperityRequirementIncludingExisting);
    }
  }

  public class ProsperityComparerImpl : ProsperityComparer
  {
    public new Settings Settings { set => base.Settings = value; }
    public new ModState ModState { set => base.ModState = value; }
    public new bool CanOverrideExisting { set => base.CanOverrideExisting = value; }
    public new MBHero InitiatingHero { set => base.InitiatingHero = value; }

    public ProsperityComparerImpl() : base() { }
    public ProsperityComparerImpl(bool hasProsperityRequirement, bool canOverrideExisting, MBHero initiatingHero)
     : base(hasProsperityRequirement, canOverrideExisting, initiatingHero) { }

    public new bool MeetsRequirements(MBSettlement settlement) => base.MeetsRequirements(settlement);
  }
}