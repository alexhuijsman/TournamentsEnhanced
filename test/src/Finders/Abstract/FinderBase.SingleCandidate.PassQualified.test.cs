using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    [Test]
    public void Find_SingleCandidate_SingleComparer_PassQualified_Result_ShouldNotFail()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_PassQualified_Result_ShouldSucceed()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_PassQualified_ResultNominee_ShouldBeExpected()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_candidates[0]);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_PassQualified_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_PassQualified_ResultRunnerUp_ShouldBeNull()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_PassQualified_ResultAllQualifiedCandidates_CountShouldBeOne()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(1);
    }
  }
}