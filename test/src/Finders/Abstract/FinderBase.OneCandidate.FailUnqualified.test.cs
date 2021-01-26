using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    private void SetUpUnqualifiedCandidateToDisqualify()
    {
      SetUpMockCandidates(MockCandidateType.Unqualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_FailUnqualified_Result_ShouldNotFail()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_FailUnqualified_Result_ShouldSucceed()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_OneCandidate_OneComparer_FailUnqualified_ResultNominee_ShouldBeExpected()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_mockCandidates[0].Object);
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
    public void Find_OneCandidate_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldBeSingle()
    {
      SetUpUnqualifiedCandidateToDisqualify();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(1);
    }
  }
}