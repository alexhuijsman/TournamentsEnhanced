using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    [Test]
    public void Find_SingleCandidate_SingleComparer_QualifyAll_Result_ShouldNotFail()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.QualifyAll);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_QualifyAll_Result_ShouldSucceed()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.QualifyAll);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_QualifyAll_ResultNominee_ShouldBeExpected()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.QualifyAll);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_mockCandidates[0].Object);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_QualifyAll_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.QualifyAll);

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_QualifyAll_ResultRunnerUp_ShouldBeNull()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.QualifyAll);

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_SingleCandidate_SingleComparer_QualifyAll_ResultAllQualifiedCandidates_ShouldBeSingle()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);
      SetUpMockComparers(MockComparerType.QualifyAll);

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(1);
    }
  }
}