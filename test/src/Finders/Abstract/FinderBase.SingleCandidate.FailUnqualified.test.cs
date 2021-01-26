using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    [Test]
    public void Find_SingleCandidate_SingleComparer_FailUnqualified_Result_ShouldNotFail()
    {
      SetUpMockCandidates(MockCandidateType.Unqualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_FailUnqualified_Result_ShouldSucceed()
    {
      SetUpMockCandidates(MockCandidateType.Unqualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_FailUnqualified_ResultNominee_ShouldBeExpected()
    {
      SetUpMockCandidates(MockCandidateType.Unqualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_mockCandidates[0].Object);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_FailUnqualified_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpMockCandidates(MockCandidateType.Unqualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_FailUnqualified_ResultRunnerUp_ShouldBeNull()
    {
      SetUpMockCandidates(MockCandidateType.Unqualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_FailUnqualified_ResultAllQualifiedCandidates_ShouldBeSingle()
    {
      SetUpMockCandidates(MockCandidateType.Unqualified);
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(1);
    }
  }
}