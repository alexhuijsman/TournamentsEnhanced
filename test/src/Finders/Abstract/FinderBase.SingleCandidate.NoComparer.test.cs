using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    [Test]
    public void Find_SingleCandidate_NoComparer_Result_ShouldNotFail()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_NoComparer_Result_ShouldSucceed()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(true);
    }

    [Test]
    public void Find_SingleCandidate_NoComparer_ResultNominee_ShouldBeExpected()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.ShouldBe(_mockCandidates[0].Object);
    }

    [Test]
    public void Find_SingleCandidate_NoComparer_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_SingleCandidate_NoComparer_ResultRunnerUp_ShouldBeNull()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_SingleCandidate_NoComparer_ResultAllQualifiedCandidates_ShouldBeSingle()
    {
      SetUpMockCandidates(MockCandidateType.Qualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.AllQualifiedCandidates.Count.ShouldBe(1);
    }
  }
}