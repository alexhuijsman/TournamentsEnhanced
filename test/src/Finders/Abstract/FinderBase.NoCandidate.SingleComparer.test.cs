using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    [Test]
    public void Find_NoCandidate_SingleComparer_Result_ShouldFail()
    {
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_Result_ShouldNotSucceed()
    {
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_ResultNominee_ShouldBeNull()
    {
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.IsNull.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_ResultRunnerUp_ShouldBeNull()
    {
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(MBWrapperBaseImpl.Null);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      SetUpSingleComparer();

      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }
  }
}