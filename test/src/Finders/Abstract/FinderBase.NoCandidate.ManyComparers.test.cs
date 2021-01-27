using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    private void SetUpWithoutCandidates()
    {
      SetUpMockComparers(MockComparerType.FailUnqualified);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_Result_ShouldFail()
    {
      SetUpWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_Result_ShouldNotSucceed()
    {
      SetUpWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_ResultNominee_ShouldBeNull()
    {
      SetUpWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.IsNull.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_ResultRunnerUp_ShouldBeNull()
    {
      SetUpWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      SetUpWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }
  }
}