using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Models.Serializable;
using TournamentsEnhanced.Wrappers.CampaignSystem;

namespace Test
{
  public class InitiatingHeroRankComparerTest
    : ExistingTournamentComparerTestBase<InitiatingHeroRankComparerImpl>
  {
    private Mock<MBHero> _mockInitiatingHero;
    private Mock<MBHero> _mockExistingInitiatingHero;
    private Mock<TournamentRecord> _mockTournamentRecord;

    protected enum TournamentRecordType
    {
      None,
      HasSameInitiatingHero,
      HasDifferentInitiatingHero,
      HasNoInitiatingHero
    }

    protected override void SetUp(bool meetsBaseRequirements, bool hasExistingTournament)
    {
      throw new InvalidOperationException();
    }

    protected void SetUp(
      bool meetsBaseRequirements,
      TournamentRecordType existingRecordType)
    {
      var hasExistingTournament = existingRecordType != TournamentRecordType.None;
      base.SetUp(meetsBaseRequirements, hasExistingTournament);

      _mockInitiatingHero = MockRepository.Create<MBHero>();
      _mockTournamentRecord = MockRepository.Create<TournamentRecord>();

      switch (existingRecordType)
      {
        case TournamentRecordType.HasDifferentInitiatingHero:
          _mockExistingInitiatingHero = MockRepository.Create<MBHero>();
          break;
        case TournamentRecordType.HasNoInitiatingHero:
        case TournamentRecordType.None:
          _mockExistingInitiatingHero = null;
          break;
        case TournamentRecordType.HasSameInitiatingHero:
          _mockExistingInitiatingHero = _mockInitiatingHero;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      _mockSettlement.SetupGet(settlement => settlement.OwnerClan.Leader)
        .Returns(_mockExistingInitiatingHero.Object);

      if (hasExistingTournament)
      {
        var existingHasInitiatingHero =
          existingRecordType != TournamentRecordType.HasNoInitiatingHero &&
          existingRecordType != TournamentRecordType.None;

        _mockTournamentRecord.SetupGet(record => record.HasInitiatingHero)
          .Returns(existingHasInitiatingHero);
      }

      _sut.InitiatingHero = _mockInitiatingHero.Object;
      _sut.Settings = _mockSettings.Object;
      _sut.ModState = _mockModState.Object;
    }

    [Test]
    public void Instance_ShouldBeNull()
    {
      var x = InitiatingHeroRankComparer.Instance;
      Assert.IsNull(InitiatingHeroRankComparer.Instance);
    }

    [Test]
    public void InstanceIncludingExisting_ShouldBeNull()
    {
      Assert.IsNull(InitiatingHeroRankComparer.InstanceIncludingExisting);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_NoExistingTournament_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.None);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_ExistingTournament_WithNoInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.HasNoInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_ExistingTournament_WithDifferentInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.HasDifferentInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_ExistingTournament_WithSameInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.HasSameInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_NoExistingTournament_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.None);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_ExistingTournament_WithNoInitiatingHero_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.HasNoInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_ExistingTournament_WithDifferentInitiatingHero_ShouldReturnFalse()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.HasDifferentInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_ExistingTournament_WithSameInitiatingHero_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.HasSameInitiatingHero);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

  }

  public class InitiatingHeroRankComparerImpl : InitiatingHeroRankComparer
  {
    public new Settings Settings { set => base.Settings = value; }
    public new ModState ModState { set => base.ModState = value; }
    public new bool CanOverrideExisting { set => base.CanOverrideExisting = value; }
    public new MBHero InitiatingHero { set => base.InitiatingHero = value; }
    public InitiatingHeroRankComparerImpl() : base(null) { }
    public InitiatingHeroRankComparerImpl(MBHero initiatingHero) : base(initiatingHero) { }

    public new bool MeetsRequirements(MBSettlement settlement) => base.MeetsRequirements(settlement);
  }
}