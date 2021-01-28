using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    private void SetUpOneCandidateAndFallbackToQualify()
    {
      SetUpMockCandidate(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.FailQualified);
      SetUpMockFallbackComparers(MockComparerType.FailUnqualified);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_WithFallback_PassQualified_Result_ShouldNotFail()
    {
      SetUpOneCandidateAndFallbackToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_WithFallback_PassQualified_Result_ShouldSucceed()
    {
      SetUpOneCandidateAndFallbackToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_WithFallback_PassQualified_ResultNominee_ShouldBeExpected()
    {
      SetUpOneCandidateAndFallbackToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_candidates[0]);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_WithFallback_PassQualified_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpOneCandidateAndFallbackToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_WithFallback_PassQualified_ResultRunnerUp_ShouldBeNull()
    {
      SetUpOneCandidateAndFallbackToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_WithFallback_PassQualified_ResultAllQualifiedCandidates_CountShouldBeOne()
    {
      SetUpOneCandidateAndFallbackToQualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.ShouldHaveSingleItem();
    }
  }
}