using Moq;
using NUnit.Framework;
using Shouldly;

using TournamentsEnhanced;


namespace Test
{
  public partial class FinderBaseTest
  {
    [Test]
    public void Find_NoCandidate_OneComparer_Result_ShouldFail()
    {
      SetUpOneComparerWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_Result_ShouldNotSucceed()
    {
      SetUpOneComparerWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_ResultNominee_ShouldBeNull()
    {
      SetUpOneComparerWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.IsNull.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpOneComparerWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_ResultRunnerUp_ShouldBeNull()
    {
      SetUpOneComparerWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      SetUpOneComparerWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }

  }
}