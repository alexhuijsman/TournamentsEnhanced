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
  public class ProsperityComparerTest
    : ExistingTournamentComparerTestBase<ProsperityComparerImpl>
  {
    private const bool IsNotFactionLeader = false;
    private const bool IsFactionLeader = true;

    private Mock<MBHero> _mockInitiatingHero;
    private Mock<MBHero> _mockExistingInitiatingHero;
    private Mock<TournamentRecord> _mockTournamentRecord;
    private Mock<IMBFaction> _mockFaction;

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
      TournamentRecordType existingRecordType,
      bool initiatingHeroIsFactionLeader)
    {
      var hasExistingTournament = existingRecordType != TournamentRecordType.None;
      base.SetUp(meetsBaseRequirements, hasExistingTournament);

      _mockInitiatingHero = MockRepository.Create<MBHero>();
      _mockFaction = MockRepository.Create<IMBFaction>();
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

      _mockInitiatingHero.SetupGet(initiatingHero => initiatingHero.IsFactionLeader)
        .Returns(initiatingHeroIsFactionLeader);

      if (_mockExistingInitiatingHero != null)
      {
        _mockFaction.SetupGet(faction => faction.Leader)
          .Returns(initiatingHeroIsFactionLeader ? _mockInitiatingHero.Object : _mockExistingInitiatingHero.Object);

        _mockTournamentRecord.Setup(record => record.FindInitiatingHero())
          .Returns(_mockExistingInitiatingHero.Object);

        _mockExistingInitiatingHero.SetupGet(hero => hero.MapFaction)
          .Returns(_mockFaction.Object);
      }

      if (hasExistingTournament)
      {
        var existingHasInitiatingHero =
          existingRecordType != TournamentRecordType.HasNoInitiatingHero &&
          existingRecordType != TournamentRecordType.None;

        _mockTournamentRecord.SetupGet(record => record.HasInitiatingHero)
          .Returns(existingHasInitiatingHero);

        _mockTournamentRecords.SetupGet(
          tournamentRecords => tournamentRecords[_mockSettlement.Object])
            .Returns(_mockTournamentRecord.Object);
      }

      _sut.InitiatingHero = _mockInitiatingHero.Object;
      _sut.Settings = _mockSettings.Object;
      _sut.ModState = _mockModState.Object;
    }

    [Test]
    public void Instance_ShouldBeNull()
    {
      var x = ProsperityComparer.Instance;
      Assert.IsNull(ProsperityComparer.Instance);
    }

    [Test]
    public void InstanceIncludingExisting_ShouldBeNull()
    {
      Assert.IsNull(ProsperityComparer.InstanceIncludingExisting);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsNotFactionLeader_NoExistingTournament_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.None, IsNotFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsNotFactionLeader_ExistingTournament_WithNoInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.HasNoInitiatingHero, IsNotFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsNotFactionLeader_ExistingTournament_WithDifferentInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.HasDifferentInitiatingHero, IsNotFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsNotFactionLeader_ExistingTournament_WithSameInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.HasSameInitiatingHero, IsNotFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsNotFactionLeader_NoExistingTournament_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.None, IsNotFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsNotFactionLeader_ExistingTournament_WithNoInitiatingHero_ShouldReturnFalse()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.HasNoInitiatingHero, IsNotFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsNotFactionLeader_ExistingTournament_WithDifferentInitiatingHero_ShouldReturnFalse()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.HasDifferentInitiatingHero, IsNotFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsNotFactionLeader_ExistingTournament_WithSameInitiatingHero_ShouldReturnFalse()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.HasSameInitiatingHero, IsNotFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsFactionLeader_NoExistingTournament_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.None, IsFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsFactionLeader_ExistingTournament_WithNoInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.HasNoInitiatingHero, IsFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsFactionLeader_ExistingTournament_WithDifferentInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.HasDifferentInitiatingHero, IsFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_IsFactionLeader_ExistingTournament_WithSameInitiatingHero_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, TournamentRecordType.HasSameInitiatingHero, IsFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsFactionLeader_NoExistingTournament_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.None, IsFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsFactionLeader_ExistingTournament_WithNoInitiatingHero_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.HasNoInitiatingHero, IsFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsFactionLeader_ExistingTournament_WithDifferentInitiatingHero_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.HasDifferentInitiatingHero, IsFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_IsFactionLeader_ExistingTournament_WithSameInitiatingHero_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, TournamentRecordType.HasSameInitiatingHero, IsFactionLeader);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
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