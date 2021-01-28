using Moq;
using NUnit.Framework;
using Shouldly;

using TournamentsEnhanced;

namespace Tests
{
  public partial class FinderBaseTests
  {
    private void SetUpOneCandidateAndFallbackToDisqualify()
    {
      SetUpMockCandidate(MockCandidateType.Unqualified);
      SetUpMockComparers(MockComparerType.FailQualified);
      SetUpMockFallbackComparers(MockComparerType.FailUnqualified);
    }

    [Test]
    public void Find_OneCandidate_FailQualified_WithFallback_Result_ShouldFail()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_OneCandidate_FailQualified_WithFallback_Result_ShouldNotSucceed()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_FailQualified_WithFallback_ResultNominee_ShouldBeNull()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_OneCandidate_FailQualified_WithFallback_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_FailQualified_WithFallback_ResultRunnerUp_ShouldBeNull()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_OneCandidate_FailQualified_WithFallback_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }
  }
}