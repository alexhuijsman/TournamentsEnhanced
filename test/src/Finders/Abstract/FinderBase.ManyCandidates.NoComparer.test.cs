using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {

    public void SetUpManyCandidatesAndNoComparers()
    {
      SetUpManyMockCandidates();
      SetUpMockComparers();
    }

    [Test]
    public void Find_ManyCandidate_NoComparer_Result_ShouldNotFail()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_ManyCandidate_NoComparer_Result_ShouldSucceed()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_ManyCandidate_NoComparer_ResultNominee_ShouldBeIdeal()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.MockCandidateType.ShouldBe(MockCandidateType.Ideal);
    }

    [Test]
    public void Find_ManyCandidate_NoComparer_ResultHasRunnerUp_ShouldBeTrue()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(true);
    }

    [Test]
    public void Find_ManyCandidate_NoComparer_ResultRunnerUp_ShouldBeIdeal()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.MockCandidateType.ShouldBe(MockCandidateType.Ideal);
    }

    [Test]
    public void Find_ManyCandidates_NoComparer_ResultAllQualifiedCandidates_ShouldNotContainNull()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates
        .ShouldNotContain(candidate => candidate.IsNull);
    }

    [Test]
    public void Find_ManyCandidate_NoComparer_ResultAllQualifiedCandidates_ShouldContainExpectedTotal()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(TotalNumberOfCandidates);
    }

    [Test]
    public void Find_ManyCandidate_NoComparer_ResultAllQualifiedCandidates_ShouldContainExpectedUnqualified()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      var unqualifiedCandidates =
        result.AllQualifiedCandidates
          .Where(candidate => candidate.MockCandidateType == MockCandidateType.Unqualified);

      _candidates
        .ShouldContain(
          (candidate) => result.AllQualifiedCandidates.Contains(candidate), NumberOfUnqualifiedCandidates);
    }

    [Test]
    public void Find_ManyCandidate_NoComparer_ResultAllQualifiedCandidates_ShouldContainExpectedQualified()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      var qualifiedCandidates =
        result.AllQualifiedCandidates
          .Where(candidate => candidate.MockCandidateType == MockCandidateType.Qualified);

      _candidates
        .ShouldContain(
          (candidate) => result.AllQualifiedCandidates.Contains(candidate), NumberOfQualifiedCandidates);
    }

    [Test]
    public void Find_ManyCandidate_NoComparer_ResultAllQualifiedCandidates_ShouldContainExpectedIdeal()
    {
      SetUpManyCandidatesAndNoComparers();
      var result = _sut.Find(_mockFindOptions.Object);

      var idealCandidates =
        result.AllQualifiedCandidates
          .Where(candidate => candidate.MockCandidateType == MockCandidateType.Ideal);

      _candidates
        .ShouldContain(
          (candidate) => result.AllQualifiedCandidates.Contains(candidate), NumberOfIdealCandidates);
    }
  }
}