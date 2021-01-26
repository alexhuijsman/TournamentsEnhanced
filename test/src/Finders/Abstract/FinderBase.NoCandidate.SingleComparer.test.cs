using Moq;
using NUnit.Framework;
using Shouldly;

namespace TournamentsEnhanced.UnitTests
{
  public partial class FinderBaseTests
  {
    [Test]
    public void Find_NoCandidate_SingleComparer_Result_ShouldFail()
    {
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_Result_ShouldNotSucceed()
    {
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_ResultNominee_ShouldBeNull()
    {
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.IsNull.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_ResultRunnerUp_ShouldBeNull()
    {
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_NoCandidate_SingleComparer_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      SetUpMockComparers(MockComparerType.FailUnqualified);

      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }
  }
}