using Moq;
using NUnit.Framework;
using Shouldly;

using TournamentsEnhanced;


namespace Test
{
  public partial class FinderBaseTest : TestBase
  {
    [Test]
    public void Find_NoCandidate_OneComparer_OneFallback_Result_ShouldFail()
    {
      SetUpOneComparerAndOneFallbackWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Failed.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_OneFallback_Result_ShouldNotSucceed()
    {
      SetUpOneComparerAndOneFallbackWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Succeeded.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_OneFallback_ResultNominee_ShouldBeNull()
    {
      SetUpOneComparerAndOneFallbackWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.Nominee.IsNull.ShouldBe(true);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_OneFallback_ResultHasRunnerUp_ShouldBeFalse()
    {
      SetUpOneComparerAndOneFallbackWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.HasRunnerUp.ShouldBe(false);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_OneFallback_ResultRunnerUp_ShouldBeNull()
    {
      SetUpOneComparerAndOneFallbackWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      result.RunnerUp.ShouldBe(CandidateImpl.Null);
    }

    [Test]
    public void Find_NoCandidate_OneComparer_OneFallback_ResultAllQualifiedCandidates_ShouldBeNull()
    {
      SetUpOneComparerAndOneFallbackWithoutCandidates();

      var result = _sut.Find(_mockFindOptions.Object);

      Assert.IsNull(result.AllQualifiedCandidates);
    }

  }
}