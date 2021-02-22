using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;

using TournamentsEnhanced;


namespace Test
{
  public partial class FinderBaseTest : TestBase<FinderBaseImpl>
  {
    private void SetUpManyCandidatesAndManyComparers()
    {
      SetUpManyMockCandidates();
      SetUpManyMockComparers();
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_Result_ShouldNotFail()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_Result_ShouldSucceed()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_ResultNominee_ShouldBeIdeal()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.MockCandidateType.ShouldBe(MockCandidateType.Ideal);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_ResultHasRunnerUp_ShouldBeTrue()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(true);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_ResultRunnerUp_ShouldBeIdeal()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.MockCandidateType.ShouldBe(MockCandidateType.Ideal);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_ResultAllQualifiedCandidates_ShouldContainExpectedTotal()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(NumberOfIdealCandidates);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_ResultAllQualifiedCandidates_ShouldNotContainNull()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates
        .ShouldNotContain(candidate => candidate.IsNull);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_ResultAllQualifiedCandidates_ShouldNotContainUnqualified()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates
        .ShouldNotContain(candidate => candidate.MockCandidateType == MockCandidateType.Unqualified);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_ResultAllQualifiedCandidates_ShouldNotContainQualified()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates
           .ShouldNotContain(candidate => candidate.MockCandidateType == MockCandidateType.Qualified);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_ResultAllQualifiedCandidates_ShouldContainExpectedNumberOfIdeal()
    {
      SetUpManyCandidatesAndManyComparers();

      var result = _sut.Find(_mockFindOptions.Object);

      var idealCandidates =
        result.AllQualifiedCandidates
          .Where(candidate => candidate.MockCandidateType == MockCandidateType.Ideal);

      idealCandidates.Count().ShouldBe(NumberOfIdealCandidates);
    }

    [Test]
    public void Find_ManyCandidates_ManyComparers_ResultAllQualifiedCandidates_ShouldContainExpectedIdeal()
    {
      SetUpManyCandidatesAndManyComparers();

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