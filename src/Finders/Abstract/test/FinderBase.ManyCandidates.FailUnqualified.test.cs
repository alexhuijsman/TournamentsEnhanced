using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;

using TournamentsEnhanced;


namespace Test
{
  public partial class FinderBaseTest : TestBase
  {
    private void SetUpManyCandidatesAndFailUnqualified()
    {
      SetUpManyMockCandidates();
      SetUpMockComparers(MockComparerType.FailUnqualified);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_Result_ShouldNotFail()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_Result_ShouldSucceed()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultNominee_ShouldBeIdeal()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.MockCandidateType.ShouldBe(MockCandidateType.Ideal);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultHasRunnerUp_ShouldBeTrue()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(true);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultRunnerUp_ShouldBeIdeal()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.MockCandidateType.ShouldBe(MockCandidateType.Ideal);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldContainExpectedTotal()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      var expectedNumberOfQualifiedCandidates = NumberOfIdealCandidates + NumberOfQualifiedCandidates;
      result.AllQualifiedCandidates.Count.ShouldBe(expectedNumberOfQualifiedCandidates);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldNotContainNull()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates
        .ShouldNotContain(candidate => candidate.IsNull);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldNotContainUnqualified()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates
        .ShouldNotContain(candidate => candidate.MockCandidateType == MockCandidateType.Unqualified);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldContainExpectedNumberOfQualified()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      var qualifiedCandidates =
        result.AllQualifiedCandidates
          .Where(candidate => candidate.MockCandidateType == MockCandidateType.Qualified);

      qualifiedCandidates.Count().ShouldBe(NumberOfQualifiedCandidates);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldContainExpectedQualified()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      var qualifiedCandidates =
        result.AllQualifiedCandidates
          .Where(candidate => candidate.MockCandidateType == MockCandidateType.Qualified);

      _candidates
        .ShouldContain(
          (candidate) => qualifiedCandidates.Contains(candidate), NumberOfQualifiedCandidates);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldHaveExpectedOrder()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.ShouldBeInOrder();
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldContainExpectedNumberOfIdeal()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      var idealCandidates =
        result.AllQualifiedCandidates
          .Where(candidate => candidate.MockCandidateType == MockCandidateType.Ideal);

      idealCandidates.Count().ShouldBe(NumberOfIdealCandidates);
    }

    [Test]
    public void Find_ManyCandidates_OneComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldContainExpectedIdeal()
    {
      SetUpManyCandidatesAndFailUnqualified();

      var result = _sut.Find(_mockFindOptions.Object);

      var idealCandidates =
        result.AllQualifiedCandidates
          .Where(candidate => candidate.MockCandidateType == MockCandidateType.Ideal);

      _candidates
        .ShouldContain(
          (candidate) => idealCandidates.Contains(candidate), NumberOfIdealCandidates);
    }

  }
}