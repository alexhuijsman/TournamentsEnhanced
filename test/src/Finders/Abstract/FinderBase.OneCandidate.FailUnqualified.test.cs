using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    private void SetUpUnqualifiedCandidateToDisqualify()
    {
      SetUpMockCandidate(MockCandidateType.Unqualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_FailUnqualified_Result_ShouldFail()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_FailUnqualified_Result_ShouldNotSucceed()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_FailUnqualified_ResultNominee_ShouldBeNull()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_FailUnqualified_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_FailUnqualified_ResultRunnerUp_ShouldBeNull()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }
  }
}