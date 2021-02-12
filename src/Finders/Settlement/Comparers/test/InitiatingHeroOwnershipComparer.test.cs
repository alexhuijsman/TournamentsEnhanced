using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace Test
{
  public class InitiatingHeroOwnershipComparerTest
    : ExistingTournamentComparerTestBase<InitiatingHeroOwnershipComparerImpl>
  {
    private const bool SettlementOwnerIsInitiatingHero = true;
    private const bool SettlementOwnerIsNotInitiatingHero = false;
    private Mock<MBHero> _mockInitiatingHero;
    private Mock<MBHero> _mockSettlementOwner;

    protected override void SetUp(bool meetsBaseRequirements, bool hasExistingTournament)
    {
      throw new InvalidOperationException();
    }

    protected void SetUp(
      bool meetsBaseRequirements,
      bool hasExistingTournament,
      bool settlementOwnerIsInitiatingHero)
    {
      base.SetUp(meetsBaseRequirements, hasExistingTournament);

      _mockInitiatingHero = MockRepository.Create<MBHero>();

      _mockSettlementOwner = settlementOwnerIsInitiatingHero ?
        _mockInitiatingHero :
        MockRepository.Create<MBHero>();

      _mockSettlement.SetupGet(settlement => settlement.OwnerClan.Leader)
        .Returns(_mockSettlementOwner.Object);

      _sut.InitiatingHero = _mockInitiatingHero.Object;
      _sut.Settings = _mockSettings.Object;
      _sut.ModState = _mockModState.Object;
    }

    [Test]
    public void Instance_ShouldBeNull()
    {
      var x = InitiatingHeroOwnershipComparer.Instance;
      Assert.IsNull(InitiatingHeroOwnershipComparer.Instance);
    }

    [Test]
    public void InstanceIncludingExisting_ShouldBeNull()
    {
      Assert.IsNull(InitiatingHeroOwnershipComparer.InstanceIncludingExisting);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_NoExistingTournament_SettlementOwnerIsNotInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, NoExistingTournament, SettlementOwnerIsNotInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }
    [Test]
    public void MeetsRequirements_FailsBaseRequirements_NoExistingTournament_SettlementOwnerIsInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, NoExistingTournament, SettlementOwnerIsInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }
    [Test]
    public void MeetsRequirements_FailsBaseRequirements_HasExistingTournament_SettlementOwnerIsNotInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, HasExistingTournament, SettlementOwnerIsNotInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }
    [Test]
    public void MeetsRequirements_FailsBaseRequirements_HasExistingTournament_SettlementOwnerIsInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, HasExistingTournament, SettlementOwnerIsInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }
    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_NoExistingTournament_SettlementOwnerIsNotInitiatingHero_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, NoExistingTournament, SettlementOwnerIsNotInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }
    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_NoExistingTournament_SettlementOwnerIsInitiatingHero_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, NoExistingTournament, SettlementOwnerIsInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }
    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_HasExistingTournament_SettlementOwnerIsNotInitiatingHero_ShouldReturnFalse()
    {
      SetUp(MeetsBaseRequirements, HasExistingTournament, SettlementOwnerIsNotInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }
    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_HasExistingTournament_SettlementOwnerIsInitiatingHero_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, HasExistingTournament, SettlementOwnerIsInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

  }

  public class InitiatingHeroOwnershipComparerImpl : InitiatingHeroOwnershipComparer
  {
    public new Settings Settings { set => base.Settings = value; }
    public new ModState ModState { set => base.ModState = value; }
    public new bool CanOverrideExisting { set => base.CanOverrideExisting = value; }
    public new MBHero InitiatingHero { set => base.InitiatingHero = value; }
    public InitiatingHeroOwnershipComparerImpl() : base(null) { }
    public InitiatingHeroOwnershipComparerImpl(MBHero initiatingHero) : base(initiatingHero) { }

    public new bool MeetsRequirements(MBSettlement settlement) => base.MeetsRequirements(settlement);
  }
}