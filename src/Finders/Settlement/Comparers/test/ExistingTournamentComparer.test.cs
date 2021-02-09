using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using TournamentsEnhanced;
using TournamentsEnhanced.Finder.Comparers.Settlement;
using TournamentsEnhanced.Models;
using TournamentsEnhanced.Wrappers.CampaignSystem;
using static TournamentsEnhanced.Constants.Settings;

namespace Test
{
  public class ExistingTournamentComparerTest
    : ExistingTournamentComparerTestBase<ExistingTournamentComparerImpl>
  {

    protected override void SetUp(bool meetsBaseRequirements, bool hasExistingTournament)
    {
      throw new InvalidOperationException();
    }

    protected void SetUp(
      bool meetsBaseRequirements,
      bool canOverrideExisting,
      bool hasExistingTournament)
    {
      base.SetUp(meetsBaseRequirements, hasExistingTournament);

      _sut.CanOverrideExisting = canOverrideExisting;
      _sut.Settings = _mockSettings.Object;
      _sut.ModState = _mockModState.Object;
    }

    [Test]
    public void Instance_ShouldBeSingleton()
    {
      ExistingTournamentComparer.Instance.ShouldBe(ExistingTournamentComparer.Instance);
    }

    [Test]
    public void Instance_CanOverrideExisting_ShouldBeFalse()
    {
      ExistingTournamentComparer.Instance.CanOverrideExisting.ShouldBe(false);
    }

    [Test]
    public void InstanceIncludingExisting_ShouldBeSingleton()
    {
      ExistingTournamentComparer.InstanceIncludingExisting.ShouldBe(ExistingTournamentComparer.InstanceIncludingExisting);
    }

    [Test]
    public void InstanceIncludingExisting_CanOverrideExisting_ShouldBeTrue()
    {
      ExistingTournamentComparer.InstanceIncludingExisting.CanOverrideExisting.ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_CannotOverrideExisting_NoExistingTournament_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, CannotOverrideExisting, NoExistingTournament);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_CannotOverrideExisting_HasExistingTournament_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, CannotOverrideExisting, HasExistingTournament);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_CanOverrideExisting_NoExistingTournament_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, CanOverrideExisting, NoExistingTournament);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_FailsBaseRequirements_CanOverrideExisting_HasExistingTournament_ShouldReturnFalse()
    {
      SetUp(FailsBaseRequirements, CanOverrideExisting, HasExistingTournament);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_CannotOverrideExisting_NoExistingTournament_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, CannotOverrideExisting, NoExistingTournament);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_CannotOverrideExisting_HasExistingTournament_ShouldReturnFalse()
    {
      SetUp(MeetsBaseRequirements, CannotOverrideExisting, HasExistingTournament);

      _sut.MeetsRequirements(_settlement).ShouldBe(false);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_CanOverrideExisting_NoExistingTournament_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, CanOverrideExisting, NoExistingTournament);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }

    [Test]
    public void MeetsRequirements_MeetsBaseRequirements_CanOverrideExisting_HasExistingTournament_ShouldReturnTrue()
    {
      SetUp(MeetsBaseRequirements, CanOverrideExisting, HasExistingTournament);

      _sut.MeetsRequirements(_settlement).ShouldBe(true);
    }
  }

  public class ExistingTournamentComparerImpl : ExistingTournamentComparer
  {
    public new Settings Settings { set => base.Settings = value; }
    public new ModState ModState { set => base.ModState = value; }
    public new bool CanOverrideExisting { set => base.CanOverrideExisting = value; }
    public new bool MeetsRequirements(MBSettlement Settlement) => base.MeetsRequirements(Settlement);
  }
}